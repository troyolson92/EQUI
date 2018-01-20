CREATE TABLE [NGAC2].[rt_value] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [c_variable_id]   INT           NULL,
    [c_controller_id] INT           NULL,
    [_timestamp]      DATETIME      NULL,
    [value]           VARCHAR (MAX) NULL,
    [isEvent]         BIT           NULL,
    [abbDateTime]     DATETIME      NULL
);

