----------------------------------------------------------------------
-- 版本：2013
-- 时间：2013-04-22
-- 用途：每日整点统计钻石数量
----------------------------------------------------------------------
USE [WHJHTreasureDB]
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PJ_DiamondStatistics]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PJ_DiamondStatistics]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PJ_DiamondStatistics]
			
WITH ENCRYPTION AS

-- 属性设置
DECLARE @DateID INT
DECLARE @TodayTime DATETIME

DECLARE @SysPresentDiamond BIGINT
DECLARE @AAGameDiamond BIGINT
DECLARE @AdminPresentDiamond BIGINT
DECLARE @PayDiamond BIGINT

DECLARE @FirstDiamond BIGINT
DECLARE @SecondDiamond BIGINT
DECLARE @ThirdDiamond BIGINT
DECLARE @TotalDiamond BIGINT
DECLARE @RoomCostDiamond BIGINT
DECLARE @PropCostDiamond BIGINT

DECLARE @STime NVARCHAR(10)
DECLARE @StartTime NVARCHAR(20)
DECLARE @EndTime NVARCHAR(20)

-- 执行逻辑
BEGIN
	SET @TodayTime = DATEADD(DAY,-1,GETDATE())
	SET @STime = Convert(CHAR(10),@TodayTime,120)
	SET @StartTime = @STime + N' 00:00:00'
	SET @EndTime = @STime + N' 23:59:59'

	-- 一级代理拥有钻石总量
	SELECT @FirstDiamond=ISNULL(SUM(Diamond),0) FROM UserCurrency WHERE UserID IN(SELECT UserID FROM WHJHAccountsDB.dbo.AccountsAgentInfo WHERE AgentLevel = 1)

	-- 二级代理拥有钻石总量
	SELECT @SecondDiamond=ISNULL(SUM(Diamond),0) FROM UserCurrency WHERE UserID IN(SELECT UserID FROM WHJHAccountsDB.dbo.AccountsAgentInfo WHERE AgentLevel = 2)

	-- 三级代理拥有钻石总量
	SELECT @ThirdDiamond=ISNULL(SUM(Diamond),0) FROM UserCurrency WHERE UserID IN(SELECT UserID FROM WHJHAccountsDB.dbo.AccountsAgentInfo WHERE AgentLevel = 3)

	-- 平台钻石总量
	SELECT @TotalDiamond=ISNULL(SUM(Diamond),0) FROM UserCurrency

	-- 充值钻石总量
	SELECT @PayDiamond=ISNULL(SUM(Diamond+OtherPresent),0) FROM OnLinePayOrder WITH(NOLOCK) WHERE OrderStatus = 1 AND PayDate BETWEEN @StartTime AND @EndTime

	-- 系统管理员赠送钻石总量
	SELECT @AdminPresentDiamond=ISNULL(SUM(AddDiamond),0) FROM WHJHRecordDB.dbo.RecordGrantDiamond WITH(NOLOCK) WHERE TypeID = 0 AND CollectDate BETWEEN @StartTime AND @EndTime

	-- 系统奖励赠送钻石总量
	SELECT @SysPresentDiamond=ISNULL(SUM(ChangeDiamond),0) FROM WHJHRecordDB.dbo.RecordDiamondSerial WITH(NOLOCK) WHERE TypeID IN(1,2,4,5,6) AND CollectDate BETWEEN @StartTime AND @EndTime

	-- 创建房间消耗总量
	SELECT @RoomCostDiamond=ISNULL(SUM(CreateTableFee),0) FROM WHJHPlatformDB.dbo.StreamCreateTableFeeInfo WITH(NOLOCK) WHERE CreateDate BETWEEN @StartTime AND @EndTime

	-- AA 制游戏消耗钻石
	SELECT @AAGameDiamond=ISNULL(SUM(Diamond),0) FROM WHJHRecordDB.dbo.RecordGameDiamond WITH(NOLOCK) WHERE CollectDate BETWEEN @StartTime AND @EndTime

	-- 购买道具消耗总量
	SELECT @PropCostDiamond=ISNULL(SUM(Diamond),0) FROM WHJHRecordDB.dbo.RecordBuyNewProperty WITH(NOLOCK) WHERE CollectDate BETWEEN @StartTime AND @EndTime

	-- 写入统计表
	SET @DateID = CAST(CAST(@TodayTime AS FLOAT) AS INT)
	INSERT INTO WHJHRecordDBLink.WHJHRecordDB.dbo.RecordEveryDayCurrency 
	VALUES(@DateID,@FirstDiamond,@SecondDiamond,@ThirdDiamond,@TotalDiamond,@SysPresentDiamond,@AdminPresentDiamond,@PayDiamond,@RoomCostDiamond,@PropCostDiamond,@AAGameDiamond,@TodayTime)

END
GO
