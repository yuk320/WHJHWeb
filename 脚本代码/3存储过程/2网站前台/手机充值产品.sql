----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：手机端获取充值产品
----------------------------------------------------------------------------------------------------

USE WHJHTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_GetMobilePayConfig') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_GetMobilePayConfig
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_GetMobilePayConfig
	@dwUserID INT,
	@PayType INT
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

DECLARE @NowTime NVARCHAR(20)
DECLARE @StartTime NVARCHAR(20)
DECLARE @EndTime NVARCHAR(20)

-- 执行逻辑
BEGIN
	-- 获取查询时间
	SET @NowTime = CONVERT(VARCHAR(10),GETDATE(),120) 
	SET @StartTime = @NowTime + N' 00:00:00'
	SET @EndTime = @NowTime + N' 23:59:59'

	-- 获取首充
	SELECT OnLineID FROM OnLinePayOrder WITH(NOLOCK) WHERE UserID=@dwUserID AND OrderStatus=1 AND OrderDate BETWEEN @StartTime AND @EndTime

	-- 获取充值产品
	SELECT ConfigID,AppleID,PayName,PayType,PayPrice,PayIdentity,ImageType,SortID,Diamond,PresentScale FROM AppPayConfig WITH(NOLOCK) WHERE PayType = @PayType ORDER BY PayIdentity DESC,SortID ASC

	-- 获取兑换产品
	SELECT ConfigID,ConfigName,Diamond,ExchGold,ImageType,SortID,ConfigTime FROM CurrencyExchConfig WITH(NOLOCK) ORDER BY SortID ASC

END

RETURN 0

GO