CREATE VIEW dbo.Spatters_Zone_Y413
AS
SELECT        dbo.WeldMeasurements.Date, dbo.Timer.Name AS Timer, dbo.WeldMeasurements.NbrWeld, dbo.Spot.Number, CONVERT(DECIMAL(5, 1), 
                         100 * CAST(dbo.WeldMeasurements.NbrSplash AS decimal(10, 5)) / CAST(dbo.WeldMeasurements.NbrWeld AS decimal(10, 5))) AS Spatters, dbo.NPT.Name, 
                         dbo.Spot.Zone, T.Vday, T.Vweek, T.Vyear
FROM            dbo.WeldMeasurements INNER JOIN
                         dbo.Spot ON dbo.WeldMeasurements.SpotId = dbo.Spot.ID INNER JOIN
                         dbo.Timer ON dbo.Spot.TimerID = dbo.Timer.ID INNER JOIN
                         dbo.NPT ON dbo.Timer.NptId = dbo.NPT.ID LEFT OUTER JOIN
                         Volvo.L_timeline AS T ON dbo.WeldMeasurements.Date BETWEEN T.starttime AND T.endtime
WHERE        (dbo.WeldMeasurements.NbrWeld <> 0) AND (dbo.Spot.Zone <> 0) AND (dbo.WeldMeasurements.Date >= CONVERT(DATETIME, '2016-01-01 00:00:00', 102)) AND 
                         (dbo.Spot.Number BETWEEN 70000 AND 79999)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Spatters_Zone_Y413';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      Begin ColumnWidths = 11
         Column = 2520
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 2340
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Spatters_Zone_Y413';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[20] 4[26] 2[10] 3) )"
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
         Begin Table = "WeldMeasurements"
            Begin Extent = 
               Top = 0
               Left = 38
               Bottom = 315
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Spot"
            Begin Extent = 
               Top = 4
               Left = 357
               Bottom = 170
               Right = 556
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Timer"
            Begin Extent = 
               Top = 0
               Left = 730
               Bottom = 158
               Right = 900
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NPT"
            Begin Extent = 
               Top = 0
               Left = 942
               Bottom = 193
               Right = 1114
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "T"
            Begin Extent = 
               Top = 0
               Left = 1181
               Bottom = 172
               Right = 1351
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Spatters_Zone_Y413';

