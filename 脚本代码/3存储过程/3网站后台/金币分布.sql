----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：后台查询金币分布
----------------------------------------------------------------------------------------------------

USE WHJHTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_GetGoldDistribute') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_GetGoldDistribute
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_GetGoldDistribute

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 执行逻辑
BEGIN
	-- 获取金币分布
	SELECT '1万以下' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)<10000 UNION ALL 
	SELECT '1万~10万' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)>=10000 AND (Score+InsureScore)<100000 UNION ALL 
	SELECT '10万~50万' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)>=100000 AND (Score+InsureScore)<500000 UNION ALL 
	SELECT '50万~100万' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)>=500000 AND (Score+InsureScore)<1000000 UNION ALL 
	SELECT '100万~500万' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)>=1000000 AND (Score+InsureScore)<5000000 UNION ALL 
	SELECT '500万~1000万' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)>=5000000 AND (Score+InsureScore)<10000000 UNION ALL 
	SELECT '1000万~5000万' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)>=10000000 AND (Score+InsureScore)<50000000 UNION ALL 
	SELECT '5000万以上' AS name, COUNT(UserID) AS value FROM GameScoreInfo WITH(NOLOCK) WHERE (Score+InsureScore)>=50000000

	-- 获取金币总数
	SELECT ISNULL(SUM(Score),0) AS Score,ISNULL(SUM(InsureScore),0) AS InsureScore FROM GameScoreInfo WITH(NOLOCK)

END

RETURN 0

GO
