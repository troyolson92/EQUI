CREATE TABLE [WELDING].[Qcontrol] (
    [ID]                INT          IDENTITY (1, 1) NOT NULL,
    [Timer]             VARCHAR (50) NULL,
    [spot]              INT          NULL,
    [uit adaptief]      BIT          NULL,
    [ColdWeldReport]    BIT          NULL,
    [ReduceWeldingTime] BIT          NULL,
    [ReduceCurrent]     BIT          NULL,
    [SBCUAlert]         BIT          NULL,
    [EnergyDrop]        BIT          NULL
);

