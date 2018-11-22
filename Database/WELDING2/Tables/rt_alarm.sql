CREATE TABLE [WELDING2].[rt_alarm] (
    [id]             INT          IDENTITY (1, 1) NOT NULL,
    [timerId]        INT          NULL,
    [_timestamp]     DATETIME     NULL,
    [protRecord_ID]  INT          NULL,
    [dateTime]       DATETIME     NULL,
    [errorCode1]     INT          NULL,
    [errorCode1_txt] VARCHAR (64) NULL,
    [errorCode2]     INT          NULL,
    [errorCode2_txt] VARCHAR (64) NULL,
    [isError]        BIT          NULL,
    [isError_txt]    VARCHAR (64) NULL,
    CONSTRAINT [PK_rt_alarm] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_alarm_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID])
);

