print 'init check [NGAC].[c_variable]'
IF (SELECT count(*) FROM  [NGAC].[c_variable]) = 0
Print 'init data insert [NGAC].[c_variable]'
BEGIN
SET IDENTITY_INSERT [NGAC].[c_variable] ON 
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (1, N'RAPID/T_ROB1/HTPU/SendAl_errwrite', -1, 1, 102, 13, N'[rt_value]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (2, N'IO/GI_Pgno', -1, 1, 100, 12, N'[rt_value]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (3, N'IO/O_SimMode', -1, 0, 0, 0, N'[rt_value]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (4, N'IO/O_Homepos', -1, 1, 103, 12, N'[rt_value]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (5, N'RAPID/SOCKETTASK/HSOCKET/BodyId', -1, 1, 101, 12, N'[rt_value]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (6, N'IO/GO_SpeedOverride', -1, 1, 0, 2, N'[rtu_SpeedOvr]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (7, N'IO/I_Barrel1_LowLevel', -1, 1, 0, 1, N'[rt_Disp_BarrelLow]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (8, N'IO/I_Barrel2_LowLevel', -1, 1, 0, 1, N'[rt_Disp_BarrelLow]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (9, N'RAPID/T_ROB1/LVcgA/VascRegister_1', -1, 1, 200, 1, N'[rt_value]')
INSERT [NGAC].[c_variable] ([id], [variable], [poll_rate], [enable_bit], [event_enum], [sql_action], [rt_table]) VALUES (10, N'RAPID/T_ROB1/DPLC/nBodyCounter', -1, 1, 104, 13, N'[rt_value]')
SET IDENTITY_INSERT [NGAC].[c_variable] OFF
END