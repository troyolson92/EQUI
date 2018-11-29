CREATE TABLE [WELDING2].[rt_job_breakdown] (
    [id]                INT      IDENTITY (1, 1) NOT NULL,
    [rt_job_id]         INT      NULL,
    [rt_alarm_id]       INT      NULL,
    [ts_breakdownStart] DATETIME NULL,
    [ts_breakdownEnd]   DATETIME NULL,
    [index]             INT      NULL,
    CONSTRAINT [PK_rt_job_breakdown_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_job_breakdown_rt_alarm] FOREIGN KEY ([rt_alarm_id]) REFERENCES [WELDING2].[rt_alarm] ([id]),
    CONSTRAINT [FK_rt_job_breakdown_rt_job] FOREIGN KEY ([rt_job_id]) REFERENCES [WELDING2].[rt_job] ([id])
);

