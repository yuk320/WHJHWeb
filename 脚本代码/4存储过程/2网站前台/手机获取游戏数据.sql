----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：手机端获取游戏列表和版本
----------------------------------------------------------------------------------------------------

USE WHJHPlatformDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_GetMobileGameAndVersion') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_GetMobileGameAndVersion
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_GetMobileGameAndVersion

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 执行逻辑
BEGIN
	-- 获取大厅版本配置
	SELECT Field1,Field2,Field3,Field4 FROM WHJHNativeWebDB.dbo.ConfigInfo WHERE ConfigKey=N'MobilePlatformVersion'

	-- 获取游戏列表
	SELECT * FROM MobileKindItem WHERE Nullity=0 ORDER BY SortID ASC,KindID DESC
	
END

RETURN 0

GO