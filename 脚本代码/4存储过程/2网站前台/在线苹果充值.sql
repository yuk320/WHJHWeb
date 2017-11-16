----------------------------------------------------------------------
-- 版权：2017
-- 时间：2017-06-8
-- 用途：苹果充值
----------------------------------------------------------------------

USE [WHJHTreasureDB]
GO

-- 在线充值
IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_FinishOnLineOrderIOS') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_FinishOnLineOrderIOS
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

---------------------------------------------------------------------------------------
-- 在线充值
CREATE PROCEDURE NET_PW_FinishOnLineOrderIOS
	@strOrdersID		NVARCHAR(50),			--	订单编号
	@PayAmount			DECIMAL(18,2),			--  支付金额
	@dwUserID			INT,					--	充值用户
	@strAppleID			NVARCHAR(32),			--  产品标识
	@strIPAddress		NVARCHAR(31),			--	用户帐号	
	@strErrorDescribe	NVARCHAR(127) OUTPUT	--	输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 帐号资料
DECLARE @Accounts NVARCHAR(31)
DECLARE @NickName NVARCHAR(31)
DECLARE @UserID INT
DECLARE @GameID INT
DECLARE @SpreaderID INT
DECLARE @Nullity TINYINT
DECLARE @BindSpread INT

-- 订单信息
DECLARE @ConfigID INT
DECLARE @Amount DECIMAL(18,2)
DECLARE @Diamond INT
DECLARE @PresentDiamond INT
DECLARE @OtherPresent INT
DECLARE @BeforeDiamond BIGINT
DECLARE @OrderStatus TINYINT
DECLARE @DateTime DATETIME
DECLARE @PresentScale DECIMAL(18,2)

-- 执行逻辑
BEGIN
	SET @DateTime = GETDATE()

	-- 获取用户信息
	SELECT @UserID=UserID,@GameID=GameID,@SpreaderID=SpreaderID,@Accounts=Accounts,@NickName=NickName,@Nullity=Nullity FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID = @dwUserID
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号不存在！'
		RETURN 1001
	END
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号已冻结状态！'
		RETURN 1002
	END

	-- 充值推广验证
	SELECT @BindSpread=StatusValue FROM WHJHAccountsDB.dbo.SystemStatusInfo WITH(NOLOCK) WHERE StatusName = N'JJPayBindSpread'
	IF @SpreaderID<=0 AND @BindSpread=0
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值账号未绑定推广人！'
		RETURN 1003
	END

	-- 配置查询
	SELECT @ConfigID=ConfigID,@Amount=PayPrice,@Diamond=Diamond,@PresentScale=PresentScale FROM AppPayConfig WITH(NOLOCK) WHERE PayType=1 AND AppleID=@strAppleID
	IF @ConfigID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值产品不存在!'
		RETURN 2001
	END
	IF @Amount <= 0 OR @Diamond <=0
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值产品配置异常！'
		RETURN 2002
	END
	IF @Amount!=@PayAmount
	BEGIN
		SET @strErrorDescribe=N'抱歉！支付金额错误!'
		RETURN 2003
	END

	-- 订单查询
	IF EXISTS(SELECT OnLineID FROM OnLinePayOrder WITH(NOLOCK) WHERE OrderID = @strOrdersID)
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单已完成!'
		RETURN 2004
	END

	-- 计算额外赠送钻石
	SET @OtherPresent = CAST((@Diamond*@PresentScale) AS INT)
	SET @PresentDiamond = @OtherPresent + @Diamond

	-- 事务处理
	BEGIN TRAN

	SELECT @BeforeDiamond=Diamond FROM UserCurrency WITH(ROWLOCK) WHERE UserID=@UserID
	IF @BeforeDiamond IS NULL
	BEGIN
		SET @BeforeDiamond=0
		INSERT INTO UserCurrency VALUES(@UserID,@PresentDiamond)
	END
	ELSE
	BEGIN
		UPDATE UserCurrency SET Diamond = Diamond + @PresentDiamond WHERE UserID=@UserID
	END
	IF @@ROWCOUNT <=0
	BEGIN
		ROLLBACK TRAN
		SET @strErrorDescribe=N'抱歉！操作异常，请稍后重试!'
		RETURN 3001
	END
	INSERT INTO OnLinePayOrder(ConfigID,ShareID,UserID,GameID,Accounts,NickName,OrderID,OrderType,Amount,Diamond,PresentScale,OtherPresent,OrderStatus,OrderDate,OrderAddress,BeforeDiamond,PayDate,PayAddress) 
	VALUES(@ConfigID,800,@UserID,@GameID,@Accounts,@NickName,@strOrdersID,1,@Amount,@Diamond,@PresentScale,@OtherPresent,1,@DateTime,@strIPAddress,@BeforeDiamond,@DateTime,@strIPAddress)
	IF @@ROWCOUNT <=0
	BEGIN
		ROLLBACK TRAN
		SET @strErrorDescribe=N'抱歉！操作异常，请稍后重试!'
		RETURN 3002
	END

	-- 写入钻石变化记录
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
	VALUES(dbo.WF_GetSerialNumber(),0,@UserID,3,@BeforeDiamond,@PresentDiamond,@strIPAddress,@DateTime)

	-- 如果存在返利配置，写入返利记录
	IF EXISTS (SELECT 1 FROM SpreadReturnConfig WHERE Nullity=0)
	BEGIN
		INSERT WHJHRecordDBLink.WHJHRecordDB.DBO.RecordSpreadReturn (SourceUserID,TargetUserID,SourceDiamond,SpreadlEvel,ReturnScale,ReturnNum,ReturnType,CollectDate) 
		SELECT @UserID,A.UserID,@Diamond,B.SpreadLevel,B.PresentScale,@Diamond*B.PriesentScale,B.PresentType,@DateTime FROM (SELECT UserID,LevelID FROM [dbo].[WF_GetAgentAboveAccounts](@UserID) ) AS A,SpreadReturnConfig AS B WHERE B.SpreadLevel=A.LevelID-1 AND A.LevelID>1 AND A.LevelID<=4 AND B.Nullity=0
	END

	COMMIT TRAN

END 
RETURN 0
GO



