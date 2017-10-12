----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：获取排行版数据(每日的排行榜)
----------------------------------------------------------------------------------------------------

USE WHJHNativeWebDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_GetDayRankingData') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_GetDayRankingData
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_GetDayRankingData
	@TypeID INT
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

DECLARE @Yesterday DATETIME
DECLARE @DateID INT

-- 执行逻辑
BEGIN
	-- 获取时间
	SET @Yesterday = DATEADD(DAY,-1,GETDATE())
	SET @DateID = CAST(CAST(@Yesterday AS FLOAT) AS INT)

	-- 获取钻石排行
	IF @TypeID & 1 = 1
	BEGIN
		SELECT TOP 10 * FROM CacheWealthRank WITH(NOLOCK) WHERE DateID = @DateID ORDER BY RankNum ASC
	END
	-- 获取消耗排行
	IF @TypeID & 2 = 2
	BEGIN
		SELECT TOP 10 * FROM CacheConsumeRank WITH(NOLOCK) WHERE DateID = @DateID ORDER BY RankNum ASC
	END
	-- 获取战绩排行
	IF @TypeID & 4 = 4
	BEGIN
		SELECT TOP 10 * FROM CacheScoreRank WITH(NOLOCK) WHERE DateID = @DateID ORDER BY RankNum ASC
	END
	
END

RETURN 0

GO