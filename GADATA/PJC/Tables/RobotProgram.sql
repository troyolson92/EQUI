CREATE TABLE [PJC].[RobotProgram] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [p_name]         VARCHAR (50)  NULL,
    [isDead]         BIT           NULL,
    [_timestamp]     DATETIME      NULL,
    [_lasttimestamp] DATETIME      NULL,
    [filename]       VARCHAR (MAX) NULL,
    [robotDetailId]  INT           NULL,
    CONSTRAINT [PK_RobotProgram] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_RobotProgram_RobotDetail] FOREIGN KEY ([robotDetailId]) REFERENCES [PJC].[RobotDetail] ([id])
);

