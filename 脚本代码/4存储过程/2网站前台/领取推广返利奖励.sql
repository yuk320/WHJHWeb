----------------------------------------------------------------------
-- 版本：2017
-- 时间：2017-11-15
-- 用途：领取充值返利
----------------------------------------------------------------------
USE [WHJHRecordDB]
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

IF EXISTS (SELECT *
FROM DBO.SYSOBJECTS
WHERE ID = OBJECT_ID(N'[dbo].[NET_PJ_ReceiveSpreadReturn]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PJ_ReceiveSpreadReturn]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PJ_ReceiveSpreadReturn]
  @dwUserID		INT,
  @dwNum   INT,
  @strClientIP NVARCHAR(15),
  @strErrorDescribe NVARCHAR(127) OUTPUT
WITH
  ENCRYPTION
AS

-- 属性设置
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @ReceiveType TINYINT
DECLARE @ReceiveCondition INT
DECLARE @DateTime DATETIME
DECLARE @TotalReturn BIGINT
DECLARE @TotalReceive BIGINT
DECLARE @ReceiveBefore BIGINT

-- 执行逻辑
BEGIN
  SET @DateTime = GETDATE()
  -- 获取用户信息
  SELECT @UserID=UserID, @Nullity=Nullity
  FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK)

  WHERE UserID=@dwUserID
  IF @Nullity IS NULL
	BEGIN
    SET @strErrorDescribe=N'抱歉，领取用户不存在'
    RETURN 1001
  END
  IF @Nullity=1
	BEGIN
    SET @strErrorDescribe=N'抱歉，领取用户已冻结'
    RETURN 1002
  END

  -- 全局推广返利类别
  SELECT @ReceiveType = StatusValue FROM WHJHAccountsDBLink.WHJHAccountsDB.DBO.SystemStatusInfo WHERE StatusName = N'SpreadReturnType'
  IF @ReceiveType IS NULL
  BEGIN
    SET @ReceiveType = 0;
  END

  -- 全局推广返利条件
  SELECT @ReceiveCondition = StatusValue FROM WHJHAccountsDBLink.WHJHAccountsDB.DBO.SystemStatusInfo WHERE StatusName = N'SpreadReceiveBase'
  IF @ReceiveCondition IS NULL
  BEGIN
    SET @ReceiveCondition = 0; -- 领取返利不限制
  END

  SELECT @TotalReturn =  CAST(ISNULL(SUM(ReturnNum),0) AS BIGINT)
  FROM RecordSpreadReturn
  WHERE TargetUserID=@UserID AND ReturnType = @ReceiveType
  SELECT @TotalReceive = CAST(ISNULL(SUM(ReceiveNum),0) AS BIGINT)
  FROM RecordSpreadReturnReceive
  WHERE UserID=@UserID AND ReceiveType = @ReceiveType
  IF @TotalReturn = 0 OR @dwNum>@TotalReturn-@TotalReceive
  BEGIN
    SET @strErrorDescribe=N'抱歉，可领取奖励不足'
    RETURN 2003
  END

  IF @TotalReturn-@TotalReceive < @ReceiveCondition
  BEGIN
    SET @strErrorDescribe=N'抱歉，当前领取门槛为可领取数大于等于'+LTRIM(STR(@ReceiveCondition)) +',您没有满足条件'
    RETURN 2003
  END


  -- 开启事务
  BEGIN TRAN

  IF @ReceiveType = 1
  BEGIN
    -- 领取类型为钻石
    SELECT @ReceiveBefore = Diamond
    FROM WHJHTreasureDB.DBO.UserCurrency
    WHERE UserID = 0
    IF @ReceiveBefore IS NULL
    BEGIN
      SET @ReceiveBefore = 0
      INSERT INTO WHJHTreasureDB.DBO.UserCurrency
      VALUES(@UserID, 0)
    END

    UPDATE WHJHTreasureDB.DBO.UserCurrency SET Diamond = Diamond + @dwNum WHERE UserID = @UserID
    IF @@ROWCOUNT<=0
    BEGIN
      SET @strErrorDescribe=N'抱歉，领取异常，请稍后重试'
      ROLLBACK TRAN
      RETURN 2004
    END

    -- 写入钻石流水记录
    INSERT INTO RecordDiamondSerial
      (SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
    VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 13, @ReceiveBefore, @dwNum, @strClientIP, @DateTime)
    IF @@ROWCOUNT<=0
    BEGIN
      SET @strErrorDescribe=N'抱歉，领取异常，请稍后重试'
      ROLLBACK TRAN
      RETURN 2004
    END

  END
  IF @ReceiveType = 0
  BEGIN
    DECLARE @BeforeInsure BIGINT
    DECLARE @BeforeScore BIGINT
    -- 领取类型为金币
    SELECT @BeforeScore = Score, @BeforeInsure = InsureScore
    FROM WHJHTreasureDB.DBO.GameScoreInfo
    WHERE UserID = @dwUserID
    IF @BeforeScore IS NULL AND @BeforeInsure IS NULL
    BEGIN
      SET @BeforeScore = 0
      SET @BeforeInsure = 0
      SET @ReceiveBefore = 0
      INSERT INTO WHJHTreasureDB.DBO.GameScoreInfo
        (UserID,Score,InsureScore,LastLogonIP)
      VALUES(@UserID, @BeforeScore, @BeforeInsure, @strClientIP)
    END
    SET @ReceiveBefore = @BeforeScore + @BeforeInsure
    UPDATE WHJHTreasureDB.DBO.GameScoreInfo SET InsureScore = InsureScore + @dwNum WHERE UserID = @UserID
    IF @@ROWCOUNT<=0
    BEGIN
      SET @strErrorDescribe=N'抱歉，领取异常，请稍后重试'
      ROLLBACK TRAN
      RETURN 2004
    END

    -- 写入金币流水记录
    INSERT INTO RecordTreasureSerial
      (SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate)
    VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 9, @BeforeScore, @BeforeInsure, @dwNum, @strClientIP, @DateTime)
    IF @@ROWCOUNT<=0
    BEGIN
      SET @strErrorDescribe=N'抱歉，领取异常，请稍后重试'
      ROLLBACK TRAN
      RETURN 2004
    END

  END

  -- 写入领取记录
  INSERT INTO RecordSpreadReturnReceive
    (UserID,ReceiveType,ReceiveNum,ReceiveBefore,ReceiveAddress,CollectDate)
  VALUES(@UserID, @ReceiveType, @dwNum, @ReceiveBefore, @strClientIP, @DateTime)
  IF @@ROWCOUNT<=0
  BEGIN
    SET @strErrorDescribe=N'抱歉，领取异常，请稍后重试'
    ROLLBACK TRAN
    RETURN 2004
  END

  COMMIT TRAN

  RETURN 0
END
GO
