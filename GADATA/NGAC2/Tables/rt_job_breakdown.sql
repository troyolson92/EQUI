CREATE TABLE [NGAC2].[rt_job_breakdown] (
    [id]                 INT      IDENTITY (1, 1) NOT NULL,
    [rt_job_active_id]   INT      NULL,
    [h_alarm_id]         INT      NULL,
    [ts_breakdownStart]  DATETIME NULL,
    [ts_breakdownEnd]    DATETIME NULL,
    [ts_breakdownAck]    DATETIME NULL,
    [ev_breakdownStart]  INT      NULL,
    [val_breakdownStart] INT      NULL,
    [programPointer]     INT      NULL,
    [motionPointer]      INT      NULL,
    [ev_breakdownAck]    INT      NULL,
    [val_breakdownAck]   INT      NULL
);

