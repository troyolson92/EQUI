print 'init check [NGAC].[c_job]'
IF (SELECT count(*) FROM  [NGAC].[c_job]) = 0
BEGIN
Print 'init data insert [NGAC].[c_job]'
SET IDENTITY_INSERT [NGAC].[c_job] ON 
INSERT [NGAC].[c_job] ([id], [jobNumber], [enable_bit], [flags], [comment]) VALUES (2, 440002, 1, 7, N'V316Common - No sun roof')
INSERT [NGAC].[c_job] ([id], [jobNumber], [enable_bit], [flags], [comment]) VALUES (4, 440003, 1, 7, N'V316Common - With sun roof')
INSERT [NGAC].[c_job] ([id], [jobNumber], [enable_bit], [flags], [comment]) VALUES (6, -1, 1, 7, N'Any other model')
SET IDENTITY_INSERT [NGAC].[c_job] OFF
END