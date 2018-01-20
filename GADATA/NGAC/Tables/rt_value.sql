CREATE TABLE [NGAC].[rt_value] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [c_variable_id]   INT           NULL,
    [c_controller_id] INT           NULL,
    [_timestamp]      DATETIME      NULL,
    [value]           VARCHAR (MAX) NULL,
    [isEvent]         BIT           NULL,
    [abbDateTime]     DATETIME      NULL
);


GO
CREATE NONCLUSTERED INDEX [_dta_index_rt_value_23_211440473__K2_K4_K3_K1_5]
    ON [NGAC].[rt_value]([c_variable_id] ASC, [_timestamp] ASC, [c_controller_id] ASC, [id] ASC)
    INCLUDE([value]);


GO
CREATE STATISTICS [_dta_stat_211440473_3_2_4_1]
    ON [NGAC].[rt_value]([c_controller_id], [c_variable_id], [_timestamp], [id]);


GO
CREATE STATISTICS [_dta_stat_211440473_4_1_3]
    ON [NGAC].[rt_value]([_timestamp], [id], [c_controller_id]);


GO
CREATE STATISTICS [_dta_stat_211440473_3_4]
    ON [NGAC].[rt_value]([c_controller_id], [_timestamp]);


GO
CREATE STATISTICS [_dta_stat_211440473_4_2]
    ON [NGAC].[rt_value]([_timestamp], [c_variable_id]);

