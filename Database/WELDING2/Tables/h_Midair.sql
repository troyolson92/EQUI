CREATE TABLE [WELDING2].[h_Midair] (
    [ID]             INT      IDENTITY (1, 1) NOT NULL,
    [DateTime]       DATETIME NULL,
    [SpotId]         INT      NOT NULL,
    [ResisActual]    SMALLINT NULL,
    [ResisRef]       SMALLINT NULL,
    [ElectrodeNo]    TINYINT  NULL,
    [EnergyActual]   SMALLINT NULL,
    [WeldTimeActual] SMALLINT NULL,
    [Iactual2]       REAL     NULL,
    [UpperTolBand]   REAL     NULL,
    [LowerTolBand]   REAL     NULL,
    [LowerCondBand]  REAL     NULL,
    CONSTRAINT [PK_h_Midair] PRIMARY KEY CLUSTERED ([ID] ASC)
);



