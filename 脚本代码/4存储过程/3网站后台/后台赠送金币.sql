----------------------------------------------------------------------
-- 时间：2010-03-16
-- 用途：赠送金币
----------------------------------------------------------------------

USE WHJHTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[WSP_PM_GrantTreasure]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[WSP_PM_GrantTreasure]
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO
----------------------------------------------------------------------

CREATE PROCEDURE WSP_PM_GrantTreasure
	@MasterID INT,				-- 管理员标识
	@ClientIP VARCHAR(15),		-- 赠送地址
	@UserID INT,				-- 用户标识
	@AddGold BIGINT,			-- 赠送金币
	@Reason NVARCHAR(32)		-- 赠送备注	
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户金币信息
DECLARE @CurScore BIGINT
DECLARE @CurInsureScore BIGINT
DECLARE @Nullity TINYINT
DECLARE @DateTime DATETIME
DECLARE @RegisterIP NVARCHAR(15)
DECLARE @RegisterDate DATETIME
DECLARE @RegisterMachine NVARCHAR(32)
DECLARE @InsureScore BIGINT

-- 执行逻辑
BEGIN
	-- 用户验证
	SELECT @Nullity=Nullity,@RegisterIP=RegisterIP,@RegisterDate=RegisterDate,@RegisterMachine=RegisterMachine FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID = @UserID
	IF @Nullity IS NULL
	BEGIN
		RETURN 1001
	END
	IF @Nullity = 1
	BEGIN
		RETURN 1001
	END

	-- 获取用户钻石
	SELECT @CurScore=Score,@CurInsureScore=InsureScore FROM GameScoreInfo WHERE UserID = @UserID
	IF @CurInsureScore IS NULL
	BEGIN
		SET @CurInsureScore = 0
		IF @AddGold<0 SET @InsureScore=0 ELSE SET @InsureScore = @AddGold
		INSERT INTO GameScoreInfo(UserID,Score,InsureScore,RegisterIP,RegisterDate,RegisterMachine) VALUES(@UserID,0,@InsureScore,@RegisterIP,@RegisterDate,@RegisterMachine)
	END
	ELSE
	BEGIN
		SET @InsureScore = @CurInsureScore + @AddGold
		IF @InsureScore<0 SET @InsureScore=0
		UPDATE GameScoreInfo SET InsureScore = @InsureScore WHERE UserID=@UserID
	END
	-- 写入赠送记录
	SET @DateTime = GETDATE()
	INSERT INTO WHJHRecordDB.dbo.RecordGrantTreasure(MasterID,ClientIP,CollectDate,UserID,CurGold,AddGold,Reason) VALUES(@MasterID,@ClientIP,@DateTime,@UserID,@CurInsureScore,@AddGold,@Reason)
	-- 写入流水记录
	INSERT INTO WHJHRecordDB.dbo.RecordTreasureSerial(SerialNumber,MasterID,UserID,TypeID,CurScore,CurInsureScore,ChangeScore,ClientIP,CollectDate) 
	VALUES(dbo.WF_GetSerialNumber(),@MasterID,@UserID,0,@CurScore,@CurInsureScore,@AddGold,@ClientIP,@DateTime)

END
RETURN 0

