print 'init check [EqUi].[c_schedule]'
IF (SELECT count(*) FROM  [EqUi].c_schedule) = 0
BEGIN
Print 'init data insert [EqUi].[c_schedule]'
SET IDENTITY_INSERT [EqUi].[c_schedule] ON 
INSERT [EqUi].[c_schedule] ([id], [enabled], [name], [description], [jcron], [runContinues], [minRunInterval]) VALUES (4, 1, N'Alerts1Min', N'Alerts1Min trigger', N'* * * * *', 0, 0)
INSERT [EqUi].[c_schedule] ([id], [enabled], [name], [description], [jcron], [runContinues], [minRunInterval]) VALUES (5, 1, N'Alerts5Min', N'Alerts5Min', N'*/5 * * * *', 0, 0)
SET IDENTITY_INSERT [EqUi].[c_schedule] OFF
END