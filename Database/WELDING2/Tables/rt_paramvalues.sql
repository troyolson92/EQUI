CREATE TABLE [WELDING2].[rt_paramvalues] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [timerId]          INT            NULL,
    [_timestamp]       DATETIME       NULL,
    [subindex]         INT            NULL,
    [value]            VARCHAR (1024) NULL,
    [c_bosch_param_id] INT            NULL,
    [isDead]           INT            NULL,
    CONSTRAINT [PK_RT_paramvalues] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_paramvalues_c_bosch_param] FOREIGN KEY ([c_bosch_param_id]) REFERENCES [WELDING2].[c_bosch_param] ([ID]),
    CONSTRAINT [FK_rt_paramvalues_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID])
);










GO


