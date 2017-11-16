USE [WHJHAccountsDB]
GO

DELETE DBO.SystemStatusInfo WHERE StatusName = N'IOSNotStorePaySwitch'
DELETE DBO.SystemStatusInfo WHERE StatusName = N'JJGoldBuyProp'
GO

USE [WHJHNativeWebDB]
GO

DELETE DBO.ConfigInfo WHERE ConfigKey = N'GameAndroidConfig'
DELETE DBO.ConfigInfo WHERE ConfigKey = N'GameIosConfig'

USE [WHJHTreasureDB]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPayConfig', @level2type=N'COLUMN',@level2name=N'PresentScale'
GO
ALTER TABLE [dbo].[AppPayConfig] DROP CONSTRAINT [DF_AppPayConfig_PresentScale]
GO
ALTER TABLE [dbo].[AppPayConfig] DROP COLUMN [PresentScale]
GO
ALTER TABLE [dbo].[AppPayConfig] ADD [PresentDiamond] INT NOT NULL DEFAULT(0)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'首冲赠送钻石数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPayConfig', @level2type=N'COLUMN',@level2name=N'PresentDiamond'
GO