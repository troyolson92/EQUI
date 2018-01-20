CREATE TABLE [PJC].[ProcessResult] (
    [id]                      INT           IDENTITY (1, 1) NOT NULL,
    [isDead]                  BIT           NULL,
    [_timestamp]              DATETIME      NULL,
    [_lasttimestamp]          DATETIME      NULL,
    [deltaX]                  FLOAT (53)    NULL,
    [deltaY]                  FLOAT (53)    NULL,
    [deltaZ]                  FLOAT (53)    NULL,
    [deltaSum]                FLOAT (53)    NULL,
    [signature]               VARCHAR (MAX) NULL,
    [pp_comment]              VARCHAR (MAX) NULL,
    [pd_comment]              VARCHAR (MAX) NULL,
    [isMissingOnRobot]        INT           NULL,
    [isMissingGeometryInName] BIT           NULL,
    [wrongProcessDataId]      BIT           NULL,
    [duplicateProcessPointId] INT           NULL,
    [robotPointID]            INT           NULL,
    [joiningPointDataID]      INT           NULL,
    CONSTRAINT [PK_ProcessResult] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ProcessResult_JoiningPointData] FOREIGN KEY ([joiningPointDataID]) REFERENCES [PJC].[JoiningPointData] ([id]),
    CONSTRAINT [FK_ProcessResult_RobotPoint] FOREIGN KEY ([robotPointID]) REFERENCES [PJC].[RobotPoint] ([id])
);

