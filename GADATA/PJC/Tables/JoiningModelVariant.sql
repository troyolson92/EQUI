﻿CREATE TABLE [PJC].[JoiningModelVariant] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [joiningFileId]  INT           NULL,
    [_timestamp]     DATETIME      NULL,
    [_lasttimestamp] DATETIME      NULL,
    [name]           VARCHAR (MAX) NULL,
    [itemId]         VARCHAR (MAX) NULL,
    [specification]  VARCHAR (MAX) NULL,
    [spotWeldId]     VARCHAR (MAX) NULL,
    [process]        VARCHAR (20)  NULL,
    [isDead]         BIT           NULL,
    CONSTRAINT [PK_JoiningModelVariant] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_JoiningModelVariant_JoiningFile] FOREIGN KEY ([joiningFileId]) REFERENCES [PJC].[JoiningFile] ([id])
);

