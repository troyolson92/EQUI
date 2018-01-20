CREATE TABLE [PJC].[JoiningResult] (
    [id]                 INT           IDENTITY (1, 1) NOT NULL,
    [isDead]             BIT           NULL,
    [_timestamp]         DATETIME      NULL,
    [_lasttimestamp]     DATETIME      NULL,
    [signature]          VARCHAR (MAX) NULL,
    [isMissingOnRobot]   INT           NULL,
    [isOnTwoRobots]      BIT           NULL,
    [jp_comment]         VARCHAR (MAX) NULL,
    [isDuplicated]       BIT           NULL,
    [joiningPointDataID] INT           NULL,
    CONSTRAINT [PK_JoiningResult] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_JoiningResult_JoiningPointData] FOREIGN KEY ([joiningPointDataID]) REFERENCES [PJC].[JoiningPointData] ([id])
);

