﻿CREATE TABLE [EqUi].[QUERYParameters] (
    [SYSTEM]      VARCHAR (20)  NOT NULL,
    [NAME]        VARCHAR (50)  NOT NULL,
    [SETNAME]     VARCHAR (50)  NOT NULL,
    [DISCRIPTION] VARCHAR (MAX) NULL,
    [Parameter]   VARCHAR (50)  NOT NULL,
    [Value]       VARCHAR (50)  NULL,
    CONSTRAINT [pkQUERYParameters] PRIMARY KEY CLUSTERED ([SYSTEM] ASC, [NAME] ASC, [SETNAME] ASC, [Parameter] ASC)
);

