﻿print 'init check [EqUi].[c_controller]'
IF (SELECT count(*) FROM  [NGAC].[c_controller]) = 0
BEGIN
Print 'init data insert [EqUi].[c_controller]'
SET IDENTITY_INSERT [NGAC].[c_controller] ON 

INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (1, N'336061R01', 1, N'dbe90ef8-e535-4991-a727-3883ec4de0f9', N'10.205.42.240', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336061 -> 336061R01', N'URA-0195', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 0)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (2, N'336061R02', 1, N'87b5142f-f5ed-4ab7-a019-fe79cff95ad3', N'10.205.42.230', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336061 -> 336061R02', N'URA-0196', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 1)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (3, N'336061R03', 1, N'313ff889-c574-419d-a203-c0b9cab82572', N'10.205.42.220', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336061 -> 336061R03', N'URA-0197', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 1)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (4, N'336061R04', 1, N'f55cb0e2-a6a2-4cec-9ad8-b21fb336d11b', N'10.205.42.210', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336061 -> 336061R04', N'URA-0198', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 1)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (5, N'336062R01', 1, N'5d198cd7-d93a-40a1-929e-fbd55b4df626', N'10.205.42.200', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336062 -> 336062R01', N'URA-0199', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 0)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (6, N'336062R02', 1, N'377286f2-53c8-41a3-9115-0978f0b1679b', N'10.205.42.190', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336062 -> 336062R02', N'URA-0200', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 0)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (7, N'336062R03', 1, N'c0bcc47c-b6c3-49af-be08-6bfa3b6b62df', N'10.205.42.180', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336062 -> 336062R03', N'URA-0201', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 1)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (8, N'336062R04', 1, N'd0e8eff8-e514-42aa-a809-fd583cfd1afb', N'10.205.42.170', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336062 -> 336062R04', N'URA-0202', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 1)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (9, N'336062R05', 1, N'9145e213-b951-4ee4-b0c6-3c4451b2a814', N'10.205.42.160', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336062 -> 336062R05', N'URA-0203', N'A TEAM 17', NULL, NULL, NULL, N'URA', 0, 1)
INSERT [NGAC].[c_controller] ([id], [controller_name], [enable_bit], [systemId], [ip], [class_id], [flags], [LocationTree], [Assetnum], [ProductionTeam], [ResponsibleTechnicianTeam], [ResponsibleProductionTeam], [ClassificationTree], [CLassificationId], [hasRackidAsBodynum], [hasSpotweld]) VALUES (10, N'336062R06', 1, N'76b1c9cc-7fac-4e07-95bc-ec45980ec248', N'10.205.42.150', 2, 1, N'VCG -> A -> A GA1.0 -> A LIJN 336 -> A STN336062 -> 336062R06', N'URA-0204', N'A TEAM 17', NULL, NULL, NULL, N'URA', 1, 1)
SET IDENTITY_INSERT [NGAC].[c_controller] OFF
END