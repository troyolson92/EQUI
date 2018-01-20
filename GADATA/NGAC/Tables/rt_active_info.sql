CREATE TABLE [NGAC].[rt_active_info] (
    [id]                    INT      IDENTITY (1, 1) NOT NULL,
    [c_controller_id]       INT      NULL,
    [h_alarm_id]            INT      NULL,
    [rt_job_id]             INT      NULL,
    [_timestamp]            DATETIME NULL,
    [program_number]        INT      NULL,
    [body_number]           INT      NULL,
    [application_error]     INT      NULL,
    [operating_mode]        INT      NULL,
    [execution_status]      INT      NULL,
    [controller_state]      INT      NULL,
    [master_state]          INT      NULL,
    [at_home]               INT      NULL,
    [vasc_state]            INT      NULL,
    [task_execution_status] INT      NULL
);

