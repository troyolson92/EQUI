﻿CREATE TABLE [PJV].[SummaryResult] (
    [id]                          INT           IDENTITY (1, 1) NOT NULL,
    [isDead]                      INT           NULL,
    [_timestamp]                  DATETIME      NULL,
    [_lasttimestamp]              DATETIME      NULL,
    [numberPrograms]              INT           NULL,
    [numberRoutines]              INT           NULL,
    [numberPointInstructions]     INT           NOT NULL,
    [AvgDX]                       FLOAT (53)    NULL,
    [AvgDY]                       FLOAT (53)    NULL,
    [AvgDZ]                       FLOAT (53)    NULL,
    [AvgDelta]                    FLOAT (53)    NULL,
    [StdDelta]                    FLOAT (53)    NULL,
    [numberOfWarnings]            INT           NULL,
    [numberOfProcessDataWarnings] INT           NULL,
    [robotDetailId]               INT           NULL,
    [comment]                     VARCHAR (MAX) NULL,
    [rev_created]                 INT           NULL,
    [rev_modified]                INT           NULL,
    [numberMatchingJPDPP]         INT           NULL,
    [maxDX]                       FLOAT (53)    NULL,
    [maxDY]                       FLOAT (53)    NULL,
    [maxDZ]                       FLOAT (53)    NULL,
    CONSTRAINT [PK_SummaryResult] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_SummaryResult_RobotDetail] FOREIGN KEY ([robotDetailId]) REFERENCES [PJV].[RobotDetail] ([id])
);

