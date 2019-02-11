CREATE TABLE [UL].[T_PointsList] (
    [DBname]    NVARCHAR (50) NULL,
    [PointID]   INT           NOT NULL,
    [FatherID]  INT           NOT NULL,
    [Name]      NVARCHAR (50) NOT NULL,
    [Method]    INT           NOT NULL,
    [Created]   DATETIME      NOT NULL,
    [Creator]   NVARCHAR (50) NOT NULL,
    [Sequence]  INT           NOT NULL,
    [Diameter]  REAL          NULL,
    [Diameter2] REAL          NULL,
    [Plate1]    INT           NOT NULL,
    [Plate2]    INT           NOT NULL,
    [Plate3]    INT           NULL
);



