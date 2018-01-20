CREATE TABLE [NGAC2].[c_variable] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [variable]   VARCHAR (MAX) NULL,
    [poll_rate]  INT           NULL,
    [enable_bit] INT           NULL,
    [event_enum] INT           NULL,
    [sql_action] INT           NULL,
    [rt_table]   VARCHAR (MAX) NULL
);

