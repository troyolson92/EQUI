CREATE TABLE [dbo].[ultral_lapt3_ULOGVersion] (
    [Version] INT NULL
);


GO
CREATE NONCLUSTERED INDEX [ULOGVERSION]
    ON [dbo].[ultral_lapt3_ULOGVersion]([Version] ASC);

