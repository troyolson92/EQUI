﻿CREATE TABLE [EqUi].[QUERYS] (
    [SYSTEM]      VARCHAR (20)  NOT NULL,
    [NAME]        VARCHAR (50)  NOT NULL,
    [DISCRIPTION] VARCHAR (MAX) NULL,
    [QUERY]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_QUERYS] PRIMARY KEY CLUSTERED ([SYSTEM] ASC, [NAME] ASC)
);
