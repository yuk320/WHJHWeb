----------------------------------------------------------------------
-- 时间：2011-10-20
-- 用途：数据汇总统计。
----------------------------------------------------------------------
USE WHJHTreasureDB
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PM_StatInfo') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PM_StatInfo
GO

----------------------------------------------------------------------
CREATE PROC NET_PM_StatInfo
			
WITH ENCRYPTION AS

BEGIN
	-- 属性设置
	SET NOCOUNT ON;	
	--用户统计
	DECLARE @OnLineCount BIGINT		--在线人数
	DECLARE @DisenableCount BIGINT		--停权用户
	DECLARE @AllCount BIGINT			--注册总人数
	DECLARE @MobileRegister BIGINT		--手机注册总人数
	SELECT  TOP 1 @OnLineCount=ISNULL(OnLineCountSum,0) FROM WHJHPlatformDBLink.WHJHPlatformDB.dbo.OnLineStreamInfo ORDER BY InsertDateTime DESC
	SELECT  @DisenableCount=COUNT(UserID) FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsInfo WHERE Nullity = 1
	SELECT  @AllCount=COUNT(UserID) FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsInfo
	SELECT @MobileRegister=COUNT(UserID) FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsInfo WHERE RegisterOrigin>10 AND RegisterOrigin<80

	--金币统计
	DECLARE @Score BIGINT		--身上金币总量
	DECLARE @InsureScore BIGINT	--银行总量
	DECLARE @AllScore BIGINT
	SELECT  @Score=ISNULL(SUM(Score),0),@InsureScore=ISNULL(SUM(InsureScore),0),@AllScore=ISNULL(SUM(Score+InsureScore),0) 
	FROM GameScoreInfo(NOLOCK)

	--钻石统计
	DECLARE @FKAdminPresent BIGINT	--后台赠送钻石
	DECLARE @FKCreateRoom BIGINT	--创建房间消耗钻石
	DECLARE @FKAACreateRoom BIGINT	--创建AA房间消耗钻石
	DECLARE @FKExchScore BIGINT		--兑换游戏币消耗钻石
	DECLARE @FKRMBPay BIGINT		--充值钻石
	DECLARE @FKTotal BIGINT			--平台钻石总数
	SELECT @FKTotal = ISNULL(SUM(Diamond),0) FROM UserCurrency(NOLOCK)
	SELECT @FKAdminPresent = ISNULL(SUM(AddDiamond),0) FROM WHJHRecordDBLink.WHJHRecordDB.dbo.RecordGrantDiamond
	SELECT @FKExchScore = ISNULL(SUM(ExchDiamond),0) FROM WHJHRecordDBLink.WHJHRecordDB.dbo.RecordCurrencyExch
	SELECT @FKCreateRoom = ISNULL(SUM(CreateTableFee),0) FROM WHJHPlatformDBLink.WHJHPlatformDB.dbo.StreamCreateTableFeeInfo WHERE PayMode=0
	SELECT @FKAACreateRoom = ISNULL(SUM(Diamond),0) FROM WHJHRecordDBLink.WHJHRecordDB.dbo.RecordGameDiamond WHERE TypeID = 1
	SELECT @FKRMBPay = ISNULL(SUM(Diamond),0) FROM OnLinePayOrder WHERE Diamond > 0 
	
	--赠送统计
	DECLARE @RegPresent BIGINT				--注册赠送(1)
	DECLARE @AgentRegPresent BIGINT			--代理注册赠送(13)
	DECLARE @DBPresent BIGINT				--低保赠送(2)
	DECLARE @QDPresent BIGINT				--签到赠送(3)
	DECLARE @YBPresent BIGINT				--元宝兑换(4)
	DECLARE @MLPresent BIGINT				--魅力兑换(5)
	DECLARE @OnlinePresent BIGINT			--在线时长赠送(6)
	DECLARE @RWPresent BIGINT				--任务奖励(7)
	DECLARE @SMPresent BIGINT				--实名验证(8)
	DECLARE @DayPresent BIGINT				--会员每日送金(9)
	DECLARE @MatchPresent BIGINT			--比赛奖励(10)
	DECLARE @DJPresent BIGINT				--等级升级(11)
	DECLARE @SharePresent BIGINT			--分享赠送(12)
	DECLARE @LotteryPresent BIGINT			--转盘赠送(14)
	DECLARE @WebPresent BIGINT				--后台赠送
	SELECT @RegPresent=ISNULL(SUM(CONVERT(BIGINT,[PresentScore])),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=1
	SELECT @AgentRegPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=13
	SELECT @DBPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=2
	SELECT @QDPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=3
	SELECT @YBPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=4
	SELECT @MLPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=5
	SELECT @OnlinePresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=6
	SELECT @RWPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=7
	SELECT @SMPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=8
	SELECT @DayPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=9
	SELECT @MatchPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=10
	SELECT @DJPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=11
	SELECT @SharePresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=12
	--SELECT @LotteryPresent=ISNULL(SUM([PresentScore]),0) FROM [dbo].[StreamPresentInfo](NOLOCK) WHERE [TypeID]=14
	SELECT @LotteryPresent=ISNULL(SUM([ItemQuota]),0) FROM WHJHRecordDBLink.WHJHRecordDB.DBO.RecordLottery WHERE ItemType=0
	SELECT @WebPresent=ISNULL(SUM(CONVERT(BIGINT,AddGold)),0) FROM WHJHRecordDBLink.WHJHRecordDB.dbo.RecordGrantTreasure
	
	--税收统计
	DECLARE @Revenue BIGINT			--税收总量
	DECLARE @TransferRevenue BIGINT	--转账税收
	SELECT @Revenue=ISNULL(SUM(Revenue),0) FROM GameScoreInfo(NOLOCK)
	SELECT @TransferRevenue=ISNULL(SUM(Revenue),0) FROM RecordInsure(NOLOCK)

	--损耗统计
	DECLARE @Waste BIGINT   --损耗总量
	SELECT @Waste=ISNULL(SUM(Waste),0) FROM WHJHRecordDBLink.WHJHRecordDB.dbo.RecordEveryDayData

	--返回
	SELECT  @OnLineCount AS	OnLineCount,				--在线人数
			@DisenableCount AS DisenableCount,			--停权用户
			@AllCount AS AllCount,						--注册总人数
			@MobileRegister AS MobileRegister,			--手机端注册总人数
			@Score AS Score,							--身上金币总量
			@InsureScore AS InsureScore,				--银行总量
			@AllScore AS AllScore,						--金币总数

			@FKAdminPresent AS FKAdminPresent,			--后台赠送钻石
			@FKCreateRoom AS FKCreateRoom,				--创建房间消耗钻石
			@FKAACreateRoom AS FKAACreateRoom,			--创建AA房间消耗钻石
			@FKExchScore AS FKExchScore,				--兑换游戏币消耗钻石
			@FKRMBPay AS FKRMBPay,						--人民币购买钻石
			@FKTotal AS FKTotal,						--平台钻石总数

			@RegPresent AS RegPresent,					--注册赠送
			@AgentRegPresent AS AgentRegPresent,		--代理注册赠送
			@DBPresent AS DBPresent,					--低保赠送
			@QDPresent AS QDPresent,					--签到赠送
			@YBPresent AS YBPresent,					--元宝兑换
			@MLPresent AS MLPresent,					--魅力兑换
			@OnlinePresent AS OnlinePresent,			--在线时长赠送
			@RWPresent AS RWPresent,					--任务奖励
			@SMPresent AS SMPresent,					--实名验证
			@DayPresent AS DayPresent,					--会员每日送金
			@MatchPresent AS MatchPresent,				--比赛奖励
			@DJPresent AS DJPresent,					--等级升级
			@SharePresent AS SharePresent,				--分享赠送
			@LotteryPresent AS LotteryPresent,			--转盘赠送
			@WebPresent AS WebPresent,					--后台赠送

			@Revenue AS Revenue,						--税收总量
			@TransferRevenue AS TransferRevenue,		--转账税收	
			@Waste AS Waste								--损耗总量
END































