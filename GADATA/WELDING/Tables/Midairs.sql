CREATE TABLE [WELDING].[Midairs] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [date]             DATETIME      NULL,
    [Timer]            NVARCHAR (50) NULL,
    [Program]          INT           NULL,
    [wear]             INT           NULL,
    [regulation]       VARCHAR (50)  NULL,
    [DressCounter]     INT           NULL,
    [ElectrodeNr]      INT           NULL,
    [WeldTime]         REAL          NULL,
    [Energy]           REAL          NULL,
    [ResistanceActual] REAL          NULL,
    [ResistanceRef]    REAL          NULL,
    [UIRMeasuring]     BIT           NULL,
    [UIRmonitoring]    BIT           NULL
);

