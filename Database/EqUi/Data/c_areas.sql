﻿print 'init check [EqUi].[c_areas]'
IF (SELECT count(*) FROM  [EqUi].[c_areas]) = 0
Print 'init data insert [EqUi].[c_areas]'
BEGIN
SET IDENTITY_INSERT [EqUi].[c_areas] ON 
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (1, N'GA', N'CMA UB12 floorline', N'A GA1.0 Floor (all)', N'VCG -> A -> A GA1.0 -> A LIJN 33', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (2, N'GA', N'CMA UB12 floorline', N'A GA1.0 Frontstructure', N'VCG -> A -> A GA1.0 -> A LIJN 331', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (3, N'GA', N'CMA UB12 floorline', N'A GA1.0 Frontfloor', N'VCG -> A -> A GA1.0 -> A LIJN 334', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (4, N'GA', N'CMA UB12 floorline', N'A GA1.0 Rearfloor', N'VCG -> A -> A GA1.0 -> A LIJN 336', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (5, N'GA', N'CMA UB12 floorline', N'A GA1.0 Marriage', N'VCG -> A -> A GA1.0 -> A LIJN 338', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (6, N'GA', N'CMA UB12 floorline', N'A GA1.0 Weldbolt', N'VCG -> A -> A GA1.0 -> A LIJN 339', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (7, N'GA', N'CMA Sides', N'A GA1.0 Sides (all)', N'VCG -> A -> A GA1.0 -> A LIJN 35', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (8, N'GA', N'CMA Preassembly', N'A GA4.0 Preassembly (all)', N'VCG -> A -> A GA4.0', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (9, N'GA', N'CMA UB12 floorline', N'A GA1.0 Slings', N'VCG -> A -> A GA1.0 -> A LIJN 337', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (11, N'GA', N'P4 AAS', N'AAS (all)', N'VCG -> A -> AAS', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (12, N'GA', N'P4 AAS', N'A FLOOR S', N'VCG -> A -> AAS -> A FLOOR S', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (13, N'GA', N'P4 AAS', N'A SIBO S', N'VCG -> A -> AAS -> A SIBO S', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (14, N'GA', N'Assembly Lines', N'Assembly Lines (all)', N'VCG -> A -> A ASSEMBLY LINES', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (15, N'GA', N'Assembly Lines', N'HOP', N'VCG -> A -> A ASSEMBLY LINES -> A HOP', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (19, N'GA', N'GA', N'GA (all)', N'VCG -> A', NULL, NULL, NULL, NULL)
INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [LocationTreeFilter2], [LocationTreeFilter3], [LocationTreeFilter4], [UserComment]) VALUES (21, N'VCC', N'no filter', N'no filter', N' ', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [EqUi].[c_areas] OFF
END