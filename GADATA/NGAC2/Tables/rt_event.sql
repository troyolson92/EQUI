CREATE TABLE [NGAC2].[rt_event] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [c_controller_id] INT           NULL,
    [event_enum]      INT           NULL,
    [event_value]     INT           NULL,
    [_timestamp]      DATETIME      NULL,
    [abbDateTime]     DATETIME      NULL,
    [description]     VARCHAR (MAX) NULL,
    [isEvent]         BIT           NULL
);

