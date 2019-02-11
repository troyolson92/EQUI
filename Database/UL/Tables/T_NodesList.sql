CREATE TABLE [UL].[T_NodesList] (
    [DBname]   NVARCHAR (50)  NULL,
    [NodeID]   INT            NOT NULL,
    [Name]     NVARCHAR (50)  NOT NULL,
    [FatherID] INT            NOT NULL,
    [FullPath] NVARCHAR (MAX) NOT NULL
);

