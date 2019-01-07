CREATE TABLE [WELDING2].[rt_job] (
    [id]                             INT      IDENTITY (1, 1) NOT NULL,
    [_timestamp]                     DATETIME NULL,
    [timerId]                        INT      NULL,
    [ngac_rt_job_id]                 INT      NULL,
    [ts_Start]                       DATETIME NULL,
    [ts_End]                         DATETIME NULL,
    [rt_weldmeasureprotddw_id_Start] INT      NULL,
    [rt_weldmeasureprotddw_id_End]   INT      NULL,
    [rt_weldfaultprot_id_Start]      INT      NULL,
    [rt_weldfaultprot_id_End]        INT      NULL,
    [state]                          INT      NULL,
    [rt_expulsion_count]             INT      NULL,
    [rt_weldmeasureprotddw_count]    INT      NULL,
    [rt_weldfaultprot_count]         INT      NULL,
    [rt_breakdown_count]             INT      NULL,
    [rt_weldmeasureprotddw_distinct] INT      NULL,
    [rt_midair_count]                INT      NULL,
    CONSTRAINT [PK_welding2_rt_job] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_job_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID])
);



