----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：代理钻石查询
----------------------------------------------------------------------------------------------------

USE WHJHRecordDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_QueryAgentDiamond') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_QueryAgentDiamond
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_QueryAgentDiamond
	@dwUserID	INT						-- 用户 I D
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @Diamond BIGINT
DECLARE @InDiamond BIGINT
DECLARE @OutDiamond BIGINT
DECLARE @AgentDiamond BIGINT
DECLARE @UserDiamond BIGINT

-- 执行逻辑
BEGIN
	-- 查询身上钻石
	SELECT @Diamond=Diamond FROM WHJHTreasureDB.dbo.UserCurrency WHERE UserID = @dwUserID
	
	--查询转入钻石量
	SELECT @InDiamond=ISNULL(SUM(PresentDiamond),0) FROM RecordPresentCurrency WITH(NOLOCK) WHERE TargetUserID = @dwUserID

	--查询转出钻石量
	SELECT @OutDiamond=ISNULL(SUM(PresentDiamond),0) FROM RecordPresentCurrency WITH(NOLOCK) WHERE SourceUserID = @dwUserID

	--查询赠送下级代理总量
	SELECT @AgentDiamond=ISNULL(SUM(PresentDiamond),0) FROM RecordPresentCurrency WITH(NOLOCK) WHERE SourceUserID = @dwUserID AND TargetAgentLevel>0

	--查询赠送下级玩家总量
	SELECT @UserDiamond=ISNULL(SUM(PresentDiamond),0) FROM RecordPresentCurrency WITH(NOLOCK) WHERE SourceUserID = @dwUserID AND TargetAgentLevel=0
	
	--输入查询值
	SELECT @Diamond AS Diamond,@InDiamond AS InDiamond,@OutDiamond AS OutDiamond,@AgentDiamond AS AgentDiamond,@UserDiamond AS UserDiamond

END

RETURN 0

GO