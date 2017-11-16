USE [WHJHTreasureDB]
GO

IF EXISTS (SELECT 1 FROM [DBO].SYSObjects WHERE ID = OBJECT_ID(N'[dbo].[SpreadReturnConfig]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
	DROP TABLE [dbo].[SpreadReturnConfig]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SpreadReturnConfig](
	[ConfigID] [int] IDENTITY(1,1) NOT NULL,
	[SpreadLevel] [int] NOT NULL,
	[PresentScale] [decimal](18, 6) NOT NULL,
	[Nullity] [bit] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SpreadReturnConfig] PRIMARY KEY CLUSTERED 
(
	[ConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_SpreadLevel]  DEFAULT ((0)) FOR [SpreadLevel]
GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_PresentScale]  DEFAULT ((0)) FOR [PresentScale]
GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_Nullity]  DEFAULT ((0)) FOR [Nullity]
GO

ALTER TABLE [dbo].[SpreadReturnConfig] ADD  CONSTRAINT [DF_SpreadReturnConfig_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'ConfigID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推广级别（目前仅支持3级）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'SpreadLevel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值返点比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'PresentScale'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用（0：启用、1：禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'Nullity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpreadReturnConfig', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO