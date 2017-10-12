----------------------------------------------------------------------
-- 版本：2013
-- 时间：2013-04-22
-- 用途：领取排行榜奖励
----------------------------------------------------------------------
USE [WHJHNativeWebDB]
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PJ_RecevieRankingAward]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PJ_RecevieRankingAward]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PJ_RecevieRankingAward]
	@dwUserID INT,
	@dwDateID INT,
	@dwTypeID INT,
	@strClientIP NVARCHAR(15),
	@strErrorDescribe NVARCHAR(127) OUTPUT	-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
DECLARE @Nullity INT
DECLARE @Diamond INT
DECLARE @BeforeDiamond BIGINT
DECLARE @ValidityTime DATETIME
DECLARE @ReceiveState BIT
DECLARE @DateTimeNow DATETIME

-- 执行逻辑
BEGIN
	-- 获取用户信息
	SELECT @Nullity=Nullity FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
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
	
	-- 获取用户钻石信息
	SELECT @BeforeDiamond=Diamond FROM WHJHTreasureDB.dbo.UserCurrency WHERE UserID=@dwUserID
	IF @BeforeDiamond IS NULL
	BEGIN
		SET @BeforeDiamond = 0
		INSERT INTO WHJHTreasureDB.dbo.UserCurrency VALUES(@dwUserID,0)
	END

	SET @DateTimeNow = GETDATE()

	-- 获取排行榜奖励信息
	SELECT @Diamond=Diamond,@ValidityTime=ValidityTime,@ReceiveState=ReceiveState FROM RecordRankingRecevie WITH(NOLOCK) WHERE DateID=@dwDateID AND UserID=@dwUserID AND TypeID=@dwTypeID
	IF @Diamond IS NULL
	BEGIN
		SET @strErrorDescribe =N'抱歉,您暂无排行榜奖励'
		RETURN 1003
	END
	IF @ReceiveState=1
	BEGIN
		SET @strErrorDescribe =N'抱歉,排行榜奖励不能重复领取'
		RETURN 1003
	END
	IF @ValidityTime< @DateTimeNow
	BEGIN
		SET @strErrorDescribe =N'抱歉,排行榜奖励已过期'
		RETURN 1003
	END

	-- 修改领取状态
	UPDATE RecordRankingRecevie SET ReceiveState=1,BeforeDiamond=@BeforeDiamond,ReceiveIP=@strClientIP,ReceiveTime=@DateTimeNow WHERE DateID=@dwDateID AND UserID=@dwUserID AND TypeID=@dwTypeID
	IF @@ROWCOUNT <= 0
	BEGIN
		SET @strErrorDescribe=N'抱歉,排行榜奖励领取失败！'
		RETURN 6
	END

	-- 修改领取后钻石信息
	UPDATE WHJHTreasureDB.dbo.UserCurrency SET Diamond=Diamond+@Diamond WHERE UserID=@dwUserID

	-- 写入钻石流水记录
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
	VALUES(dbo.WF_GetSerialNumber(),0,@dwUserID,5,@BeforeDiamond,@Diamond,@strClientIP,@DateTimeNow)

	-- 输出领取后钻石数
	SELECT @dwUserID AS UserID,(@BeforeDiamond+@Diamond) AS Diamond
	SET @strErrorDescribe= N'恭喜您获得'+ CAST(@Diamond AS NVARCHAR(30)) +'钻石排行奖励'

END
RETURN 0
GO
