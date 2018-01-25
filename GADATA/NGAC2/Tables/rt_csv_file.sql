﻿CREATE TABLE [NGAC2].[rt_csv_file] (
    [id]              INT      IDENTITY (1, 1) NOT NULL,
    [c_controller_id] INT      NULL,
    [c_csv_log_id]    INT      NULL,
    [lastDateRecord]  DATETIME NULL,
    [lastLineRead]    INT      NULL,
    [lastFileSize]    INT      NULL,
    [_timestamp]      DATETIME NULL,
    [lastStreamPos]   INT      NULL,
    CONSTRAINT [PK_rt_csv_file] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_csv_file_c_controller] FOREIGN KEY ([c_controller_id]) REFERENCES [NGAC2].[c_controller] ([id]),
    CONSTRAINT [FK_rt_csv_file_c_csv_log] FOREIGN KEY ([c_csv_log_id]) REFERENCES [NGAC2].[c_csv_log] ([id])
);
