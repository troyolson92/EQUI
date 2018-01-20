CREATE TABLE [NGAC2].[h_alarm] (
    [id]              INT      IDENTITY (1, 1) NOT NULL,
    [controller_id]   INT      NULL,
    [_timestamp]      DATETIME NULL,
    [error_timestamp] DATETIME NULL,
    [SequenceNumber]  INT      NULL,
    [L_error_id]      INT      NULL,
    CONSTRAINT [PK_h_alarm_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_h_alarm_c_controller] FOREIGN KEY ([controller_id]) REFERENCES [NGAC2].[c_controller] ([id]),
    CONSTRAINT [FK_h_alarm_L_error] FOREIGN KEY ([L_error_id]) REFERENCES [NGAC2].[L_error] ([_id])
);


GO
CREATE NONCLUSTERED INDEX [IDX_Controller_Sequencenumber]
    ON [NGAC2].[h_alarm]([controller_id] ASC, [SequenceNumber] ASC);

