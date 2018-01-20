CREATE TABLE [NGAC2].[rt_device_info] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [c_controller_id]  INT           NULL,
    [c_device_info_id] INT           NULL,
    [_timestamp]       DATETIME      NULL,
    [value]            VARCHAR (MAX) NULL
);

