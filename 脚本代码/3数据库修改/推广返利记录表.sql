USE [WHJHRecordDB]
GO

IF EXISTS (SELECT 1 FROM [DBO].SYSObjects WHERE ID = OBJECT_ID(N'[dbo].[RecordSpreadReturn]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
	DROP TABLE [dbo].[RecordSpreadReturn]
END

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RecordSpreadReturn](
	[RecordID] [int] IDENTITY(1,1) NOT NULL, -- 记录标识
	[SourceUserID] [int] NOT NULL,  -- 充值对象
	[TargetUserID] [int] NOT NULL,	-- 返利对象
	[SourceDiamond] [int] NOT NULL,  -- 充值所得钻石
	[SpreadLevel] [int] NOT NULL, -- 返利配置推广级别
	[ReturnScale] [decimal](18, 6) NOT NULL, -- 返利比例
	[ReturnNum] [int] NOT NULL,	-- 返利数值 （根据ReturnType 0：金币 1：钻石）
	[ReturnType] [tinyint] NOT NULL, -- 返利类型 0：金币 1：钻石
	[CollectDate] [datetime] NOT NULL, -- 记录日期
 CONSTRAINT [PK_RecordSpreadReturn] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_SourceUserID]  DEFAULT ((0)) FOR [SourceUserID]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_TargetUserID]  DEFAULT ((0)) FOR [TargetUserID]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_SourceDiamond]  DEFAULT ((0)) FOR [SourceDiamond]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_SpreadLevel]  DEFAULT ((0)) FOR [SpreadLevel]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_ReturnScale]  DEFAULT ((0)) FOR [ReturnScale]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_ReturnNum]  DEFAULT ((0)) FOR [ReturnNum]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_ReturnType]  DEFAULT ((0)) FOR [ReturnType]
GO
ALTER TABLE [dbo].[RecordSpreadReturn] ADD  CONSTRAINT [DF_RecordSpreadReturn_CollectDate]  DEFAULT (getdate()) FOR [CollectDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'RecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'SourceUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'TargetUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'充值所得钻石' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'SourceDiamond'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利配置推广级别（目前仅支持3级）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'SpreadLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'ReturnScale'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利数值 （根据ReturnType 0：金币 1：钻石）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'ReturnNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利类型（0：金币、1：钻石）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'ReturnType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordSpreadReturn', @level2type=N'COLUMN',@level2name=N'CollectDate'
GO