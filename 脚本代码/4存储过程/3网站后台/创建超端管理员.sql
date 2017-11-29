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

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[NET_PM_AddSuperUser]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[NET_PM_AddSuperUser]
GO

----------------------------------------------------------------------
CREATE PROC [NET_PM_AddSuperUser]
(
	@strAccounts			NVARCHAR(32),			--用户名
	@strLogonPass    	NVARCHAR(32),			-- 登录密码

	@strErrorDescribe NVARCHAR(127) OUTPUT		--输出信息
)

WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

DECLARE @UserID INT  -- 用户标识

BEGIN
	-- 超端管理员信息
	INSERT INTO [dbo].[AccountsInfo]
      ([Accounts],[NickName],[RegAccounts],[LogonPass],[InsurePass],[UserRight],[MasterRight],[ServiceRight],[MasterOrder]
		  ,[LastLogonIP],[LastLogonDate],[RegisterIP],[RegisterDate],[RegisterOrigin])
  VALUES
      (@strAccounts,@strAccounts,@strAccounts,@strLogonPass,@strLogonPass,536870912,184549632,0,9,
      N'',GETDATE(),N'',GETDATE(),0)

	SELECT @UserID = SCOPE_IDENTITY()

	-- 设置用户
	IF @@ERROR=0
	BEGIN
		SET @strErrorDescribe=N'恭喜您！超端管理员创建成功。'
		RETURN 0
	END
	ELSE
	BEGIN
		SET @strErrorDescribe=N'抱歉，超端管理员创建失败。'
		RETURN 2003
	END
END
GO
