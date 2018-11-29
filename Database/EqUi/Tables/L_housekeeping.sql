CREATE TABLE [EqUi].[L_housekeeping] (
    [id]                INT      IDENTITY (1, 1) NOT NULL,
    [c_housekeeping_id] INT      NOT NULL,
    [timestamp]         DATETIME NOT NULL,
    [nRowCountStart]    INT      NULL,
    [nRowCountEnd]      INT      NULL,
    [nLoopCount]        INT      NULL,
    [nDeleteCount]      INT      NULL,
    [nRunTime]          INT      NULL,
    [nJobID]            INT      NOT NULL,
    CONSTRAINT [PK_L_housekeeping] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_L_housekeeping_c_housekeeping] FOREIGN KEY ([c_housekeeping_id]) REFERENCES [EqUi].[c_housekeeping] ([id])
);

