print 'init check [NGAC].[c_csv_log]'
IF (SELECT count(*) FROM  [NGAC].[c_csv_log]) = 0
BEGIN
Print 'init data insert [NGAC].[c_csv_log]'
SET IDENTITY_INSERT [NGAC].[c_csv_log] ON 
INSERT [NGAC].[c_csv_log] ([id], [enable_bit], [csv_filename], [logcount_variable], [rt_table], [poll_rate], [comment], [Tempnote], [flags]) VALUES (2, 1, N'ErrDispLog', N'RAPID/T_ROB1/HTPU/logCounterEnable', N'rt_ErrDispLog', 60000, N'All error display on the TPU using the HTPU', N'OK', 1)
INSERT [NGAC].[c_csv_log] ([id], [enable_bit], [csv_filename], [logcount_variable], [rt_table], [poll_rate], [comment], [Tempnote], [flags]) VALUES (3, 1, N'TipDressLogFile', N'RAPID/T_ROB1/HDress/TipDressLogCounter', N'rt_TipDressLogFile', -1, N'TipDressData For robot mounted guns', N'OK', 1)
INSERT [NGAC].[c_csv_log] ([id], [enable_bit], [csv_filename], [logcount_variable], [rt_table], [poll_rate], [comment], [Tempnote], [flags]) VALUES (4, 1, N'TCP_log', N'RAPID/T_ROB1/BEAM/LogCounterBeam', N'rt_TCP_LOG', -1, N'log of the TCP measurement system.', N'NOK *enkel nodig voor stud en glue (spot komt van tipdress)', 1)
INSERT [NGAC].[c_csv_log] ([id], [enable_bit], [csv_filename], [logcount_variable], [rt_table], [poll_rate], [comment], [Tempnote], [flags]) VALUES (5, 1, N'BeamSetUpLog', N'RAPID/T_ROB1/BEAM/logCounterSetUpBeam', N'rt_BeamSetUpLog', -1, N'log of SBCU setup', N'SKIP *gewoon loggen geen frontend nodig nu', 1)
INSERT [NGAC].[c_csv_log] ([id], [enable_bit], [csv_filename], [logcount_variable], [rt_table], [poll_rate], [comment], [Tempnote], [flags]) VALUES (6, 1, N'TipDressLogFile', N'RAPID/T_ROB1/HMeas/TipDressLogCounter', N'rt_TipDressLogFile', -1, N'TipDressData For robot FLOOR guns (added 17w44 afther FP update before was common with robot mounted)', N'TESt', 1)
SET IDENTITY_INSERT [NGAC].[c_csv_log] OFF
END