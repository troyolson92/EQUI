print 'init check [NGAC].[c_variable_search]'
IF (SELECT count(*) FROM  [NGAC].[c_variable_search]) = 0
Print 'init data insert [NGAC].[c_variable_search]'
BEGIN
SET IDENTITY_INSERT [NGAC].[c_variable_search] ON 
INSERT [NGAC].[c_variable_search] ([id], [variable], [property], [datatype], [poll_rate], [enable_bit], [insert_update], [rt_table]) VALUES (2, N'RAPID/T_ROB1/^Version.*', 16, N'string', 0, 1, 0, N'[rt_version_info]')
INSERT [NGAC].[c_variable_search] ([id], [variable], [property], [datatype], [poll_rate], [enable_bit], [insert_update], [rt_table]) VALUES (3, N'RAPID/T_ROB1/^vers.*', 64, N'string', 0, 1, 0, N'[rt_version_info]')
SET IDENTITY_INSERT [NGAC].[c_variable_search] OFF
END