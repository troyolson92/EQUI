CREATE TABLE [WELDING].[Weldparameter] (
    [ID]              INT  IDENTITY (1, 1) NOT NULL,
    [ParameterNameID] INT  NOT NULL,
    [SpotID]          INT  NOT NULL,
    [Value]           REAL NOT NULL
);

