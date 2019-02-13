CREATE TABLE [UL].[rt_UltralogData] (
    [id]                   INT           IDENTITY (1, 1) NOT NULL,
    [Spotname]             CHAR (50)     NULL,
    [EvaluationClass]      CHAR (50)     NULL,
    [Autocomment]          CHAR (30)     NULL,
    [InspectorComment]     CHAR (50)     NULL,
    [Partname]             CHAR (50)     NULL,
    [InspectionPlanname]   CHAR (50)     NULL,
    [measuredThickness]    FLOAT (53)    NULL,
    [NominalDiameter]      FLOAT (53)    NULL,
    [ULDateTime]           DATETIME      NULL,
    [Planlength]           INT           NULL,
    [BackWallEchos]        INT           NULL,
    [FlawEchos]            INT           NULL,
    [GasporeEchos]         INT           NULL,
    [IndexOfTestsequence]  INT           NULL,
    [MinimumIndentation]   INT           NULL,
    [MinimumIndentationMM] FLOAT (53)    NULL,
    [Inspector]            NCHAR (25)    NULL,
    [teststation]          CHAR (50)     NULL,
    [NamePlate1]           CHAR (50)     NULL,
    [MaterialPlate1]       CHAR (50)     NULL,
    [ThicknessPlate1]      FLOAT (53)    NULL,
    [NamePlate2]           CHAR (50)     NULL,
    [MaterialPlate2]       CHAR (50)     NULL,
    [ThicknessPlate2]      FLOAT (53)    NULL,
    [NamePlate3]           CHAR (50)     NULL,
    [MaterialPlate3]       CHAR (50)     NULL,
    [ThicknessPlate3]      FLOAT (53)    NULL,
    [InspectionLaptop]     VARCHAR (255) NULL,
    CONSTRAINT [PK_rt_UltralogData] PRIMARY KEY CLUSTERED ([id] ASC)
);





