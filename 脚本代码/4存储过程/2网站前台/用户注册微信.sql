----------------------------------------------------------------------------------------------------
-- 版权：2017
-- 时间：2017-01-20
-- 用途：帐号注册
----------------------------------------------------------------------------------------------------

USE WHJHAccountsDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].NET_PW_RegisterAccountsWX') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].NET_PW_RegisterAccountsWX
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_NULLS ON
GO

----------------------------------------------------------------------------------------------------

-- 帐号注册
CREATE PROCEDURE NET_PW_RegisterAccountsWX
	@strUserUin			NVARCHAR(32),			    -- 用户Uin
	@strNickName		NVARCHAR(31),				-- 用户昵称
	@cbGender			TINYINT,					-- 用户性别
	@strFaceUrl			NVARCHAR(250),				-- 微信头像
	@strSpreader		NVARCHAR(31),				-- 推广员名
	@strClientIP		NVARCHAR(15),				-- 连接地址
	@dwRegisterOrigin   TINYINT,					-- 注册渠道
	@strErrorDescribe	NVARCHAR(127) OUTPUT		-- 输出信息
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 基本信息
DECLARE @UserID INT
DECLARE @FaceID INT
DECLARE @CustomID INT
DECLARE @Accounts NVARCHAR(31)
DECLARE @Nickname NVARCHAR(31)
DECLARE @UnderWrite NVARCHAR(63)

-- 扩展信息
DECLARE @GameID INT
DECLARE @SpreaderID INT
DECLARE @AgentID INT
DECLARE @Nullity TINYINT
DECLARE @Gender TINYINT
DECLARE @Experience INT
DECLARE @Loveliness INT
DECLARE @MemberOrder INT
DECLARE @MemberOverDate DATETIME
DECLARE @CustomFaceVer TINYINT
DECLARE @Compellation NVARCHAR(16)
DECLARE @PassPortID NVARCHAR(18)

-- 辅助变量
DECLARE @EnjoinLogon INT
DECLARE @EnjoinRegister INT
DECLARE @EnjoinLogonState BIT
DECLARE @EnjoinRegisterState BIT
DECLARE @StatusString NVARCHAR(200)
DECLARE @DateTime DATETIME

-- 执行逻辑
BEGIN
	SET @DateTime = GETDATE()
	-- 注册暂停
	SELECT @EnjoinRegister=StatusValue,@StatusString=StatusString FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'EnjoinRegister'
	IF @EnjoinRegister=1
	BEGIN
		SET @strErrorDescribe = @StatusString
		RETURN 1001
	END

	-- 登录暂停
	SELECT @EnjoinLogon=StatusValue,@StatusString=StatusString FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'EnjoinLogon'
	IF @EnjoinLogon=1
	BEGIN
		SET @strErrorDescribe = @StatusString
		RETURN 1002
	END

	-- 效验地址
	SELECT @EnjoinLogonState=EnjoinLogon,@EnjoinRegisterState=EnjoinRegister FROM ConfineAddress WITH(NOLOCK) WHERE AddrString=@strClientIP AND (EnjoinOverDate>@DateTime OR EnjoinOverDate IS NULL)
	IF @EnjoinRegisterState=1 OR @EnjoinLogonState=1
	BEGIN
		SET @strErrorDescribe=N'抱歉，系统禁止了您所在的 IP 地址功能！'
		RETURN 1003
	END

	-- 查推广员
	IF @strSpreader<>''
	BEGIN
		-- 查推广员
		SELECT @SpreaderID=UserID,@AgentID=AgentID FROM AccountsInfo WITH(NOLOCK) WHERE GameID=@strSpreader

		-- 结果处理
		IF @SpreaderID IS NULL OR @SpreaderID=0
		BEGIN
			SET @strErrorDescribe=N'抱歉,推广人ID填写错误！'
			RETURN 2001
		END
	END
	ELSE
	BEGIN
		SET @SpreaderID=0
		SET @AgentID=0
	END

	-- 查询用户
	IF EXISTS(SELECT UserID FROM AccountsInfo WITH(NOLOCK) WHERE UserUin=@strUserUin)
	BEGIN
		SET @strErrorDescribe=N'抱歉,相同账号已注册,请更换其他账号！'
		RETURN 2002
	END

	-- 注册用户
	-- 生成账号
	DECLARE @strTemp NVARCHAR(31)
	SET @strTemp=CONVERT(NVARCHAR(31),REPLACE(NEWID(),'-','_'))
	-- 查询账号
	IF EXISTS (SELECT UserID FROM AccountsInfo WITH(NOLOCK) WHERE Accounts=@strTemp)
	BEGIN
		SET @strErrorDescribe=N'抱歉,注册繁忙,请稍后重试！'
		RETURN 2004
	END

	-- 注册用户
	INSERT AccountsInfo (Accounts,NickName,RegAccounts,UserUin,LogonPass,InsurePass,SpreaderID,Gender,FaceID,WebLogonTimes,RegisterIP,LastLogonIP,RegisterOrigin,PlatformID)
	VALUES (@strTemp,@strNickName,@strTemp,@strUserUin,N'd1fd5495e7b727081497cfce780b6456',N'',@SpreaderID,@cbGender,0,0,@strClientIP,@strClientIP,@dwRegisterOrigin,5)
	IF @@ROWCOUNT<=0
	BEGIN
		SET @strErrorDescribe=N'抱歉，注册失败，请稍后重试！'
		RETURN 2005
	END

	-- 查询用户
	SELECT @UserID=UserID, @Accounts=Accounts, @Nickname=Nickname,@UnderWrite=UnderWrite, @Gender=Gender, @Compellation=Compellation,@PassPortID=PassPortID
	FROM AccountsInfo WITH(NOLOCK) WHERE UserUin=@strUserUin

	-- 写入头像
	INSERT INTO AccountsFace(UserID,InsertTime,InsertAddr,InsertMachine,FaceUrl) VALUES(@UserID,@DateTime,@strClientIP,'',@strFaceUrl)
	SELECT @CustomID = SCOPE_IDENTITY()

	-- 分配标识
	SELECT @GameID=GameID FROM GameIdentifier WITH(NOLOCK) WHERE UserID=@UserID
	IF @GameID IS NULL
	BEGIN
		UPDATE AccountsInfo SET CustomID = @CustomID WHERE UserID=@UserID
		SET @GameID=0
		SET @strErrorDescribe=N'注册成功，请联系管理员分配ID ！'
	END
	ELSE
	BEGIN
		UPDATE AccountsInfo SET GameID=@GameID,CustomID = @CustomID WHERE UserID=@UserID
	END

	-- 初始化金币信息
	INSERT INTO WHJHTreasureDBLink.WHJHTreasureDB.dbo.GameScoreInfo(UserID,RegisterIP) VALUES(@UserID,@strClientIP)
	INSERT INTO WHJHTreasureDBLink.WHJHTreasureDB.dbo.UserCurrency(UserID,Diamond) VALUES(@UserID,0)


  DECLARE @BeforeDiamond INT
  DECLARE @BeforeScore BIGINT
  DECLARE @BeforeInsure BIGINT
  SET @BeforeDiamond = 0
  SET @BeforeScore = 0
  SET @BeforeInsure=0
	-- 注册赠送钻石
	DECLARE @PresentDiamond INT
	DECLARE @PresentGold INT
	SELECT @PresentDiamond=StatusValue FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'JJRegisterDiamondCount'
	SELECT @PresentGold=StatusValue FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'GrantScoreCount'
	IF (@PresentDiamond IS NULL OR @PresentDiamond<0) SET @PresentDiamond=0
	IF (@PresentGold IS NULL OR @PresentGold<0) SET @PresentGold=0
	IF @PresentDiamond>0
  BEGIN
		UPDATE WHJHTreasureDBLink.WHJHTreasureDB.dbo.UserCurrency SET Diamond=Diamond + @PresentDiamond WHERE UserID=@UserID

    INSERT INTO WHJHRecordDBLink.WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
		VALUES(dbo.WF_GetSerialNumber(),0,@UserID,1,@BeforeDiamond,@PresentDiamond,@strClientIP,GETDATE())
    SET @BeforeDiamond = @BeforeDiamond + @PresentDiamond
  END
  IF @PresentGold>0
	BEGIN
    UPDATE WHJHTreasureDBLink.WHJHTreasureDB.dbo.GameScoreInfo SET Score = Score + @PresentGold WHERE UserID=@UserID

    INSERT INTO WHJHRecordDBLink.WHJHRecordDB.dbo.RecordTreasureSerial(SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate)
		VALUES(dbo.WF_GetSerialNumber(),0,@UserID,1,@BeforeScore,@BeforeInsure,@PresentGold,@strClientIP,GETDATE())
    SET @BeforeScore = @BeforeScore + @PresentGold
	END

  -- 绑定推广赠送钻石
	DECLARE @PresentBindDiamond INT
  SELECT @PresentBindDiamond=StatusValue FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=N'JJBindSpreadPresent'
  IF (@PresentBindDiamond IS NULL OR @PresentBindDiamond<0) SET @PresentBindDiamond=0
  IF @PresentBindDiamond>0
  BEGIN
    -- 更新用户钻石信息
		UPDATE WHJHTreasureDBLink.WHJHTreasureDB.dbo.UserCurrency SET Diamond=Diamond + @PresentBindDiamond WHERE UserID=@UserID

    INSERT INTO WHJHRecordDBLink.WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate)
		VALUES(dbo.WF_GetSerialNumber(),0,@UserID,4,@BeforeDiamond,@PresentBindDiamond,@strClientIP,GETDATE())
  END

	-- 记录日志
	DECLARE @DateID INT
	SET @DateID=CAST(CAST(@DateTime AS FLOAT) AS INT)
	UPDATE SystemStreamInfo SET WebRegisterSuccess=WebRegisterSuccess+1 WHERE DateID=@DateID
	IF @@ROWCOUNT=0 INSERT SystemStreamInfo (DateID, WebRegisterSuccess) VALUES (@DateID, 1)

END

RETURN 0

GO
