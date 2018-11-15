CREATE TABLE [EqUi].[Wiki] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (50)  NULL,
    [Description] VARCHAR (50)  NULL,
    [Culture]     VARCHAR (50)  NULL,
    [wiki]        VARCHAR (MAX) NULL,
    [searchtags]  VARCHAR (MAX) NULL,
    [Comments]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Wiki] PRIMARY KEY CLUSTERED ([id] ASC)
);



