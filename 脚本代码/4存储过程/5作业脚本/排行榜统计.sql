----------------------------------------------------------------------
-- 版本：2013
-- 时间：2013-04-22
-- 用途：每日整点统计排行榜
----------------------------------------------------------------------
USE [WHJHAccountsDB]
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PJ_RankingStatistics]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PJ_RankingStatistics]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PJ_RankingStatistics]

WITH ENCRYPTION AS

-- 属性设置

DECLARE @Yesterday DATETIME
DECLARE @StartTime NVARCHAR(20)
DECLARE @EndTime NVARCHAR(20)
DECLARE @STime NVARCHAR(10)
DECLARE @DateID INT
-- 执行逻辑
BEGIN

	-- 获取前一天时间
	SET @Yesterday = DATEADD(DAY,-1,GETDATE())
	SET @DateID = CAST(CAST(@Yesterday AS FLOAT) AS INT)
	SET @STime = Convert(CHAR(10),@Yesterday,120)
	SET @StartTime = @STime + N' 00:00:00'
	SET @EndTime = @STime + N' 23:59:59'

  DELETE WHJHNativeWebDB.dbo.CacheWealthRank WHERE DateID = @DateID
	-- 写入财富排行榜 -- 2018/1/16 改为统计身上金币
	SELECT * INTO #WealthUser FROM (SELECT TOP 10 ROW_NUMBER() OVER(ORDER BY Diamond DESC,UserID ASC) AS RankNum,* FROM (SELECT A.UserID,A.GameID,A.NickName,A.CustomID,A.FaceID,U.Score AS Diamond FROM WHJHTreasureDB.dbo.GameScoreInfo AS U LEFT JOIN AccountsInfo AS A ON U.UserID = A.UserID AND A.Nullity=0 AND A.IsAndroid=0 AND U.Score>0) AS AU WHERE AU.UserID IS NOT NULL) AS A
	INSERT INTO WHJHNativeWebDB.dbo.CacheWealthRank SELECT @DateID AS DateID,W.UserID,W.GameID,W.NickName,ISNULL(F.FaceUrl,''),W.FaceID,W.RankNum,W.Diamond,@Yesterday AS CollectDate FROM #WealthUser AS W LEFT JOIN AccountsFace AS F ON W.CustomID=F.ID

	DELETE WHJHNativeWebDB.dbo.CacheConsumeRank WHERE DateID = @DateID
	-- 写入消耗排行榜
	SELECT * INTO #CostUser FROM (SELECT TOP 10 ROW_NUMBER() OVER(ORDER BY SUM(CreateTableFee) DESC,UserID ASC) AS RankNum,UserID,SUM(CreateTableFee) AS TotalFee FROM WHJHPlatformDB.dbo.StreamCreateTableFeeInfo WHERE CreateDate BETWEEN @StartTime AND @EndTime AND DissumeDate IS NOT NULL GROUP BY UserID ORDER BY SUM(CreateTableFee) DESC) AS S
	INSERT INTO WHJHNativeWebDB.dbo.CacheConsumeRank SELECT @DateID AS DateID,C.UserID,A.GameID,A.NickName,ISNULL(F.FaceUrl,''),A.FaceID,C.RankNum,C.TotalFee,@Yesterday AS CollectDate FROM #CostUser AS C LEFT JOIN AccountsInfo AS A ON C.UserID=A.UserID LEFT JOIN AccountsFace AS F ON A.CustomID=F.ID

  DELETE WHJHNativeWebDB.dbo.CacheScoreRank WHERE DateID = @DateID
	-- 写入战绩排行榜
	SELECT * INTO #ScoreUser FROM (SELECT TOP 10 ROW_NUMBER() OVER(ORDER BY SUM(Score) DESC,UserID ASC) AS RankNum,UserID,SUM(Score) AS TotalScore FROM WHJHPlatformDB.dbo.PersonalRoomScoreInfo WHERE Score>0 AND WriteTime BETWEEN @StartTime AND @EndTime GROUP BY UserID ORDER BY SUM(Score) DESC) AS S
	INSERT INTO WHJHNativeWebDB.dbo.CacheScoreRank SELECT @DateID AS DateID,C.UserID,A.GameID,A.NickName,ISNULL(F.FaceUrl,''),A.FaceID,C.RankNum,C.TotalScore,@Yesterday AS CollectDate FROM #ScoreUser AS C LEFT JOIN AccountsInfo AS A ON C.UserID=A.UserID LEFT JOIN AccountsFace AS F ON A.CustomID=F.ID

	-- 清理临时表
	DROP TABLE #WealthUser
	DROP TABLE #CostUser
	DROP TABLE #ScoreUser
END
GO

