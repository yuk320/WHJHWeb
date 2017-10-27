----------------------------------------------------------------------
-- 版本：2017
-- 时间：2017-10-26
-- 用途：统计每日数据，每天凌晨4点自动统计
----------------------------------------------------------------------
USE [msdb]
GO
DECLARE @JOBNAME NVARCHAR(16)
SET @JOBNAME = N'每日统计'

/****** Object:  Job [每日统计]    Script Date: 2017/6/15 9:53:36 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [[Uncategorized (Local)]]]    Script Date: 2017/6/15 9:53:36 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

-- 删除重复
IF EXISTS (SELECT 1 FROM sysjobs where name=@JOBNAME)
BEGIN
	EXEC msdb.dbo.sp_delete_jobschedule @job_name=@JOBNAME,@name=@JOBNAME
	EXEC msdb.dbo.sp_delete_jobstep @job_name=@JOBNAME,@step_id=1
	EXEC msdb.dbo.sp_delete_job @job_name=@JOBNAME
END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=@JOBNAME, 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=@JOBNAME, 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [每日统计]    Script Date: 2017/6/15 9:53:36 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@JOBNAME, 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'NET_PJ_AnalEveryDayDataStat', 
		@database_name=N'WHJHRecordDB', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=@JOBNAME, 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20170615, 
		@active_end_date=99991231, 
		@active_start_time=41000, 
		@active_end_time=235959, 
		@schedule_uid=N'c5e11fe8-aa46-489b-adb4-0a904a477a9a'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO


