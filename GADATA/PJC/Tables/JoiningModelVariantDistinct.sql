CREATE TABLE [PJC].[JoiningModelVariantDistinct] (
    [id]                    INT           IDENTITY (1, 1) NOT NULL,
    [joiningModelVariantId] INT           NULL,
    [_timestamp]            DATETIME      NULL,
    [_lasttimestamp]        DATETIME      NULL,
    [name]                  VARCHAR (MAX) NULL,
    [isDead]                BIT           NULL,
    CONSTRAINT [PK_JoiningModelVariantDistinct] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_JoiningModelVariantDistinct_JoiningModelVariant] FOREIGN KEY ([joiningModelVariantId]) REFERENCES [PJC].[JoiningModelVariant] ([id])
);

