----------------------------------------------------------------------
-- 时间：2010-03-16
-- 用途：赠送靓号
----------------------------------------------------------------------
USE WHJHRecordDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[WSP_PM_GrantGameID]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[WSP_PM_GrantGameID]
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO
----------------------------------------------------------------------


CREATE PROCEDURE WSP_PM_GrantGameID
	@MasterID INT,								-- 管理员标识
	@UserID INT,								-- 用户标识
	@ReGameID INT,								-- 赠送ID
	@ClientIP VARCHAR(15),						-- 赠送地址
	@Reason NVARCHAR(32),						-- 赠送原因
	@strErrorDescribe NVARCHAR(127)	OUTPUT		-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @CurGameID BIGINT
DECLARE @dwUserID INT

-- 保留ID信息
DECLARE @dwGameID INT
DECLARE @IDLevel INT

-- 返回参数
DECLARE @ReturnValue NVARCHAR(127)

-- 执行逻辑
BEGIN
	
	-- 获取游戏ID
	SELECT @CurGameID = GameID FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID = @UserID

	-- 判断
	SELECT @dwGameID = GameID,@IDLevel = IDLevel FROM WHJHAccountsDB.dbo.ReserveIdentifier WITH(NOLOCK) WHERE GameID = @ReGameID
	IF @dwGameID IS NULL
	BEGIN
		SET @strErrorDescribe = N'抱歉，赠送的靓号不存在！'
		RETURN 1001
	END

	SELECT @dwUserID = UserID FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE GameID = @ReGameID
	IF @dwUserID IS NOT NULL
	BEGIN
		SET @strErrorDescribe = N'抱歉，赠送的靓号已占用！'
		RETURN 1002
	END	

	-- 新增记录
	INSERT INTO RecordGrantGameID(MasterID,UserID,CurGameID,ReGameID,IDLevel,ClientIP,Reason)
	VALUES(@MasterID,@UserID,@CurGameID,@ReGameID,@IDLevel,@ClientIP,@Reason)

	-- 更新保留表
	UPDATE WHJHAccountsDB.dbo.ReserveIdentifier SET Distribute = 1 WHERE GameID = @ReGameID

	-- 更新用户表
	UPDATE WHJHAccountsDB.dbo.AccountsInfo SET GameID = @ReGameID WHERE UserID = @UserID

END
RETURN 0
GO