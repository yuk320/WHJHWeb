----------------------------------------------------------------------
-- 时间：2010-03-16
-- 用途：赠送钻石
----------------------------------------------------------------------

USE WHJHTreasureDB
GO

IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[dbo].[WSP_PM_GrantDiamond]') and OBJECTPROPERTY(ID, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[WSP_PM_GrantDiamond]
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_NULLS ON 
GO
----------------------------------------------------------------------

CREATE PROCEDURE WSP_PM_GrantDiamond
	@MasterID INT,				-- 管理员标识
	@ClientIP VARCHAR(15),		-- 赠送地址
	@UserID INT,				-- 用户标识
	@AddDiamond BIGINT,			-- 赠送钻石
	@TypeID INT,				-- 记录类型
	@CollectNote NVARCHAR(32)	-- 赠送备注	
WITH ENCRYPTION AS

-- 属性设置
SET NOCOUNT ON

-- 用户金币信息
DECLARE @CurDiamond BIGINT
DECLARE @Nullity TINYINT
DECLARE @DateTime DATETIME

-- 执行逻辑
BEGIN
	-- 用户验证
	SELECT @Nullity=Nullity FROM WHJHAccountsDB.dbo.AccountsInfo WITH(NOLOCK) WHERE UserID = @UserID
	IF @Nullity IS NULL
	BEGIN
		RETURN 1001
	END
	IF @Nullity = 1
	BEGIN
		RETURN 1001
	END

	-- 获取用户钻石
	SELECT @CurDiamond=Diamond FROM UserCurrency WHERE UserID = @UserID
	IF @CurDiamond IS NULL
	BEGIN
		SET @CurDiamond = 0
		INSERT INTO UserCurrency VALUES(@UserID,@AddDiamond)
	END
	ELSE
	BEGIN
		UPDATE UserCurrency SET Diamond = Diamond + @AddDiamond WHERE UserID=@UserID
	END

	SET @DateTime = GETDATE()
	INSERT INTO WHJHRecordDB.dbo.RecordGrantDiamond(MasterID,UserID,TypeID,CurDiamond,AddDiamond,ClientIP,CollectDate,CollectNote) 
	VALUES(@MasterID,@UserID,@TypeID,@CurDiamond,@AddDiamond,@ClientIP,@DateTime,@CollectNote)

	-- 写入钻石变化记录
	INSERT INTO WHJHRecordDB.dbo.RecordDiamondSerial(SerialNumber,MasterID,UserID,TypeID,CurDiamond,ChangeDiamond,ClientIP,CollectDate) 
	VALUES(dbo.WF_GetSerialNumber(),@MasterID,@UserID,0,@CurDiamond,@AddDiamond,@ClientIP,@DateTime)

END
RETURN 0

