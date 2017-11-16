----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：后台查询钻石分布
----------------------------------------------------------------------------------------------------

USE WHJHTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_GetDiamondDistribute') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_GetDiamondDistribute
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_GetDiamondDistribute

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 执行逻辑
BEGIN
	-- 获取钻石分布
	SELECT '1百以下' AS name, COUNT(UserID) AS value FROM UserCurrency WITH(NOLOCK) WHERE Diamond<100 UNION ALL 
	SELECT '1百~3百' AS name, COUNT(UserID) AS value FROM UserCurrency WITH(NOLOCK) WHERE Diamond>=100 AND Diamond<300 UNION ALL 
	SELECT '3百~5百' AS name, COUNT(UserID) AS value FROM UserCurrency WITH(NOLOCK) WHERE Diamond>=300 AND Diamond<500 UNION ALL 
	SELECT '5百~1千' AS name, COUNT(UserID) AS value FROM UserCurrency WITH(NOLOCK) WHERE Diamond>=500 AND Diamond<1000 UNION ALL 
	SELECT '1千~5千' AS name, COUNT(UserID) AS value FROM UserCurrency WITH(NOLOCK) WHERE Diamond>=1000 AND Diamond<5000 UNION ALL 
	SELECT '5千以上' AS name, COUNT(UserID) AS value FROM UserCurrency WITH(NOLOCK) WHERE Diamond>=5000

	-- 获取钻石总数
	SELECT ISNULL(SUM(Diamond),0) AS Diamond FROM UserCurrency WITH(NOLOCK)

END

RETURN 0

GO
