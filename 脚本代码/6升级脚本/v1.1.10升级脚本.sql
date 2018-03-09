USE [WHJHNativeWebDB]
GO

-- v1.1.10 更新站点配置，中网站站点配置的说明及字段。
UPDATE ConfigInfo SET ConfigString = N'参数说明
字段1：网站二维码地址
字段2：网站图片服务器地址
字段3：网站前台服务器地址
字段4：H5游戏服务器地址
字段5：代理网站域名或IP
字段8：网站前台底部内容',Field5 = N'/Card'
