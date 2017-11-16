----------------------------------------------------------------------
-- 时间：2015-10-10
-- 用途：代理商添加下线
----------------------------------------------------------------------
USE WHJHAccountsDB
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PM_AddAgentSpreadUser]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PM_AddAgentSpreadUser]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PM_AddAgentSpreadUser]
(
	@dwUserID			INT,					--用户标识
	@dwGameID			INT,					--游戏ID

	@strErrorDescribe NVARCHAR(127) OUTPUT		--输出信息
)

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @AgentID INT
DECLARE @AgentNullity TINYINT

DECLARE @SpreaderID INT
DECLARE @UAgentID INT
DECLARE @UserIDStr NVARCHAR(2000)

BEGIN
	-- 查询用户	
	SELECT @UserID=UserID,@Nullity=Nullity,@AgentID=AgentID FROM AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
	-- 用户存在
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号不存在！'
		RETURN 1001
	END	
	-- 帐号禁止
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已冻结！'
		RETURN 1002
	END	
	IF @AgentID<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的账号为非代理商！'
		RETURN 1003
	END

	-- 查询代理信息
	SELECT @AgentNullity=Nullity FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID=@AgentID
	IF @AgentNullity IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的账号为非代理商！'
		RETURN 1003
	END
	IF @AgentNullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的代理账号已冻结！'
		RETURN 1004
	END
	
	-- 查询下线用户
	SET @UserIDStr = ''
	SELECT @SpreaderID=SpreaderID,@UAgentID=AgentID FROM AccountsInfo WITH(NOLOCK) WHERE GameID=@dwGameID
	IF @UAgentID>0
	BEGIN
		SET @strErrorDescribe=N'抱歉，您添加的下线已绑定代理商！'
		RETURN 1005
	END
	WHILE @SpreaderID>0
	BEGIN
		SET @UserIDStr = @UserIDStr + CAST(@SpreaderID AS NVARCHAR(10)) + ','
		SELECT @SpreaderID=SpreaderID,@UAgentID=AgentID FROM AccountsInfo WITH(NOLOCK) WHERE UserID = @SpreaderID
		IF @UAgentID>0
		BEGIN
			SET @SpreaderID = 0
			SET @strErrorDescribe=N'抱歉，您添加的下线已绑定代理商！'
			RETURN 1005
		END
		IF CHARINDEX(CAST(@SpreaderID AS NVARCHAR(10)),@UserIDStr)>0
		BEGIN
			SET @SpreaderID = 0
		END
	END

	-- 绑定下线
	UPDATE AccountsInfo SET SpreaderID=@UserID WHERE GameID=@dwGameID
	IF @@ROWCOUNT>0
	BEGIN
		SET @strErrorDescribe=N'恭喜您，下线绑定成功！'
		RETURN 0
	END
	ELSE
	BEGIN
		SET @strErrorDescribe=N'抱歉，下线绑定失败！'
		RETURN 2005
	END
END
GO