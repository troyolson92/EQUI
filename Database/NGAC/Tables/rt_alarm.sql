CREATE TABLE [NGAC].[rt_alarm] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [controller_id]   INT           NULL,
    [_timestamp]      DATETIME      NULL,
    [error_timestamp] DATETIME      NULL,
    [sequenceNumber]  INT           NULL,
    [number]          INT           NULL,
    [categoryId]      INT           NULL,
    [title]           VARCHAR (MAX) NULL,
    [description]     VARCHAR (MAX) NULL,
    [consequences]    VARCHAR (MAX) NULL,
    [causes]          VARCHAR (MAX) NULL,
    [actions]         VARCHAR (MAX) NULL,
    [type]            VARCHAR (MAX) NULL,
    CONSTRAINT [PK_rt_alarm_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_alarm_c_controller] FOREIGN KEY ([controller_id]) REFERENCES [NGAC].[c_controller] ([id])
);



go
CREATE NONCLUSTERED INDEX [nci_forVASC] ON [NGAC].[rt_alarm]
(
	[controller_id] ASC,
	[sequenceNumber] ASC,
	[categoryId] ASC,
	[error_timestamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [nci_FORVASCSTARTUP]
    ON [NGAC].[rt_alarm]([controller_id] ASC)
    INCLUDE([_timestamp], [error_timestamp], [sequenceNumber]);

