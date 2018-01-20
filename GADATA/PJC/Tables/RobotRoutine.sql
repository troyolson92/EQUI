CREATE TABLE [PJC].[RobotRoutine] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [s_name]         VARCHAR (MAX) NULL,
    [programLine]    INT           NULL,
    [_timestamp]     DATETIME      NULL,
    [_lasttimestamp] DATETIME      NULL,
    [isDead]         BIT           NULL,
    [RobotProgramId] INT           NULL,
    CONSTRAINT [PK_RobotRoutine] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_RobotRoutine_RobotProgram] FOREIGN KEY ([RobotProgramId]) REFERENCES [PJC].[RobotProgram] ([id])
);

