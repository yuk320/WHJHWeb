----------------------------------------------------------------------
-- 版本：2013
-- 时间：2013-04-22
-- 用途：领取注册赠送奖励
----------------------------------------------------------------------
USE [WHJHTreasureDB]
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PJ_RecevieRegisterGrant]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PJ_RecevieRegisterGrant]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PJ_RecevieRegisterGrant]
	@dwUserID INT,
	@strClientIP NVARCHAR(15),
	@strErrorDescribe NVARCHAR(127) OUTPUT	-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
DECLARE @Nullity INT
DECLARE @Diamond INT
DECLARE @GrantDiamond INT
DECLARE @GrantGold INT
DECLARE @BeforeDiamond BIGINT
DECLARE @BeforeGold BIGINT
DECLARE @BeforeInsureScore BIGINT
DECLARE @ValidityTime DATETIME
DECLARE @ReceiveState BIT
DECLARE @DateTimeNow DATETIME
DECLARE @RegisterIP NVARCHAR(15)
DECLARE @RegisterDate DATETIME
DECLARE @RegisterMachine NVARCHAR(32)
DECLARE @Descript NVARCHAR(5)

-- 执行逻辑
BEGIN
	-- 获取用户信息
	SELECT @Nullity=Nullity,@RegisterIP=RegisterIP,@RegisterDate=RegisterDate,@RegisterMachine=RegisterMachine FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
	IF @Nullity IS NULL
	BEGIN
		SET @strErrorDescribe =N'抱歉,领取用户不存在'
		RETURN 1001
	END
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe =N'抱歉,领取用户已冻结'
		RETURN 1002
	END
	
	-- 获取注册记录
	SELECT @GrantDiamond=GrantDiamond,@GrantGold=GrantGold,@ReceiveState=IsReceive FROM WHJHRecordDB.dbo.RecordRegisterGrant WITH(NOLOCK) WHERE UserID = @dwUserID
	IF @ReceiveState IS NULL
	BEGIN
		SET @strErrorDescribe =N'抱歉,领取用户不存在'
		RETURN 2001
	END
	IF @ReceiveState=1
	BEGIN
		SET @strErrorDescribe =N'抱歉,注册奖励已领取'
		RETURN 2002
	END
	IF @GrantDiamond<=0 AND @GrantGold<=0
	BEGIN
		SET @strErrorDescribe =N'抱歉,注册奖励异常'
		RETURN 2003
	END

	SET @strErrorDescribe= N'恭喜您获得'

	-- 注册奖励为钻石
	IF @GrantDiamond>0 SET @Descript='，' ELSE SET @Descript=''
	IF @GrantDiamond>0
	BEGIN
		SET @strErrorDescribe = @strErrorDescribe + CAST(@GrantDiamond AS NVARCHAR(30)) + '钻石'
		-- 更新用户钻石信息
		SELECT @BeforeDiamond=Diamond FROM UserCurrency WHERE UserID=@dwUserID
		IF @BeforeDiamond IS NULL
		BEGIN
			SET @BeforeDiamond = 0
			INSERT INTO UserCurrency VALUES(@dwUserID,@GrantDiamond)
		END
		ELSE 
		BEGIN
			UPDATE UserCurrency SET Diamond=Diamond + @GrantDiamond WHERE UserID=@dwUserID
		END
	END

	-- 注册奖励为金币
	IF @GrantGold>0
	BEGIN
		SET @strErrorDescribe = @strErrorDescribe + @Descript + CAST(@GrantGold AS NVARCHAR(30)) + '金币'
		-- 更新用户金币信息
		SELECT @BeforeGold=Score,@BeforeInsureScore=InsureScore FROM GameScoreInfo WHERE UserID = @dwUserID
		IF @BeforeGold IS NULL
		BEGIN
			SET @BeforeGold = 0
			SET @BeforeInsureScore = 0
			INSERT INTO GameScoreInfo(UserID,Score,RegisterIP,RegisterDate,RegisterMachine) VALUES(@dwUserID,@GrantGold,@RegisterIP,@RegisterDate,@RegisterMachine)
		END
		ELSE
		BEGIN
			UPDATE GameScoreInfo SET Score = Score + @GrantGold WHERE UserID=@dwUserID
		END
	END
	SET @strErrorDescribe = @strErrorDescribe + '注册奖励'

	-- 更新注册记录
	SET @DateTimeNow = GETDATE()
	UPDATE WHJHRecordDB.dbo.RecordRegisterGrant SET IsReceive=1,ReceiveDate=@DateTimeNow,ReceiveIP=@strClientIP WHERE UserID=@dwUserID
	IF @@ROWCOUNT>0
	BEGIN
		 -- 写入流水记录
		 IF @GrantDiamond>0
		 BEGIN
			INSERT INTO WHJHRecordDBLink.WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
			VALUES(dbo.WF_GetSerialNumber(),0,@dwUserID,1,@BeforeDiamond,@GrantDiamond,@strClientIP,@DateTimeNow)
		 END
		 IF @GrantGold>0
		 BEGIN
			INSERT INTO WHJHRecordDBLink.WHJHRecordDB.dbo.RecordTreasureSerial(SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate) 
			VALUES(dbo.WF_GetSerialNumber(),0,@dwUserID,1,@BeforeGold,@BeforeInsureScore,@GrantGold,@strClientIP,@DateTimeNow)
		 END
	END

	-- 输出领取后钻石数和金币数
	SELECT (@BeforeGold+@GrantGold) AS Score,@BeforeInsureScore AS InsureScore,(@BeforeDiamond+@GrantDiamond) AS Diamond
END
RETURN 0
GO
