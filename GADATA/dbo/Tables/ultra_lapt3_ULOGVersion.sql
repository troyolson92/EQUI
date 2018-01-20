CREATE TABLE [dbo].[ultra_lapt3_ULOGVersion] (
    [Version] INT NULL
);


GO
CREATE NONCLUSTERED INDEX [ULOGVERSION]
    ON [dbo].[ultra_lapt3_ULOGVersion]([Version] ASC);

