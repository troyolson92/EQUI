CREATE TABLE [WELDING].[Datachanges] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Timestamp] DATETIME      NULL,
    [Timer]     VARCHAR (50)  NULL,
    [program]   INT           NULL,
    [Parameter] VARCHAR (MAX) NULL,
    [OldValue]  VARCHAR (MAX) NULL,
    [NewValue]  VARCHAR (MAX) NULL,
    [CDSID]     VARCHAR (50)  NULL
);

