CREATE TABLE [WELDING].[Timers] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Timer]           VARCHAR (50)  NULL,
    [TimerAdress]     VARCHAR (MAX) NULL,
    [Online]          BIT           NULL,
    [SoftwareVersion] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Timers] PRIMARY KEY CLUSTERED ([ID] ASC)
);

