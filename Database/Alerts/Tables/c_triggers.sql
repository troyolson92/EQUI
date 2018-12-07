CREATE TABLE [Alerts].[c_triggers] (
    [id]                       INT           IDENTITY (1, 1) NOT NULL,
    [enabled]                  BIT           NOT NULL,
    [discription]              VARCHAR (MAX) NOT NULL,
    [RunAgainst]               INT           NOT NULL,
    [sqlStatement]             VARCHAR (MAX) NULL,
    [smsSystem]                INT           NULL,
    [initial_state]            INT           NOT NULL,
    [c_schedule_id]            INT           NOT NULL,
    [continueOnJobFailure]     BIT           NOT NULL,
    [ordinal]                  INT           NOT NULL,
    [interval]                 INT           NOT NULL,
    [intervalCounter]          INT           NOT NULL,
    [alertGroup]               VARCHAR (50)  NULL,
    [alertType]                VARCHAR (50)  NOT NULL,
    [AutoSetStateTechComp]     BIT           NOT NULL,
    [smsOnRetrigger]           BIT           NOT NULL,
    [enableSMS]                BIT           NOT NULL,
    [isDowntime]               BIT           NOT NULL,
    [isInReport]               BIT           NOT NULL,
    [hasControlLimits]         BIT           NOT NULL,
    [controllimitSqlStatement] VARCHAR (MAX) NULL,
    [controlChartSqlStatement] VARCHAR (MAX) NULL,
    [Animation]                VARCHAR (50)  NULL,
    [isDebugmode]              BIT           NOT NULL,
    [c_datasource_id]          INT           NOT NULL,
    [controlChartYlabel]       VARCHAR (MAX) NULL,
    [OptValueLabels]           VARCHAR (MAX) NULL,
    CONSTRAINT [PK_c_triggers] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_c_triggers_c_datasource] FOREIGN KEY ([c_datasource_id]) REFERENCES [EqUi].[c_datasource] ([Id]),
    CONSTRAINT [FK_c_triggers_c_schedule] FOREIGN KEY ([c_schedule_id]) REFERENCES [EqUi].[c_schedule] ([id]),
    CONSTRAINT [FK_c_triggers_c_smsSystem] FOREIGN KEY ([smsSystem]) REFERENCES [Alerts].[c_smsSystem] ([id]),
    CONSTRAINT [FK_c_triggers_c_state] FOREIGN KEY ([initial_state]) REFERENCES [Alerts].[c_state] ([id])
);





