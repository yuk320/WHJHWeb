----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：手机登录数据获取
----------------------------------------------------------------------------------------------------

USE WHJHNativeWebDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_GetMobileLoginData') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_GetMobileLoginData
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_GetMobileLoginData

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 执行逻辑
BEGIN
	
	-- 获取系统配置
	SELECT StatusName,StatusValue FROM WHJHAccountsDB.dbo.SystemStatusInfo WITH(NOLOCK)

	-- 获取客服配置
	SELECT Field1 AS Phone,Field2 AS WeiXin,Field3 AS QQ,Field4 AS Link FROM ConfigInfo WITH(NOLOCK) WHERE ConfigKey =N'SysCustomerService'

	-- 获取系统公告
	SELECT TOP 10 NoticeID,NoticeTitle,MoblieContent,PublisherTime FROM SystemNotice WITH(NOLOCK) WHERE Nullity=0 ORDER BY IsTop DESC,IsHot DESC,SortID ASC,NoticeID DESC

	-- 获取广告资源
	SELECT ResourceURL,LinkURL,SortID FROM Ads WITH(NOLOCK) WHERE [Type] = 3 ORDER BY SortID ASC
	SELECT ResourceURL,LinkURL,SortID FROM Ads WITH(NOLOCK) WHERE [Type] = 4 ORDER BY SortID ASC

END

RETURN 0

GO