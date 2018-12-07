CREATE TABLE [WELDING2].[c_bosch_view] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [view]       VARCHAR (MAX) NULL,
    [enable_bit] INT           NULL,
    [poll_rate]  INT           NULL,
    [flag]       INT           NULL,
    [rt_table]   VARCHAR (50)  NULL,
    [hour_limit] INT           NULL,
    [ordinal]    INT           NULL,
    CONSTRAINT [PK_c_bosch_view] PRIMARY KEY CLUSTERED ([id] ASC)
);



