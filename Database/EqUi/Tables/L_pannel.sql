CREATE TABLE [EqUi].[L_pannel] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50)  NOT NULL,
    [description] VARCHAR (MAX) NULL,
    [collapsed]   BIT           NOT NULL,
    [HeaderCss]   VARCHAR (50)  NULL,
    CONSTRAINT [PK_L_pannel] PRIMARY KEY CLUSTERED ([id] ASC)
);

