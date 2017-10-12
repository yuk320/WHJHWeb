----------------------------------------------------------------------
-- 版本：2013
-- 时间：2013-04-22
-- 用途：领取有效好友奖励
----------------------------------------------------------------------
USE [WHJHTreasureDB]
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PJ_ReceiveSpreadAward]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PJ_ReceiveSpreadAward]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PJ_ReceiveSpreadAward]
	@UserID		INT,
	@ConfigID   INT,
	@strClientIP NVARCHAR(15),
	@strErrorDescribe NVARCHAR(127) OUTPUT
WITH ENCRYPTION AS

-- 属性设置
DECLARE @Nullity TINYINT
DECLARE @Count INT
DECLARE @FriendCount INT
DECLARE @SpreadNum INT
DECLARE @PresentDiamond INT
DECLARE @PresentPropID INT
DECLARE @PresentPropName NVARCHAR(32)
DECLARE @PresentPropNum INT
DECLARE @GoodsID INT
DECLARE @CurrentDiamond BIGINT
DECLARE @DateTime DATETIME

-- 执行逻辑
BEGIN
	SET @DateTime = GETDATE()
	-- 获取用户信息
	SELECT @Nullity=Nullity FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID=@UserID
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

	-- 获取配置信息
	SELECT @SpreadNum=SpreadNum,@PresentDiamond=PresentDiamond,@PresentPropID=PresentPropID,@PresentPropName=PresentPropName,@PresentPropNum=PresentPropNum FROM SpreadConfig WITH(NOLOCK) WHERE ConfigID=@ConfigID
	IF @SpreadNum IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，配置信息不存在'
		RETURN 2001
	END

	-- 获取游戏局数配置
	SELECT @Count=StatusValue FROM WHJHAccountsDB.dbo.SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'JJEffectiveFriendGame'
	IF @Count IS NULL OR @Count<0
	BEGIN
		SET @Count=1
	END

	-- 获取有效好友
	SELECT @FriendCount=COUNT(UserID) FROM (SELECT UserID,COUNT(UserID) AS UCount FROM WHJHPlatformDB.dbo.PersonalRoomScoreInfo WITH(NOLOCK) 
	WHERE UserID IN (SELECT UserID FROM WHJHAccountsDB.[dbo].AccountsInfo WITH(NOLOCK) WHERE SpreaderID = @UserID) GROUP BY UserID HAVING COUNT(UserID)>@Count) AS P

	-- 判断有效好友
	IF @FriendCount<@SpreadNum
	BEGIN
		SET @strErrorDescribe=N'抱歉，领取用户有效好友数不足'
		RETURN 2001
	END

	-- 开启事务
	BEGIN TRAN

	SELECT @CurrentDiamond=Diamond FROM UserCurrency WITH(ROWLOCK) WHERE UserID=@UserID
	IF @CurrentDiamond IS NULL
	BEGIN
		SET @CurrentDiamond = 0
		INSERT INTO UserCurrency VALUES(@UserID,0)
	END
	IF EXISTS(SELECT RecordID FROM RecordSpreadAward WHERE UserID=@UserID AND ConfigID=@ConfigID)
	BEGIN
		SET @strErrorDescribe=N'抱歉，领取用户已经领取过奖励'
		ROLLBACK TRAN
		RETURN 2002
	END

	-- 赠送钻石
	UPDATE UserCurrency SET Diamond = Diamond + @PresentDiamond WHERE UserID = @UserID
	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，领取异常，请稍后重试'
		ROLLBACK TRAN
		RETURN 2003
	END

	-- 写入记录
	INSERT INTO RecordSpreadAward(UserID,UserNum,ConfigID,SpreadNum,CurrentDiamond,PresentDiamond,PresentPropID,PresentPropName,PresentPropNum,ClientIP,CollectDate) 
	VALUES(@UserID,@FriendCount,@ConfigID,@SpreadNum,@CurrentDiamond,@PresentDiamond,@PresentPropID,@PresentPropName,@PresentPropNum,@strClientIP,GETDATE())
	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，领取异常，请稍后重试'
		ROLLBACK TRAN
		RETURN 2004
	END

	COMMIT TRAN

	-- 赠送道具
	SELECT @GoodsID=GoodsID FROM WHJHAccountsDB.dbo.AccountsPackage WHERE UserID=@UserID AND GoodsID=@PresentPropID
	IF @GoodsID IS NULL
	BEGIN
		INSERT INTO WHJHAccountsDB.dbo.AccountsPackage(UserID,GoodsID,GoodShowID,GoodsSortID,GoodsCount,PushTime) 
		VALUES(@UserID,@PresentPropID,0,0,@PresentPropNum,@DateTime)
	END
	ELSE
	BEGIN
		UPDATE WHJHAccountsDB.dbo.AccountsPackage SET GoodsCount=GoodsCount+@PresentPropNum WHERE UserID=@UserID AND GoodsID=@PresentPropID
	END

	-- 写入钻石流水记录
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
	VALUES(dbo.WF_GetSerialNumber(),0,@UserID,2,@CurrentDiamond,@PresentDiamond,@strClientIP,@DateTime)

END
GO


