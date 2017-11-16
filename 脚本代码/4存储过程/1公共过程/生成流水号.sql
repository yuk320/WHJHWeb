-- =============================================
-- 用途: 生成随机数（每个库均执行）
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE name = N'WV_GetRandom')
DROP VIEW WV_GetRandom
GO 
-----------------------------------------------------------------
CREATE VIEW WV_GetRandom
WITH ENCRYPTION AS
SELECT CAST(FLOOR(90000*RAND()+10000) AS VARCHAR(5)) AS Random
GO

-- =============================================
-- 用途: 生成唯一流水号（每个库均执行）
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WF_GetSerialNumber]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WF_GetSerialNumber]
GO
-----------------------------------------------------------------
CREATE FUNCTION [dbo].[WF_GetSerialNumber] 
(
)
RETURNS NVARCHAR(20)
WITH ENCRYPTION AS
BEGIN
	DECLARE @DateTime DATETIME
	DECLARE @DateDay VARCHAR(5)
	DECLARE @DateRemain VARCHAR(9)
	DECLARE @RandNumber VARCHAR(5)

	SET @DateTime=GETDATE()
	SET @DateDay = CAST(CAST(@DateTime AS FLOAT) AS INT)
	SET @DateRemain = REPLACE(CONVERT(VARCHAR(12),@DateTime,114),':','')
	SELECT @RandNumber=Random FROM WV_GetRandom

	RETURN (@RandNumber + @DateDay + @DateRemain)
END
GO