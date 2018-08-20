print 'init check [NGAC].[c_controller_class]'
IF (SELECT count(*) FROM  [NGAC].[c_controller_class]) = 0
Print 'init data insert [NGAC].[c_controller_class]'
BEGIN
SET IDENTITY_INSERT [NGAC].[c_controller_class] ON 
INSERT [NGAC].[c_controller_class] ([id], [name], [doConnect], [evStateChange], [evOperatingModeChange], [evConnectionChange], [evExecutionStatus], [evExecutionStatusTRob1], [evBackupCompleted], [evDataResolveChange], [evExecutionCycleChange], [evTaskEnabledChange], [evMasterChange], [evMotionPointerTRob1Change], [evProgramPointerTRob1Change], [evMotionPointerTRob1ManualChange], [evProgramPointerTRob1ManualChange], [cVariableMask], [cVariableSearchMask], [cDeviceInfoMask], [cCSVLogMask], [cJobMask], [logCategoryMask], [handleHSocket], [Username], [Password], [setClock], [evLogMessageAction], [cPJVEventMask], [cPJVActionMask], [cErrorMask]) VALUES (2, N'NGAC robots', 1, 12, 12, 8, 12, 12, 0, 0, 0, 0, 8, 0, 0, 1, 1, 1, 1, -1, 1, 65535, -1, 0, N'Default User', N'robotics', 28800000, 4, 0, -1, -1)
SET IDENTITY_INSERT [NGAC].[c_controller_class] OFF
END