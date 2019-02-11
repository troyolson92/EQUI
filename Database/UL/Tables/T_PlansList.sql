CREATE TABLE [UL].[T_PlansList] (
    [DBname]                   NVARCHAR (50) NULL,
    [PlanID]                   INT           NOT NULL,
    [FatherID]                 INT           NOT NULL,
    [Name]                     NVARCHAR (50) NOT NULL,
    [Created]                  DATETIME      NOT NULL,
    [Creator]                  NVARCHAR (50) NOT NULL,
    [CreationVersion]          NVARCHAR (25) NULL,
    [Identifikation]           BIT           NOT NULL,
    [NIO]                      BIT           NOT NULL,
    [ASCAN]                    BIT           NOT NULL,
    [EvaluationKeys]           SMALLINT      NULL,
    [ToleranceNIOClass]        BIT           NOT NULL,
    [ToleranceZinkNIOClass]    BIT           NOT NULL,
    [ToleranceGasporeNIOClass] BIT           NOT NULL
);



