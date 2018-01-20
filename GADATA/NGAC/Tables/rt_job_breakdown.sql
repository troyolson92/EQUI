CREATE TABLE [NGAC].[rt_job_breakdown] (
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
    [val_breakdownAck]   INT      NULL,
    [index]              INT      NULL
);


GO
CREATE NONCLUSTERED INDEX [ngac_rtjb_activeJob]
    ON [NGAC].[rt_job_breakdown]([rt_job_active_id] ASC, [index] ASC)
    INCLUDE([h_alarm_id]);


GO
CREATE NONCLUSTERED INDEX [ngac_rtjb_h_alarm]
    ON [NGAC].[rt_job_breakdown]([index] ASC)
    INCLUDE([rt_job_active_id], [h_alarm_id]);

