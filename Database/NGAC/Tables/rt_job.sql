CREATE TABLE [NGAC].[rt_job] (
    [id]                INT        IDENTITY (1, 1) NOT NULL,
    [c_controller_id]   INT        NULL,
    [c_job_id]          INT        NULL,
    [jobNo]             INT        NULL,
    [bodyNo]            INT        NULL,
    [ts_Start]          DATETIME   NULL,
    [ts_End]            DATETIME   NULL,
    [errorMask]         INT        NULL,
    [breakDownCount]    INT        NULL,
    [state]             INT        NULL,
    [ts_breakDownStart] DATETIME   NULL,
    [ts_breakDownEnd]   DATETIME   NULL,
    [ts_breakDownAck]   DATETIME   NULL,
    [_timestamp]        DATETIME   NULL,
    [cycletime]         FLOAT (53) NULL,
    [partDataCount]     INT        NULL,
    CONSTRAINT [PK_rt_job] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_job_c_controller] FOREIGN KEY ([c_controller_id]) REFERENCES [NGAC].[c_controller] ([id]),
    CONSTRAINT [FK_rt_job_c_job] FOREIGN KEY ([c_job_id]) REFERENCES [NGAC].[c_job] ([id])
);

go
CREATE NONCLUSTERED INDEX [NGAC_rtj_tsStart] ON [NGAC].[rt_job]
(
	[ts_breakDownStart] ASC,
	[ts_breakDownEnd] ASC
)
INCLUDE ( 	[id],
	[c_controller_id],
	[ts_breakDownAck]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

