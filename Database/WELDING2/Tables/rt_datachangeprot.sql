CREATE TABLE [WELDING2].[rt_datachangeprot] (
    [id]                INT          IDENTITY (1, 1) NOT NULL,
    [timerId]           INT          NULL,
    [_timestamp]        DATETIME     NULL,
    [protRecord_ID]     INT          NULL,
    [dateTime]          DATETIME     NULL,
    [param_ID]          INT          NULL,
    [param_status_txt]  VARCHAR (81) NULL,
    [subIndex]          INT          NULL,
    [oldValue]          VARCHAR (81) NULL,
    [oldValue_txt]      VARCHAR (81) NULL,
    [newValue]          VARCHAR (81) NULL,
    [newValue_txt]      VARCHAR (81) NULL,
    [oldNormValue]      VARCHAR (81) NULL,
    [newNormValue]      VARCHAR (81) NULL,
    [c_user_id]         INT          NULL,
    [rt_paramvalues_id] INT          NULL,
    [comment]           VARCHAR (64) NULL,
    CONSTRAINT [PK_rt_datachangeprot] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_datachangeprot_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID]),
    CONSTRAINT [FK_rt_datachangeprot_c_user] FOREIGN KEY ([c_user_id]) REFERENCES [WELDING2].[c_user] ([id])
);





