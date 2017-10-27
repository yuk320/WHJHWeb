USE [WHJHAccountsDB]
GO

DELETE DBO.SystemStatusInfo WHERE StatusName = N'IOSNotStorePaySwitch'
DELETE DBO.SystemStatusInfo WHERE StatusName = N'JJGoldBuyProp'
GO

USE [WHJHNativeWebDB]
GO

DELETE DBO.ConfigInfo WHERE ConfigKey = N'GameAndroidConfig'
DELETE DBO.ConfigInfo WHERE ConfigKey = N'GameIosConfig'