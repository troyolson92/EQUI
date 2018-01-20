CREATE TABLE [WELDING].[spot] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [Timer]             VARCHAR (50)  NULL,
    [Spot]              INT           NULL,
    [Program]           INT           NOT NULL,
    [uit adaptief]      VARCHAR (MAX) NULL,
    [regulation]        BIT           NULL,
    [monitoring]        BIT           NULL,
    [cond.tol.band PSF] INT           NULL,
    [SpotID]            INT           NULL
);

