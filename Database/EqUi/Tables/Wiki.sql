CREATE TABLE [EqUi].[Wiki] (
    [Name]             VARCHAR (50)  NOT NULL,
    [Title]            VARCHAR (50)  NULL,
    [Body]             VARCHAR (MAX) NULL,
    [Comments]         VARCHAR (MAX) NULL,
    [createDate]       DATETIME2 (7) NOT NULL,
    [l_user_id]        INT           NOT NULL,
    [ChangeDate]       DATETIME2 (7) NULL,
    [l_change_user_id] INT           NULL,
    CONSTRAINT [PK_Wiki] PRIMARY KEY CLUSTERED ([Name] ASC),
    CONSTRAINT [FK_Wiki_L_users] FOREIGN KEY ([l_user_id]) REFERENCES [Volvo].[L_users] ([id])
);





