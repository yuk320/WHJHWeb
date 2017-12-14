USE WHJHPlatformManagerDB

-- 模块表
TRUNCATE TABLE [dbo].[Base_Module]

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (1, 0, N'用户系统', N'', 1, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (2, 0, N'充值系统', N'', 2, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (3, 0, N'维护系统', N'', 3, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (4, 0, N'网站系统', N'', 4, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (5, 0, N'金币系统', N'', 5, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (6, 0, N'钻石系统', N'', 6, 0, 1, N'',0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (7, 0, N'统计系统', N'', 7, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [OrderNo], [Nullity], [IsMenu], [Description], [ManagerPopedom]) VALUES (8, 0, N'后台系统', N'', 8, 0, 1, N'', 0)

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (100, 1, N'用户管理', N'/Module/AccountManager/AccountsList.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom])
                    VALUES (101, 1, N'金币在线玩家', N'/Module/AccountManager/UserPlaying.aspx', 0, 0, 2, N'', 0)
-- INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (102, 1, N'限制管理', N'/Module/AccountManager/ConfineAddressList.aspx', 0, 0, 4, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (103, 1, N'代理管理', N'/Module/AgentManager/AgentUserList.aspx', 0, 0, 1, N'', 0)

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (200, 2, N'充值配置', N'/Module/FilledManager/AppPayConfigList.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (201, 2, N'充值记录', N'/Module/FilledManager/RecordPayDiamond.aspx', 0, 0, 2, N'', 0)

-- INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (300, 3, N'机器管理', N'/Module/AppManager/DataBaseInfoList.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (301, 3, N'游戏管理', N'/Module/AppManager/GameGameItemList.aspx', 0, 0, 2, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (302, 3, N'系统消息', N'/Module/AppManager/SystemMessageList.aspx', 0, 0, 3, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (303, 3, N'系统设置', N'/Module/AppManager/SystemSet.aspx', 0, 0, 4, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (304, 3, N'推广管理', N'/Module/AppManager/SpreadConfigList.aspx', 0, 0, 5, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (305, 3, N'排行管理', N'/Module/AppManager/RankingConfigList.aspx', 0, 0, 6, N'', 0)

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (400, 4, N'站点配置', N'/Module/WebManager/LogoSet.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (401, 4, N'游戏规则', N'/Module/WebManager/KindRuleList.aspx', 0, 0, 2, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (402, 4, N'广告管理', N'/Module/WebManager/AdsList.aspx', 0, 0, 3, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (403, 4, N'新闻公告', N'/Module/WebManager/SystemNoticeList.aspx', 0, 0, 4, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (404, 4, N'消息推送', N'/Module/WebManager/UMessagePushList.aspx', 0, 0, 5, N'', 0)

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (500, 5, N'金币管理', N'/Module/GoldManager/AccountsGoldList.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (501, 5, N'兑换管理', N'/Module/GoldManager/GoldExchConfigList.aspx', 0, 0, 2, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (502, 5, N'兑换记录', N'/Module/GoldManager/RecordGoldExch.aspx', 0, 0, 3, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (503, 5, N'银行记录', N'/Module/GoldManager/RecordBankTrade.aspx', 0, 0, 4, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (504, 5, N'进出记录', N'/Module/GoldManager/RecordGameInOut.aspx', 0, 0, 5, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (505, 5, N'游戏记录', N'/Module/GoldManager/RecordUserGame.aspx', 0, 0, 6, N'', 0)

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (600, 6, N'用户钻石管理', N'/Module/Diamond/DiamondList.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (601, 6, N'购买喇叭记录', N'/Module/Diamond/RecordBuyHorn.aspx', 0, 0, 2, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (602, 6, N'赠送交易记录', N'/Module/Diamond/RecordPresentTrade.aspx', 0, 0, 3, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (603, 6, N'后台赠送记录', N'/Module/Diamond/RecordSysPresent.aspx', 0, 0, 4, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (604, 6, N'钻石开房记录', N'/Module/Diamond/RecordOpenRoom.aspx', 0, 0, 5, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (605, 6, N'兑换金币记录', N'/Module/Diamond/RecordDiamondExch.aspx', 0, 0, 6, N'', 0)

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (700, 7, N'用户注册统计', N'/Module/DataStatistics/UserRegister.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (701, 7, N'用户在线统计', N'/Module/DataStatistics/UserOnline.aspx', 0, 0, 2, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (702, 7, N'用户钻石统计', N'/Module/Diamond/StatisticsDiamond.aspx', 0, 0, 3, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (703, 7, N'每日税收统计', N'/Module/DataStatistics/DailyRevenue.aspx', 0, 0, 4, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (704, 7, N'每日损耗统计', N'/Module/DataStatistics/DailyWaste.aspx', 0, 0, 5, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (705, 7, N'财富分布统计', N'/Module/DataStatistics/WealthDistribute.aspx', 0, 0, 6, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (706, 7, N'系统全局统计', N'/Module/DataStatistics/SystemStat.aspx', 0, 0, 0, N'', 0)

INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (800, 8, N'账号管理', N'/Module/BackManager/BaseRoleList.aspx', 0, 0, 1, N'', 0)
INSERT [dbo].[Base_Module] ([ModuleID], [ParentID], [Title], [Link], [IsMenu], [Nullity], [OrderNo], [Description], [ManagerPopedom]) VALUES (801, 8, N'安全日志', N'/Module/OperationLog/SystemSecurityList.aspx', 0, 0, 2, N'', 0)




-- 模板权限表
TRUNCATE TABLE Base_ModulePermission

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (100, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (100, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (100, N'赠送靓号', 256, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (100, N'冻/解', 8192, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (100, N'设置权限/取消权限', 524288, 0, 0, 1)

-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (102, N'查看', 1, 0, 0, 1)
-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (102, N'添加', 2, 0, 0, 1)
-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (102, N'编辑', 4, 0, 0, 1)
-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (102, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (103, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (103, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (103, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (103, N'冻/解', 8192, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (103, N'赠送钻石', 262144, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (200, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (200, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (200, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (200, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (201, N'查看', 1, 0, 0, 1)

-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (300, N'查看', 1, 0, 0, 1)
-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (300, N'添加', 2, 0, 0, 1)
-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (300, N'编辑', 4, 0, 0, 1)
-- INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (300, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (301, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (301, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (301, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (301, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (302, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (302, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (302, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (302, N'删除', 8, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (302, N'冻/解', 8192, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (303, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (303, N'编辑', 4, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (304, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (304, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (304, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (304, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (305, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (305, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (305, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (305, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (400, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (400, N'编辑', 4, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (401, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (401, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (401, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (401, N'删除', 8, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (401, N'冻/解', 8192, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (402, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (402, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (402, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (402, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (403, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (403, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (403, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (403, N'删除', 8, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (404, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (404, N'编辑', 4, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (500, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (500, N'赠送金币', 32, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (501, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (502, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (503, N'查看', 1, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (504, N'查看', 1, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (505, N'查看', 1, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (600, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (600, N'赠送钻石', 262144, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (601, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (602, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (603, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (604, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (605, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (606, N'查看', 1, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (700, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (701, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (702, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (703, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (704, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (705, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (706, N'查看', 1, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (800, N'查看', 1, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (800, N'添加', 2, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (800, N'编辑', 4, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (800, N'删除', 8, 0, 0, 1)
INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (800, N'冻/解', 8192, 0, 0, 1)

INSERT [dbo].[Base_ModulePermission] ([ModuleID], [PermissionTitle], [PermissionValue], [Nullity], [StateFlag], [ParentID]) VALUES (801, N'查看', 1, 0, 0, 1)

-- 用户表
TRUNCATE TABLE Base_Users

INSERT INTO Base_Users(Username,Password,RoleID) VALUES('admin','E10ADC3949BA59ABBE56E057F20F883E',1)

-- 角色表
TRUNCATE TABLE Base_Roles

INSERT INTO Base_Roles(RoleName,Description) VALUES('超级管理员','超级管理员')


