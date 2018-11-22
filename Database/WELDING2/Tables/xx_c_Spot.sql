CREATE TABLE [WELDING2].[xx_c_Spot] (
    [ID]                  INT        IDENTITY (1, 1) NOT NULL,
    [Number]              INT        NULL,
    [Zone]                TINYINT    NULL,
    [Comment1]            NCHAR (25) NULL,
    [Comment2]            NCHAR (25) NULL,
    [Comment3]            NCHAR (25) NULL,
    [PlateCombinationtId] INT        NOT NULL,
    [Program]             TINYINT    NOT NULL,
    [TimerID]             INT        NOT NULL,
    [ElectrodeDia]        TINYINT    NOT NULL,
    [AlternativeNumber]   NCHAR (25) NULL,
    [Model]               NCHAR (10) NULL,
    [Variant]             NCHAR (50) NULL,
    [JobCode]             INT        NULL,
    [NuggetDemand]        REAL       NULL,
    [HiddenSpot]          BIT        NULL,
    [GeoSpot]             BIT        NULL,
    [SpotLeft]            INT        NULL,
    [SpotRight]           INT        NULL,
    CONSTRAINT [PK_Spot] PRIMARY KEY CLUSTERED ([ID] ASC)
);

