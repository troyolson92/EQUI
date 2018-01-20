CREATE TABLE [NGAC2].[x_reAlarms] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [controller_id]   INT           NULL,
    [Title]           VARCHAR (MAX) NULL,
    [Number]          INT           NULL,
    [SequenceNumber]  INT           NULL,
    [CategoryId]      INT           NOT NULL,
    [Description]     VARCHAR (MAX) NULL,
    [Consequences]    VARCHAR (MAX) NULL,
    [Causes]          VARCHAR (MAX) NULL,
    [Action]          VARCHAR (MAX) NULL,
    [Type]            VARCHAR (MAX) NULL,
    [error_timestamp] DATETIME      NULL,
    [_timestamp]      DATETIME      NULL,
    [why]             VARCHAR (MAX) NULL
);

