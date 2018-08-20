print 'init check [Alerts].[c_state]'
IF (SELECT count(*) FROM  [Alerts].[c_state]) = 0
BEGIN
Print 'init data insert [Alerts].[c_state]'
SET IDENTITY_INSERT [Alerts].[c_state] ON
INSERT [Alerts].[c_state] ([id], [discription], [state]) VALUES (1, N'WGK', N'WGK')
INSERT [Alerts].[c_state] ([id], [discription], [state]) VALUES (2, N'OKREQ', N'OKREQ')
INSERT [Alerts].[c_state] ([id], [discription], [state]) VALUES (3, N'COMP', N'COMP')
INSERT [Alerts].[c_state] ([id], [discription], [state]) VALUES (4, N'VOID', N'VOID')
INSERT [Alerts].[c_state] ([id], [discription], [state]) VALUES (5, N'TECHOMP', N'TECHCOMP')
SET IDENTITY_INSERT [Alerts].[c_state] OFF
END