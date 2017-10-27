----------------------------------------------------------------------------------------------------
-- 版权：2011
-- 时间：2012-02-23
-- 用途：代理钻石赠送
----------------------------------------------------------------------------------------------------

USE WHJHTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_AgentPresentDiamond') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_AgentPresentDiamond
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE NET_PW_AgentPresentDiamond
	@dwUserID	INT,						-- 用户 I D
	@dwPresentCount INT,					-- 赠送数量
	@dwGameID INT,							-- 赠送ID
	@strPassword NCHAR(32),					-- 安全密码
	@strNote	NVARCHAR(63),				-- 赠送备注
	@strClientIP NVARCHAR(15),				-- 操作地址
	@strErrorDescribe NVARCHAR(127) OUTPUT	-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户信息
DECLARE @UserID INT
DECLARE @Nullity BIT
DECLARE @StunDown BIT
DECLARE @Password NVARCHAR(32)
DECLARE @SDiamond BIGINT
DECLARE @AgentID INT
DECLARE @TAgentID INT
DECLARE @ParentAgent INT
DECLARE @SpreaderID INT

DECLARE @TUserID INT
DECLARE @TDiamond BIGINT
DECLARE @TAgentLevel TINYINT

DECLARE @DateTime DATETIME

-- 执行逻辑
BEGIN
	SET @DateTime = GETDATE()
	-- 查询用户
	SELECT @UserID=UserID, @Nullity=Nullity, @StunDown=StunDown,@AgentID=AgentID FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID=@dwUserID
	SELECT @TUserID=UserID,@SpreaderID=SpreaderID,@TAgentID=AgentID FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE GameID=@dwGameID

	-- 查询用户
	IF @UserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，赠送账号不存在！'
		RETURN 1001
	END	
	--IF @AgentID<=0
	--BEGIN
	--	SET @strErrorDescribe=N'抱歉，赠送账号为非代理账号！'
	--	RETURN 1002
	--END
	IF @TUserID IS NULL
	BEGIN
		SET @strErrorDescribe=N'抱歉，赠送目标账号不存在！'
		RETURN 1003
	END

	-- 帐号禁止
	IF @Nullity=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，赠送账号已冻结状态！'
		RETURN 1004
	END	

	-- 帐号关闭
	IF @StunDown=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，赠送账号已开启安全关闭功能！'
		RETURN 1005
	END		

	-- 验证安全密码
	SELECT @Password=[Password] FROM WHJHAccountsDB.dbo.AccountsAgentInfo WITH(NOLOCK) WHERE AgentID=@AgentID
	IF @Password=N''
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的安全密码未设置！'
		RETURN 2001
	END
	IF @Password IS NULL OR @Password!=@strPassword
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的安全密码错误！'
		RETURN 2002
	END

	-- 验证目标账号级别
	SELECT @ParentAgent=ParentAgent,@TAgentLevel=AgentLevel FROM WHJHAccountsDB.dbo.AccountsAgentInfo WITH(NOLOCK) WHERE AgentID=@TAgentID
	IF @TAgentID>0 AND @ParentAgent!=@AgentID
	BEGIN
		SET @strErrorDescribe=N'抱歉，代理只能给自己下线赠送！'
		RETURN 3001
	END
	IF @TAgentLevel IS NULL SET @TAgentLevel =0


	-- 开启事务
	BEGIN TRAN

	SELECT @SDiamond=Diamond FROM UserCurrency WITH(ROWLOCK) WHERE UserID=@UserID
	IF @SDiamond IS NULL OR @SDiamond < @dwPresentCount
	BEGIN
		SET @strErrorDescribe=N'抱歉，您的钻石不足，请先充值！'
		ROLLBACK TRAN
		RETURN 4001
	END

	UPDATE UserCurrency SET Diamond = Diamond - @dwPresentCount WHERE UserID=@UserID
	IF @@ROWCOUNT <= 0
	BEGIN
		SET @strErrorDescribe=N'抱歉，赠送钻石失败！'
		ROLLBACK TRAN
		RETURN 4002
	END

	SELECT @TDiamond=Diamond FROM UserCurrency WHERE UserID = @TUserID
	IF @TDiamond IS NULL
	BEGIN
		SET @TDiamond = 0
		INSERT INTO UserCurrency VALUES(@TUserID,@dwPresentCount)
	END
	ELSE
	BEGIN
		UPDATE UserCurrency SET Diamond = Diamond + @dwPresentCount WHERE UserID = @TUserID
	END

	IF @@ROWCOUNT <= 0
	BEGIN
		SET @strErrorDescribe=N'抱歉，赠送钻石失败！'
		ROLLBACK TRAN
		RETURN 4003
	END

	COMMIT TRAN

	-- 写入操作记录
	INSERT INTO WHJHRecordDB.dbo.RecordPresentCurrency(SourceUserID,SourceDiamond,TargetUserID,TargetDiamond,TargetAgentLevel,PresentDiamond,ClientIP,CollectDate,CollectNote) 
	VALUES(@UserID,@SDiamond,@TUserID,@TDiamond,@TAgentLevel,@dwPresentCount,@strClientIP,@DateTime,@strNote)
	
	-- 写入钻石流水记录
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
	VALUES(dbo.WF_GetSerialNumber(),0,@UserID,7,@SDiamond,-@dwPresentCount,@strClientIP,@DateTime)
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
	VALUES(dbo.WF_GetSerialNumber(),0,@TUserID,8,@TDiamond,@dwPresentCount,@strClientIP,@DateTime)

	SET @strErrorDescribe=N'赠送成功！' 
END

RETURN 0

GO