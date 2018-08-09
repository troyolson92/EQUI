CREATE TABLE [NGAC].[rt_value] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [c_variable_id]   INT           NULL,
    [c_controller_id] INT           NULL,
    [_timestamp]      DATETIME      NULL,
    [value]           VARCHAR (MAX) NULL,
    [isEvent]         BIT           NULL,
    [abbDateTime]     DATETIME      NULL,
    CONSTRAINT [PK_rt_value_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_value_c_controller] FOREIGN KEY ([c_controller_id]) REFERENCES [NGAC].[c_controller] ([id]),
    CONSTRAINT [FK_rt_value_c_variable] FOREIGN KEY ([c_variable_id]) REFERENCES [NGAC].[c_variable] ([id])
);

