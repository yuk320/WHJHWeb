----------------------------------------------------------------------------------------------------
-- 版权：2017
-- 时间：2017-10-11
-- 用途：钻石兑换金币
----------------------------------------------------------------------------------------------------

USE WHJHTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_DiamondExchangeGold') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_DiamondExchangeGold
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_DiamondExchangeGold
	@dwUserID INT,
	@dwConfigID INT,
	@dwTypeID	INT,
	@strClientIP NVARCHAR(15),
	@strErrorDescribe	NVARCHAR(127) OUTPUT		-- 输出信息

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

DECLARE @Nullity BIT
DECLARE @CurDiamond BIGINT
DECLARE @ExchDiamond BIGINT
DECLARE @CurScore BIGINT
DECLARE @CurInsureScore BIGINT
DECLARE @PresentGold BIGINT

-- 执行逻辑
BEGIN
	-- 获取用户信息
	SELECT @Nullity=Nullity FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
	IF @Nullity IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，兑换用户不存在'
		RETURN 1001
	END
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，兑换用户已冻结'
		RETURN 1002
	END

	-- 查询兑换配置
	SELECT @ExchDiamond=[Diamond], @PresentGold = [ExchGold] FROM [CurrencyExchConfig]WITH(NOLOCK) WHERE ConfigID = @dwConfigID

	IF @ExchDiamond IS NULL OR @PresentGold IS NULL
	BEGIN
		SET @strErrorDescribe = N'抱歉，钻石兑换金币配置有误'
		RETURN 1003
	END

	SELECT @CurDiamond=Diamond FROM UserCurrency WITH(ROWLOCK) WHERE UserID=@dwUserID
	IF @CurDiamond IS NULL OR @CurDiamond < @ExchDiamond
	BEGIN
		SET @strErrorDescribe = N'抱歉，玩家钻石不足'
		RETURN 1004
	END

	-- 新用户处理
	IF NOT EXISTS (SELECT 1 FROM [GameScoreInfo]WITH(NOLOCK) WHERE UserID = @dwUserID)
	BEGIN
		INSERT [GameScoreInfo] (UserID) VALUES (@dwUserID)
	END

	-- 查询用户金币信息
	SELECT @CurScore=Score,@CurInsureScore=InsureScore FROM [GameScoreInfo]WITH(NOLOCK) WHERE UserID = @dwUserID

	-- 开启事务
	BEGIN TRAN

	-- 扣除钻石
	UPDATE [UserCurrency] SET Diamond = Diamond - @ExchDiamond WHERE UserID = @dwUserID
	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，兑换异常，请稍后重试'
		ROLLBACK TRAN
		RETURN 2003
	END

	-- 增加金币
	UPDATE [GameScoreInfo] SET Score = @CurScore + @PresentGold WHERE UserID = @dwUserID
 	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，兑换异常，请稍后重试'
		ROLLBACK TRAN
		RETURN 2003
	END

	-- 写入记录 （兑换记录）
	INSERT INTO WHJHRecordDB.DBO.RecordCurrencyExch(UserID,TypeID,CurDiamond,ExchDiamond,CurScore,CurInsureScore,PresentGold,ClientIP,CollectDate)
	VALUES(@dwUserID,@dwTypeID,@CurDiamond,@ExchDiamond,@CurScore,@CurInsureScore,@PresentGold,@strClientIP,GETDATE())
	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，兑换异常，请稍后重试'
		ROLLBACK TRAN
		RETURN 2004
	END

	-- 钻石流水 （兑换记录）
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
	VALUES(dbo.WF_GetSerialNumber(),0,@dwUserID,12,@CurDiamond,-@ExchDiamond,@strClientIP,GETDATE())
	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，兑换异常，请稍后重试'
		ROLLBACK TRAN
		RETURN 2004
	END
	-- 金币流水 （兑换记录）
	INSERT INTO WHJHRecordDB.dbo.RecordTreasureSerial(SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate)
		VALUES(dbo.WF_GetSerialNumber(),0,@dwUserID,5,@CurScore,@CurInsureScore,@PresentGold,@strClientIP,GETDATE())
	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，兑换异常，请稍后重试'
		ROLLBACK TRAN
		RETURN 2004
	END

	COMMIT TRAN

	SET @strErrorDescribe = N'钻石兑换金币成功，消耗['+CAST(@ExchDiamond AS NVARCHAR(30))+']钻石，获得['+CAST(@PresentGold AS NVARCHAR(30))+']金币。'

	SELECT @CurScore + @PresentGold AS AfterScore,@CurInsureScore AS AfterInsureScore,@CurDiamond-@ExchDiamond AS AfterDiamond,@ExchDiamond AS ExchDiamond,@PresentGold AS PresentGold
END

RETURN 0

GO
