CREATE TABLE [EqUi].[L_link] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [L_pannel_id] INT           NULL,
    [name]        VARCHAR (50)  NOT NULL,
    [url]         VARCHAR (MAX) NULL,
    [description] VARCHAR (MAX) NULL,
    [iconcss]     VARCHAR (50)  NULL,
    [icon]        VARCHAR (50)  NULL,
    [Wiki_id]     INT           NULL,
    [helptext]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_L_links] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_L_link_L_pannel] FOREIGN KEY ([L_pannel_id]) REFERENCES [EqUi].[L_pannel] ([id])
);



