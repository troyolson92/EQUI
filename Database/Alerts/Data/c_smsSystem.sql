print 'init check [Alerts].[c_smsSystem]'

IF (SELECT count(*) FROM  [Alerts].[c_smsSystem]) = 0
BEGIN
Print 'init data insert [Alerts].[c_smsSystem]'
SET IDENTITY_INSERT [Alerts].[c_smsSystem] ON 
INSERT [Alerts].[c_smsSystem] ([id], [Discription], [system]) VALUES (2, N'NO sms (don''t send anything)', N'NOsms')
INSERT [Alerts].[c_smsSystem] ([id], [Discription], [system]) VALUES (3, N'Debug system (send to sam)', N'DBG_SMS')
SET IDENTITY_INSERT [Alerts].[c_smsSystem] OFF
END