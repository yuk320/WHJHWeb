----------------------------------------------------------------------
-- 时间：2015-10-10
-- 用途：后台管理员添加代理用户
----------------------------------------------------------------------
USE WHJHAccountsDB
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PM_AddAgentUser]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PM_AddAgentUser]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PM_AddAgentUser]
(
	@dwUserID			INT,					--用户标识
	@strCompellation	NVARCHAR(16),			--代理姓名
	@strAgentDomain		NVARCHAR(50),			--代理域名
	@strQQAccount       NVARCHAR(32),			--Q Q 账号
	@strWCNickName      NVARCHAR(32),			--微信昵称
	@strContactPhone    NVARCHAR(32),			--联系电话
	@strContactAddress  NVARCHAR(32),			--联系地址
	@dwAgentLevel       NVARCHAR(32),			--代理等级
	@strAgentNote       NVARCHAR(32),			--代理备注
	@dwParentGameID		INT,					--父级代理

	@strErrorDescribe NVARCHAR(127) OUTPUT		--输出信息
)

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @UserID INT
DECLARE @Nullity TINYINT
DECLARE @AgentID INT
DECLARE @ParentAgentID INT
DECLARE @PParentAgentID INT
DECLARE @NewAgentID INT

BEGIN
	-- 查询用户	
	SELECT @UserID=UserID,@Nullity=Nullity FROM AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID

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

	-- 查询代理重复信息
	IF EXISTS(SELECT AgentID FROM AccountsAgentInfo WITH(NOLOCK) WHERE UserID=@dwUserID)
	BEGIN
		SET @strErrorDescribe=N'抱歉，添加账号已是代理账号！'
		RETURN 1003
	END

	-- 查询代理域名重复信息
	IF EXISTS(SELECT AgentID FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentDomain=@strAgentDomain)
	BEGIN
		SET @strErrorDescribe=N'抱歉，代理域名已存在！'
		RETURN 1004
	END

	SET @AgentID = 0
	IF @dwAgentLevel<1 AND @dwAgentLevel>3
	BEGIN
		SET @strErrorDescribe=N'抱歉，暂时只支持三级代理！'
		RETURN 1005
	END
	IF @dwAgentLevel=1
	BEGIN
		IF @dwParentGameID>0
		BEGIN
			SET @strErrorDescribe=N'抱歉，一级代理无法添加上线！'
			RETURN 1006
		END
	END
	IF @dwAgentLevel=2
	BEGIN
		IF @dwParentGameID=0
		BEGIN
			SET @strErrorDescribe=N'抱歉，二级代理需指定上一级代理！'
			RETURN 1007
		END
		SELECT @AgentID=AgentID FROM AccountsInfo WITH(NOLOCK) WHERE GameID=@dwParentGameID
		IF @AgentID <=0
		BEGIN
			SET @strErrorDescribe=N'抱歉，指定账号为非代理！'
			RETURN 1008
		END
		SELECT @ParentAgentID=ParentAgent FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID=@AgentID
		IF @ParentAgentID!=0
		BEGIN
			SET @strErrorDescribe=N'抱歉，二级代理需指定一级代理！'
			RETURN 1009
		END
	END
	IF @dwAgentLevel=3
	BEGIN
		IF @dwParentGameID=0
		BEGIN
			SET @strErrorDescribe=N'抱歉，三级代理需指定二级代理！'
			RETURN 2001
		END
		SELECT @AgentID=AgentID FROM AccountsInfo WITH(NOLOCK) WHERE GameID=@dwParentGameID
		IF @AgentID <=0
		BEGIN
			SET @strErrorDescribe=N'抱歉，指定账号为非代理！'
			RETURN 2002
		END
		SELECT @ParentAgentID=ParentAgent FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID=@AgentID
		IF @ParentAgentID=0
		BEGIN
			SET @strErrorDescribe=N'抱歉，三级代理需指定二级代理！'
			RETURN 2003
		END
		SELECT @PParentAgentID=ParentAgent FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID=@ParentAgentID
		IF @PParentAgentID!=0
		BEGIN
			SET @strErrorDescribe=N'抱歉，三级代理需指定二级代理！'
			RETURN 2004
		END
	END

	-- 代理信息
	INSERT INTO AccountsAgentInfo(ParentAgent,UserID,Compellation,QQAccount,WCNickName,ContactPhone,ContactAddress,AgentDomain,AgentLevel,AgentNote,Nullity,CollectDate)
	VALUES(@AgentID,@dwUserID,@strCompellation,@strQQAccount,@strWCNickName,@strContactPhone,@strContactAddress,@strAgentDomain,@dwAgentLevel,@strAgentNote,0,getdate())
	
	SELECT @NewAgentID = SCOPE_IDENTITY()

	-- 设置用户
	IF @@ERROR=0 
	BEGIN
		UPDATE AccountsInfo SET AgentID=@NewAgentID WHERE UserID = @dwUserID
		SET @strErrorDescribe=N'恭喜您！代理创建成功。'
		RETURN 0
	END
	ELSE
	BEGIN
		SET @strErrorDescribe=N'抱歉，代理创建失败。'
		RETURN 2005
	END
END
GO