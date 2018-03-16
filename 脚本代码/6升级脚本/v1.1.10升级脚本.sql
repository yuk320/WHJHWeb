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


-- v1.1.10 新建代理认证信息表
IF EXISTS (SELECT 1
FROM [DBO].SYSObjects
WHERE ID = OBJECT_ID(N'[dbo].[AgentTokenInfo]') AND OBJECTPROPERTY(ID,'IsTable')=1 )
BEGIN
  DROP TABLE [dbo].[AgentTokenInfo]
END
GO

/****** Object:  Table [dbo].[AgentTokenInfo]    Script Date: 2018/3/16 16:32:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AgentTokenInfo](
	[UserID] [int] NOT NULL,
	[AgentID] [int] NOT NULL,
	[Token] [nvarchar](64) NOT NULL,
	[ExpirtAt] [datetime] NOT NULL,
 CONSTRAINT [PK_AgentTokenInfo] PRIMARY KEY CLUSTERED
(
	[UserID] ASC,
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_UserID]  DEFAULT ((0)) FOR [UserID]
GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_AgentID]  DEFAULT ((0)) FOR [AgentID]
GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_Token]  DEFAULT (N'') FOR [Token]
GO

ALTER TABLE [dbo].[AgentTokenInfo] ADD  CONSTRAINT [DF_AgentTokenInfo_ExpirtAt]  DEFAULT (getdate()+(1)) FOR [ExpirtAt]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'AgentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'认证串（SHA256）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'Token'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AgentTokenInfo', @level2type=N'COLUMN',@level2name=N'ExpirtAt'
GO


-- v1.1.10 代理后台登录（手机号+安全密码）存储
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
