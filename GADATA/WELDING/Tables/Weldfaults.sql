CREATE TABLE [WELDING].[Weldfaults] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [Datetime]          DATETIME      NULL,
    [Timer]             VARCHAR (50)  NULL,
    [program]           INT           NULL,
    [spot]              INT           NULL,
    [TimerFault]        VARCHAR (MAX) NULL,
    [WeldmasterComment] VARCHAR (MAX) NULL
);

