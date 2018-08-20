CREATE TABLE [NGAC].[rt_device_info] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [c_controller_id]  INT           NULL,
    [c_device_info_id] INT           NULL,
    [_timestamp]       DATETIME      NULL,
    [value]            VARCHAR (MAX) NULL,
    CONSTRAINT [PK_rt_device_info] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_device_info_c_controller] FOREIGN KEY ([c_controller_id]) REFERENCES [NGAC].[c_controller] ([id]),
    CONSTRAINT [FK_rt_device_info_c_device_info] FOREIGN KEY ([c_device_info_id]) REFERENCES [NGAC].[c_device_info] ([id])
);




GO
CREATE NONCLUSTERED INDEX [nci_FORVASCSTARTUP]
    ON [NGAC].[rt_device_info]([c_controller_id] ASC, [c_device_info_id] ASC)
    INCLUDE([_timestamp], [value]);

