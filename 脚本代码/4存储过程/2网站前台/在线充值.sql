----------------------------------------------------------------------
-- 版权：2017
-- 时间：2017-06-8
-- 用途：在线充值
----------------------------------------------------------------------

USE [WHJHTreasureDB]
GO

-- 在线充值
IF EXISTS (SELECT *
FROM DBO.SYSOBJECTS
WHERE ID = OBJECT_ID(N'[dbo].NET_PW_FinishOnLineOrder') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_FinishOnLineOrder
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

---------------------------------------------------------------------------------------
-- 在线充值
CREATE PROCEDURE NET_PW_FinishOnLineOrder
	@strOrdersID		NVARCHAR(50),
	--	订单编号
	@PayAmount			DECIMAL(18,2),
	--  支付金额
	@strIPAddress		NVARCHAR(31),
	--	用户帐号	
	@strErrorDescribe	NVARCHAR(127) OUTPUT
--	输出信息
WITH
	ENCRYPTION
AS

-- 属性设置
SET NOCOUNT ON

-- 订单信息
DECLARE @UserID INT
DECLARE @Amount DECIMAL(18,2)
DECLARE @Diamond INT
DECLARE @PresentDiamond INT
DECLARE @OtherPresent INT
DECLARE @BeforeDiamond BIGINT
DECLARE @OrderStatus TINYINT
DECLARE @PayIdentity TINYINT
DECLARE @DateTime DATETIME
DECLARE @CurrentTime DATETIME
DECLARE @STime NVARCHAR(10)
DECLARE @StartTime NVARCHAR(20)
DECLARE @EndTime NVARCHAR(20)

-- 执行逻辑
BEGIN
	SET @DateTime = GETDATE()
	-- 订单查询
	SELECT @UserID=UserID, @Amount=Amount, @Diamond=Diamond, @OtherPresent=OtherPresent, @OrderStatus=OrderStatus
	FROM OnLinePayOrder WITH(NOLOCK)
	WHERE OrderID = @strOrdersID
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单不存在!'
		RETURN 1001
	END
	IF @OrderStatus=1
	BEGIN
		SET @strErrorDescribe=N'抱歉！充值订单已完成!'
		RETURN 1002
	END
	IF @Amount != @PayAmount
	BEGIN
		SET @strErrorDescribe=N'抱歉！支付金额错误!'
		RETURN 1003
	END

	--时间计算
	SELECT @CurrentTime = GETDATE()
	SET @STime = Convert(CHAR(10),@CurrentTime,120)
	SET @StartTime = @STime + N' 00:00:00'
	SET @EndTime = @STime + N' 23:59:59'
	-- 对额外赠送字段进行条件过滤
	IF @PayIdentity=2	-- 每日首冲
	BEGIN
		IF EXISTS(SELECT OnLineID
		FROM OnLinePayOrder
		WHERE UserID=@UserID AND OrderStatus=1 AND OrderDate BETWEEN @StartTime AND @EndTime)
		BEGIN
			SET @OtherPresent = 0
		END
	END
	ELSE IF @PayIdentity=3 --预增加 账户首冲模式
	BEGIN
		IF EXISTS(SELECT OnLineID
		FROM OnLinePayOrder
		WHERE UserID=@UserID AND OrderStatus=1)
		BEGIN
			SET @OtherPresent = 0
		END
	END
	ELSE
	BEGIN
		--其他情况一律过滤
		SET @OtherPresent = 0
	END

	SET @PresentDiamond = @Diamond + @OtherPresent

	-- 事务处理
	BEGIN TRAN

	SELECT @BeforeDiamond=Diamond
	FROM UserCurrency WITH(ROWLOCK)
	WHERE UserID=@UserID
	IF @BeforeDiamond IS NULL
	BEGIN
		SET @BeforeDiamond=0
		INSERT INTO UserCurrency
		VALUES(@UserID, @PresentDiamond)
	END
	ELSE
	BEGIN
		UPDATE UserCurrency SET Diamond = Diamond + @PresentDiamond WHERE UserID=@UserID
	END
	IF @@ROWCOUNT <=0
	BEGIN
		ROLLBACK TRAN
		SET @strErrorDescribe=N'抱歉！操作异常，请稍后重试!'
		RETURN 2001
	END
	UPDATE OnLinePayOrder SET OrderStatus=1,OtherPresent=@OtherPresent,BeforeDiamond=@BeforeDiamond,PayDate=@CurrentTime,PayAddress=@strIPAddress WHERE OrderID = @strOrdersID
	IF @@ROWCOUNT <=0
	BEGIN
		ROLLBACK TRAN
		SET @strErrorDescribe=N'抱歉！操作异常，请稍后重试!'
		RETURN 2001
	END

	-- 写入钻石流水记录
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial
		(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
	VALUES(dbo.WF_GetSerialNumber(), 0, @UserID, 3, @BeforeDiamond, @PresentDiamond, @strIPAddress, @DateTime)

	-- 如果存在返利配置，写入返利记录
	IF EXISTS (SELECT 1 FROM SpreadReturnConfig WHERE Nullity=0)
	BEGIN
		DECLARE @ReturnType TINYINT
		SELECT @ReturnType = StatusValue FROM WHJHAccountsDB.DBO.SystemStatusInfo WHERE StatusName = N'SpreadReturnType'
		IF @ReturnType IS NULL
		BEGIN
			SET @ReturnType = 0
		END
		INSERT WHJHRecordDB.DBO.RecordSpreadReturn (SourceUserID,TargetUserID,SourceDiamond,SpreadlEvel,ReturnScale,ReturnNum,ReturnType,CollectDate) 
		SELECT @UserID,A.UserID,@Diamond,B.SpreadLevel,B.PresentScale,@Diamond*B.PriesentScale,@ReturnType,@DateTime FROM (SELECT UserID,LevelID FROM [dbo].[WF_GetAgentAboveAccounts](@UserID) ) AS A,SpreadReturnConfig AS B WHERE B.SpreadLevel=A.LevelID-1 AND A.LevelID>1 AND A.LevelID<=4 AND B.Nullity=0
	END

	COMMIT TRAN

END
RETURN 0
GO