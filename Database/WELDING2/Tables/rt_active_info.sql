CREATE TABLE [WELDING2].[rt_active_info] (
    [id]                       INT          IDENTITY (1, 1) NOT NULL,
    [timerId]                  INT          NULL,
    [_timestamp]               DATETIME     NULL,
    [online]                   BIT          NULL,
    [communicationState]       TINYINT      NULL,
    [errorNumber]              INT          NULL,
    [errorText]                VARCHAR (64) NULL,
    [rt_alarm_id]              INT          NULL,
    [rt_job_id]                INT          NULL,
    [vwsc_state]               INT          NULL,
    [rt_weldfault_id]          INT          NULL,
    [rt_weldmeasureprotddw_id] INT          NULL,
    [rt_datechangeprot_id]     INT          NULL,
    [errorNumber_2]            INT          NULL,
    [errorText_2]              VARCHAR (64) NULL,
    [rt_user_id]               INT          NULL,
    CONSTRAINT [PK_rt_active_info] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_active_info_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID])
);



