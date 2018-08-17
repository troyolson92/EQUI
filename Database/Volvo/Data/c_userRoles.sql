print 'init check [Volvo].[c_datasource]'
IF (SELECT count(*) FROM  [Volvo].[c_userRoles]) = 0
Print 'init data insert [Volvo].[c_datasource]'
BEGIN

--SET IDENTITY_INSERT [Volvo].[c_userRoles] ON 
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'AAOSR', N'robotgroep member')
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'Administrator', N'Admin full power')
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'AlertMaster', N'can make new Alert Triggers and change alerts directly')
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'Editor', N'can contribute')
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'HangFire', N'can make hangfire jobs and control hangefire dash')
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'MAXIMOrealtime', N'can query maximo in realtime (production server)')
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'ScreenManager', N'can do cool stuf to the in Field Screens')
INSERT [Volvo].[c_userRoles] ([Role], [Description]) VALUES (N'VSTOpoweruser', N'can make connections in VSTO excel tool')
--SET IDENTITY_INSERT [Volvo].[c_userRoles] OFF
END