-- =============================================
-- 用途: 查询代理所有下级（无下级限制）
-- =============================================
USE WHJHAccountsDB
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WF_GetAgentBelowAccounts]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WF_GetAgentBelowAccounts]
GO
-----------------------------------------------------------------
CREATE FUNCTION [dbo].[WF_GetAgentBelowAccounts] 
(
	@dwUserID INT = 0	--用户标识
)
RETURNS 
@tbUserInfo TABLE 
(
	UserID INT ,
	LevelID INT 
)
WITH ENCRYPTION AS
BEGIN
	DECLARE  @dwLevel INT 
	SET  @dwLevel = 1
	INSERT  INTO  @tbUserInfo SELECT  @dwUserID,@dwLevel
	WHILE @@ROWCOUNT > 0
	BEGIN 
		SET  @dwLevel = @dwLevel + 1
		INSERT  INTO  @tbUserInfo SELECT a.UserID, @dwLevel FROM AccountsInfo a INNER JOIN  @tbUserInfo b ON a.SpreaderID = b.UserID WHERE b.LevelID = @dwLevel - 1
	END 
	RETURN 
END
GO

USE WHJHTreasureDB
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WF_GetAgentBelowAccounts]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WF_GetAgentBelowAccounts]
GO
-----------------------------------------------------------------
CREATE FUNCTION [dbo].[WF_GetAgentBelowAccounts] 
(
	@dwUserID INT = 0	--用户标识
)
RETURNS 
@tbUserInfo TABLE 
(
	UserID INT ,
	LevelID INT 
)
WITH ENCRYPTION AS
BEGIN
	DECLARE  @dwLevel INT 
	SET  @dwLevel = 1
	INSERT  INTO  @tbUserInfo SELECT  @dwUserID,@dwLevel
	WHILE @@ROWCOUNT > 0
	BEGIN 
		SET  @dwLevel = @dwLevel + 1
		INSERT  INTO  @tbUserInfo SELECT a.UserID , @dwLevel FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsInfo a INNER JOIN  @tbUserInfo b ON  a.SpreaderID = b.UserID WHERE b.LevelID = @dwLevel - 1
	END 	
	RETURN 
END
GO


-- =============================================
-- 用途: 查询代理所有注册下级（无下级限制）
-- =============================================
USE WHJHAccountsDB
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WF_GetAgentBelowAccountsRegister]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WF_GetAgentBelowAccountsRegister]
GO
-----------------------------------------------------------------
CREATE FUNCTION [dbo].[WF_GetAgentBelowAccountsRegister] 
(
	@dwUserID INT = 0	--用户标识
)
RETURNS 
@tbUserInfo TABLE 
(
	UserID INT ,
	LevelID INT,
	RegisterDate DATETIME,
	RegisterOrigin TINYINT,
	GameID INT,
	NickName NVARCHAR(31),
	AgentID INT
)
WITH ENCRYPTION AS
BEGIN
	DECLARE  @dwLevel INT 
	SET  @dwLevel = 1
	INSERT INTO @tbUserInfo SELECT @dwUserID,@dwLevel,GETDATE(),0,0,N'',0
	WHILE @@ROWCOUNT > 0
	BEGIN 
		SET  @dwLevel = @dwLevel + 1
		INSERT INTO @tbUserInfo SELECT a.UserID, @dwLevel,a.RegisterDate,a.RegisterOrigin,a.GameID,a.NickName,a.AgentID FROM AccountsInfo a INNER JOIN  @tbUserInfo b ON a.SpreaderID = b.UserID WHERE b.LevelID = @dwLevel - 1
	END 
	RETURN 
END
GO
