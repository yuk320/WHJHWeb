----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2011-08-31
-- 用途：代理后台登录（微信）
----------------------------------------------------------------------------------------------------

USE WHJHAccountsDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_AgentAccountsLogin') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_AgentAccountsLogin
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

-- 帐号登录
CREATE PROCEDURE NET_PW_AgentAccountsLogin
	@strUserUin NVARCHAR(32),					-- 登录微信标识
	@strClientIP NVARCHAR(15),					-- 连接地址
	@strErrorDescribe	NVARCHAR(127) OUTPUT	-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 基本信息
DECLARE @UserID INT
DECLARE @FaceID INT
DECLARE @Accounts NVARCHAR(31)
DECLARE @Nickname NVARCHAR(31)
DECLARE @UnderWrite NVARCHAR(63)
DECLARE @AgentID INT
DECLARE @Nullity BIT
DECLARE @StunDown BIT

-- 扩展信息
DECLARE @GameID INT
DECLARE @CustomID INT
DECLARE @Gender TINYINT
DECLARE @Experience INT
DECLARE @Loveliness INT
DECLARE @MemberOrder INT
DECLARE @MemberOverDate DATETIME
DECLARE @CustomFaceVer TINYINT
DECLARE @SpreaderID INT
DECLARE @PlayTimeCount INT
DECLARE @AgentNullity TINYINT

-- 辅助变量
DECLARE @EnjoinLogon AS INT
DECLARE @StatusString NVARCHAR(127)

-- 执行逻辑
BEGIN
	-- 系统暂停
	SELECT @EnjoinLogon=StatusValue,@StatusString=StatusString FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'EnjoinLogon'
	IF @EnjoinLogon=1
	BEGIN
		SELECT @strErrorDescribe=@StatusString
		RETURN 1001
	END

	-- 效验地址
	SELECT @EnjoinLogon=EnjoinLogon FROM ConfineAddress WITH(NOLOCK) WHERE AddrString=@strClientIP AND (EnjoinOverDate>GETDATE() OR EnjoinOverDate IS NULL)
	IF @EnjoinLogon=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，系统禁止了您所在的 IP 地址的登录功能！'
		RETURN 1002
	END

	-- 查询用户
	SELECT @UserID=UserID, @GameID=GameID, @Accounts=Accounts, @Nickname=Nickname, @UnderWrite=UnderWrite, @FaceID=FaceID,@CustomID=CustomID,
		@Gender=Gender, @Nullity=Nullity, @StunDown=StunDown, @SpreaderID=SpreaderID,@PlayTimeCount=PlayTimeCount,@AgentID=AgentID
	FROM AccountsInfo WITH(NOLOCK) WHERE UserUin=@strUserUin

	-- 查询用户
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号不存在！'
		RETURN 1002
	END

	-- 帐号禁止
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已冻结！'
		RETURN 1003
	END

	-- 帐号关闭
	IF @StunDown=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已开启安全关闭！'
		RETURN 1004
	END
	-- 代理判断
	IF @AgentID=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号为非代理商！'
		RETURN 2001
	END
	-- 获取代理信息
	SELECT @AgentNullity=Nullity FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID=@AgentID
	IF @AgentNullity IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号为非代理商！'
		RETURN 2001
	END
	IF @AgentNullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的代理帐号已冻结！'
		RETURN 2002
	END

	-- 更新信息
	UPDATE AccountsInfo SET WebLogonTimes=WebLogonTimes+1,LastLogonDate=GETDATE(),LastLogonIP=@strClientIP WHERE UserID=@UserID

	-- 记录日志
	DECLARE @DateID INT
	SET @DateID=CAST(CAST(GETDATE() AS FLOAT) AS INT)
	UPDATE SystemStreamInfo SET WebLogonSuccess=WebLogonSuccess+1 WHERE DateID=@DateID
	IF @@ROWCOUNT=0 INSERT SystemStreamInfo (DateID, WebLogonSuccess) VALUES (@DateID, 1)

	-- 输出变量
	SELECT @UserID AS UserID, @GameID AS GameID, @Accounts AS Accounts, @Nickname AS Nickname,@UnderWrite AS UnderWrite, @FaceID AS FaceID, @CustomID AS CustomID,
		@Gender AS Gender,@AgentID AS AgentID
END

RETURN 0
GO


----------------------------------------------------------------------------------------------------
-- 版权：2018
-- 时间：2018-03-16
-- 用途：代理后台登录（手机号+安全密码）
----------------------------------------------------------------------------------------------------

USE WHJHAccountsDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_AgentAccountsLogin_MP') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_AgentAccountsLogin_MP
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

-- 帐号登录
CREATE PROCEDURE NET_PW_AgentAccountsLogin_MP
	@strMobile NVARCHAR(11),					-- 手机号码
	@strPassword NVARCHAR(32),					-- 安全密码
	@strClientIP NVARCHAR(15),					-- 连接地址
	@strErrorDescribe	NVARCHAR(127) OUTPUT	-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 基本信息
DECLARE @UserID INT
DECLARE @FaceID INT
DECLARE @Accounts NVARCHAR(31)
DECLARE @Nickname NVARCHAR(31)
DECLARE @UnderWrite NVARCHAR(63)
DECLARE @AgentID INT
DECLARE @Nullity BIT
DECLARE @StunDown BIT

-- 扩展信息
DECLARE @GameID INT
DECLARE @CustomID INT
DECLARE @Gender TINYINT
DECLARE @Experience INT
DECLARE @Loveliness INT
DECLARE @MemberOrder INT
DECLARE @MemberOverDate DATETIME
DECLARE @CustomFaceVer TINYINT
DECLARE @SpreaderID INT
DECLARE @PlayTimeCount INT
DECLARE @AgentNullity TINYINT

-- 辅助变量
DECLARE @EnjoinLogon AS INT
DECLARE @StatusString NVARCHAR(127)

-- 执行逻辑
BEGIN
	-- 系统暂停
	SELECT @EnjoinLogon=StatusValue,@StatusString=StatusString FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'EnjoinLogon'
	IF @EnjoinLogon=1
	BEGIN
		SELECT @strErrorDescribe=@StatusString
		RETURN 1001
	END

	-- 效验地址
	SELECT @EnjoinLogon=EnjoinLogon FROM ConfineAddress WITH(NOLOCK) WHERE AddrString=@strClientIP AND (EnjoinOverDate>GETDATE() OR EnjoinOverDate IS NULL)
	IF @EnjoinLogon=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，系统禁止了您所在的 IP 地址的登录功能！'
		RETURN 1002
	END

  -- 查询代理
  SELECT @AgentID = AgentID,@UserID = UserID,@AgentNullity=Nullity FROM AccountAgentInfo WITH(NOLOCK) WHERE ContactPhone = @strMobile AND [Password] = @strPassword

  IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号不存在！'
		RETURN 1002
	END

	-- 查询用户
	SELECT @GameID=GameID, @Accounts=Accounts, @Nickname=Nickname, @UnderWrite=UnderWrite, @FaceID=FaceID,@CustomID=CustomID,
		@Gender=Gender, @Nullity=Nullity, @StunDown=StunDown, @SpreaderID=SpreaderID,@PlayTimeCount=PlayTimeCount,@AgentID=AgentID
	FROM AccountsInfo WITH(NOLOCK) WHERE UserID=@UserID

	-- 帐号禁止
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已冻结！'
		RETURN 1003
	END

	-- 帐号关闭
	IF @StunDown=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号已开启安全关闭！'
		RETURN 1004
	END

  -- 代理判断
	IF @AgentID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号为非代理商！'
		RETURN 2001
	END
	IF @AgentNullity IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的帐号为非代理商！'
		RETURN 2001
	END
	IF @AgentNullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的代理帐号已冻结！'
		RETURN 2002
	END

	-- 更新信息
	UPDATE AccountsInfo SET WebLogonTimes=WebLogonTimes+1,LastLogonDate=GETDATE(),LastLogonIP=@strClientIP WHERE UserID=@UserID

	-- 记录日志
	DECLARE @DateID INT
	SET @DateID=CAST(CAST(GETDATE() AS FLOAT) AS INT)
	UPDATE SystemStreamInfo SET WebLogonSuccess=WebLogonSuccess+1 WHERE DateID=@DateID
	IF @@ROWCOUNT=0 INSERT SystemStreamInfo (DateID, WebLogonSuccess) VALUES (@DateID, 1)

	-- 输出变量
	SELECT @UserID AS UserID, @GameID AS GameID, @Accounts AS Accounts, @Nickname AS Nickname,@UnderWrite AS UnderWrite, @FaceID AS FaceID, @CustomID AS CustomID,
		@Gender AS Gender,@AgentID AS AgentID
END

RETURN 0
GO
