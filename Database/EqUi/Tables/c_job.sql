CREATE TABLE [EqUi].[c_job] (
    [id]                   INT           IDENTITY (1, 1) NOT NULL,
    [enabled]              BIT           NOT NULL,
    [name]                 VARCHAR (50)  NOT NULL,
    [description]          VARCHAR (MAX) NOT NULL,
    [c_schedule_id]        INT           NOT NULL,
    [c_datasource_id]      INT           NOT NULL,
    [sqlCommand]           VARCHAR (MAX) NOT NULL,
    [ordinal]              INT           NOT NULL,
    [interval]             INT           NOT NULL,
    [intervalCounter]      INT           NOT NULL,
    [continueOnJobFailure] BIT           NOT NULL,
    [maxRuntime]           INT           NULL,
    [warnRuntime]          INT           NULL,
    CONSTRAINT [PK_c_job_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_c_job_c_datasource] FOREIGN KEY ([c_datasource_id]) REFERENCES [EqUi].[c_datasource] ([Id]),
    CONSTRAINT [FK_c_job_c_schedule] FOREIGN KEY ([c_schedule_id]) REFERENCES [EqUi].[c_schedule] ([id])
);

