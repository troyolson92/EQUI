CREATE TABLE [dbo].[ultralog_lapt3_ULOGVersion] (
    [Version] INT NULL
);


GO
CREATE NONCLUSTERED INDEX [ULOGVERSION]
    ON [dbo].[ultralog_lapt3_ULOGVersion]([Version] ASC);

