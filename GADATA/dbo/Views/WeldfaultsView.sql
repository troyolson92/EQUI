CREATE VIEW dbo.WeldfaultsView
AS
SELECT        dbo.WeldFaultLog.ID, dbo.WeldFaultLog.DateTime, dbo.Spot.Number, dbo.Spot.PlateCombinationtId, dbo.PlateCombinations.Plate1ID, 
                         dbo.PlateCombinations.Plate2ID, dbo.PlateCombinations.Plate4ID, dbo.PlateCombinations.Plate3ID, dbo.Timer.Name, dbo.WeldingGun.Name AS Expr1, 
                         dbo.WeldFaultName.WeldFaultName, dbo.WeldSubFaultName.WeldSubFaultName, dbo.WeldFaultLog.Wear, dbo.WeldFaultLog.Filter, 
                         dbo.WeldFaultLog.WeldMasterComment
FROM            dbo.WeldFaultLog INNER JOIN
                         dbo.Spot ON dbo.WeldFaultLog.SpotID = dbo.Spot.ID INNER JOIN
                         dbo.Timer ON dbo.Spot.TimerID = dbo.Timer.ID INNER JOIN
                         dbo.WeldingGun ON dbo.Timer.ID = dbo.WeldingGun.TimerID INNER JOIN
                         dbo.WeldFaultName ON dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID INNER JOIN
                         dbo.WeldSubFaultName ON dbo.WeldFaultLog.WeldSubFaultID = dbo.WeldSubFaultName.ID INNER JOIN
                         dbo.PlateCombinations ON dbo.Spot.PlateCombinationtId = dbo.PlateCombinations.ID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'WeldfaultsView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'         DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'WeldfaultsView';


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
         Begin Table = "WeldFaultLog"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 224
               Right = 244
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Spot"
            Begin Extent = 
               Top = 13
               Left = 277
               Bottom = 249
               Right = 476
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Timer"
            Begin Extent = 
               Top = 6
               Left = 519
               Bottom = 142
               Right = 689
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "WeldingGun"
            Begin Extent = 
               Top = 126
               Left = 537
               Bottom = 255
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "WeldFaultName"
            Begin Extent = 
               Top = 178
               Left = 462
               Bottom = 273
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "WeldSubFaultName"
            Begin Extent = 
               Top = 190
               Left = 288
               Bottom = 285
               Right = 482
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PlateCombinations"
            Begin Extent = 
               Top = 31
               Left = 498
               Bottom = 160
               Right = 668
            End
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'WeldfaultsView';

