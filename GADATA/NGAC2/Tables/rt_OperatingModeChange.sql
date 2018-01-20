CREATE TABLE [NGAC2].[rt_OperatingModeChange] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [c_controller_id] INT           NULL,
    [_timestamp]      DATETIME      NULL,
    [etimestamp]      DATETIME      NULL,
    [value]           VARCHAR (MAX) NULL
);

