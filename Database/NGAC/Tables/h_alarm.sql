CREATE TABLE [NGAC].[h_alarm] (
    [id]              INT      IDENTITY (1, 1) NOT NULL,
    [controller_id]   INT      NULL,
    [_timestamp]      DATETIME NULL,
    [error_timestamp] DATETIME NULL,
    [SequenceNumber]  INT      NULL,
    [L_error_id]      INT      NULL,
    [CategoryId]      INT      NULL,
    CONSTRAINT [PK_h_alarm_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_h_alarm_c_controller] FOREIGN KEY ([controller_id]) REFERENCES [NGAC].[c_controller] ([id]),
    CONSTRAINT [FK_h_alarm_L_error] FOREIGN KEY ([L_error_id]) REFERENCES [NGAC].[L_error] ([_id])
);

go

CREATE NONCLUSTERED INDEX [nci_controller_id_SequenceNumber] ON [NGAC].[h_alarm]
(
	[controller_id] ASC,
	[SequenceNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO