CREATE TABLE [EqUi].[c_housekeeping] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [c_schedule_id]    INT           NOT NULL,
    [c_datasource_id]  INT           NULL,
    [Ordinal]          INT           NOT NULL,
    [Name]             VARCHAR (50)  NOT NULL,
    [Description]      VARCHAR (MAX) NULL,
    [SchemaName]       VARCHAR (50)  NOT NULL,
    [TableName]        VARCHAR (50)  NOT NULL,
    [nDaysKeepHistory] INT           NOT NULL,
    [nDeleteBatchSize] INT           NOT NULL,
    [nMaxRunTime]      INT           NOT NULL,
    [IdColName]        VARCHAR (50)  NOT NULL,
    [DateTimeColName]  VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_c_housekeeping] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_c_housekeeping_c_datasource] FOREIGN KEY ([c_datasource_id]) REFERENCES [EqUi].[c_datasource] ([Id]),
    CONSTRAINT [FK_c_housekeeping_c_schedule] FOREIGN KEY ([c_schedule_id]) REFERENCES [EqUi].[c_schedule] ([id])
);

