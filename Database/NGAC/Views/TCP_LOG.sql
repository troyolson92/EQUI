﻿/*only the most recent setup ! */
CREATE VIEW NGAC.TCP_LOG
AS
SELECT        c.controller_name, c.LocationTree, c.ClassificationTree, SUBSTRING(c.controller_name, 0, CHARINDEX('R', c.controller_name)) + REPLACE(REPLACE(REPLACE(REPLACE(rt.[Tool Name], 'Doser', ''), 'Stud', ''), 'Gun', ''), 'TCP', '') 
                         AS 'Toolname', c.controller_name + '_gun' + CAST(rt.[SetUp No] AS varchar(MAX)) AS 'alarmobject', setup.[Meas TCP X] AS 'SetupTCP_X', setup.[Meas TCP Y] AS 'SetupTCP_Y', setup.[Meas TCP Z] AS 'SetupTCP_Z', 
                         EqUi.DistanceBetweenPoints(rt.[Old TCP X] + rt.[Tool Diff X], rt.[Old TCP Y] + rt.[Tool Diff Y], rt.[Old TCP Z] + rt.[Tool Diff Z], setup.[Meas TCP X], setup.[Meas TCP Y], setup.[Meas TCP Z]) AS 'DeltaRef', rt.id, rt.rt_csv_file_id, 
                         rt.[Date Time], rt.[Tool Name], rt.[SetUp No], rt.[Tool Diff X], rt.[Tool Diff Y], rt.[Tool Diff Z], rt.[Tool Tol X], rt.[Tool Tol Y], rt.[Tool Tol Z], rt.[Old TCP X], rt.[Old TCP Y], rt.[Old TCP Z], rt.[New TCP X], rt.[New TCP Y], rt.[New TCP Z], 
                         rt.Action, rt._timestamp
FROM            NGAC.rt_TCP_LOG AS rt LEFT OUTER JOIN
                         NGAC.rt_csv_file AS rt_csv ON rt.rt_csv_file_id = rt_csv.id LEFT OUTER JOIN
                         NGAC.c_controller AS c ON c.id = rt_csv.c_controller_id LEFT OUTER JOIN
                         NGAC.L_BeamSetUpLog AS setup ON setup.c_controller_id = c.id AND setup.SetupNo = rt.[SetUp No] AND setup.rnDesc = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TCP_LOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "rt"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rt_csv"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 420
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 458
               Bottom = 136
               Right = 699
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "setup"
            Begin Extent = 
               Top = 6
               Left = 737
               Bottom = 136
               Right = 914
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TCP_LOG';





