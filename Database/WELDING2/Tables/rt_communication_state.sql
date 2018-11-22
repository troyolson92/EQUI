CREATE TABLE [WELDING2].[rt_communication_state] (
    [id]                 INT      IDENTITY (1, 1) NOT NULL,
    [timerId]            INT      NULL,
    [_timestamp]         DATETIME NULL,
    [online]             BIT      NULL,
    [communicationState] TINYINT  NULL,
    CONSTRAINT [PK_rt_communication_state] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_communication_state_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID])
);

