
----------------------------------------------------------------------
-- 时间：2011-09-26
-- 用途：管理员登录
----------------------------------------------------------------------
USE WHJHPlatformManagerDB
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PM_UserLogon]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PM_UserLogon]
GO
-----------------------------------------------------------------------
CREATE PROC [NET_PM_UserLogon]
	@strUserName		NVARCHAR(31),					-- 管理员帐号
	@strPassword		NCHAR(32),						-- 登录密码
	@strClientIP		NVARCHAR(15),					-- 登录IP
	@strErrorDescribe	NVARCHAR(127) OUTPUT			-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON
DECLARE @UserID INT
DECLARE @Nullity INT 
DECLARE @RoleID INT
DECLARE @RoleName NVARCHAR(128)
DECLARE @PreLogintime DATETIME
DECLARE @PreLoginIP NVARCHAR(50)
DECLARE @NowTime DATETIME
DECLARE @LoginTimes INT

-- 执行逻辑
BEGIN	
	-- 账号验证
	SELECT @UserID=UserID,@Nullity=Nullity,@RoleID=RoleID,@PreLogintime=LastLogintime,@PreLoginIP=LastLoginIP,@LoginTimes=LoginTimes FROM Base_Users WITH(NOLOCK) WHERE UserName=@strUserName AND [Password]=@strPassword
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe = N'抱歉，您的帐号信息错误！'
		RETURN 100
	END 
	IF @Nullity = 1
	BEGIN
		SET @strErrorDescribe = N'抱歉，您的帐号已冻结！'
		RETURN 101
	END
	-- 角色信息
	SELECT @RoleName=RoleName FROM Base_Roles WITH(NOLOCK) WHERE RoleID=@RoleID

	-- 更新登录信息
	SET @NowTime = GETDATE()
	UPDATE Base_Users SET LoginTimes=LoginTimes+1,PreLogintime=@PreLogintime,PreLoginIP=@PreLoginIP, LastLoginTime=@NowTime,LastLoginIP=@strClientIP WHERE UserID=@UserID

	-- 记录登录信息
	INSERT INTO SystemSecurity(OperatingTime,OperatingName,OperatingIP,OperatingAccounts) VALUES(@NowTime,'后台登录',@strClientIP,@strUserName)

	-- 返回信息
	SET @LoginTimes = @LoginTimes + 1
	SELECT @UserID AS UserID,@strUserName AS UserName,@RoleID AS RoleID,@RoleName AS RoleName,@LoginTimes AS LoginTimes,@PreLogintime AS PreLogintime,@PreLoginIP AS PreLoginIP

END
RETURN 0
GO