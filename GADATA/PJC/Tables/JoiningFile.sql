CREATE TABLE [PJC].[JoiningFile] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [_timestamp]       DATETIME      NULL,
    [_lasttimestamp]   DATETIME      NULL,
    [path]             VARCHAR (MAX) NULL,
    [name]             VARCHAR (MAX) NULL,
    [fileDateTime]     DATETIME      NULL,
    [createDateTime]   DATETIME      NULL,
    [modifiedDateTime] DATETIME      NULL,
    [propertyInfo]     VARCHAR (MAX) NULL,
    [isDead]           BIT           NULL,
    CONSTRAINT [PK_JoiningFile] PRIMARY KEY CLUSTERED ([id] ASC)
);

