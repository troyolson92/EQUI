print 'init check [EqUi].[c_areas]'
IF (SELECT count(*) FROM  [EqUi].[c_areas]) = 0
BEGIN
Print 'init data insert [EqUi].[c_areas]'
SET IDENTITY_INSERT [EqUi].[c_areas] ON 

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (1, N'GA', N'CMA UB12 floorline', N'A GA1.0 Floor (all)', N'VCG -> A -> A GA1.0 -> A LIJN 33', 10, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (2, N'GA', N'CMA UB12 floorline', N'A GA1.0 Frontstructure', N'VCG -> A -> A GA1.0 -> A LIJN 331', 20, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (3, N'GA', N'CMA UB12 floorline', N'A GA1.0 Frontfloor', N'VCG -> A -> A GA1.0 -> A LIJN 334', 30, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (4, N'GA', N'CMA UB12 floorline', N'A GA1.0 Rearfloor', N'VCG -> A -> A GA1.0 -> A LIJN 336', 40, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (5, N'GA', N'CMA UB12 floorline', N'A GA1.0 Marriage', N'VCG -> A -> A GA1.0 -> A LIJN 338', 50, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (6, N'GA', N'CMA UB12 floorline', N'A GA1.0 Weldbolt', N'VCG -> A -> A GA1.0 -> A LIJN 339', 60, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (7, N'GA', N'CMA Sides', N'A GA1.0 Sides (all)', N'VCG -> A -> A GA1.0 -> A LIJN 35', 70, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (8, N'GA', N'CMA Preassembly', N'A GA4.0 Preassembly (all)', N'VCG -> A -> A GA4.0', 80, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (9, N'GA', N'CMA UB12 floorline', N'A GA1.0 Slings', N'VCG -> A -> A GA1.0 -> A LIJN 337', 90, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (11, N'GA', N'P4 AAS', N'AAS (all)', N'VCG -> A -> AAS', 100, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (12, N'GA', N'P4 AAS', N'A FLOOR S', N'VCG -> A -> AAS -> A FLOOR S', 110, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (13, N'GA', N'P4 AAS', N'A SIBO 2', N'VCG -> A -> AAS -> A SIBO 2', 120, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (14, N'GA', N'Assembly Lines', N'Assembly Lines (all)', N'VCG -> A -> A ASSEMBLY LINES', 130, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (15, N'GA', N'Assembly Lines', N'HOP', N'VCG -> A -> A ASSEMBLY LINES -> A HOP', 140, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (19, N'VCG', N'VCG', N'GA (all)', N'VCG -> A', 2, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (21, N'VCG', N'VCG', N'no filter', N' ', 1, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (22, N'VCG', N'VCG', N'GB (all)', N'VCG -> BGB', 1000, NULL)

INSERT [EqUi].[c_areas] ([id], [Plant], [Optgroup], [Area], [LocationTreeFilter1], [Ordinal], [UserComment]) VALUES (23, N'GA', N'Assembly Lines', N'A FIN', N'VCG -> A -> A ASSEMBLY LINES -> A BOSKI -> A FIN', 150, NULL)

SET IDENTITY_INSERT [EqUi].[c_areas] OFF

END