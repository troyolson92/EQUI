CREATE VIEW LAUNCH.TDT_PPV316_FLOOR
AS
SELECT        dbo.NPT.Name, dbo.Timer.Name AS timer, dbo.Spot.Number, LAUNCH.PPV316.[nom diameter], LAUNCH.PPV316.[diameter PP], 
                         LAUNCH.PPV316.[TDT PP BUILD 30 augustus 2017], LAUNCH.PPV316.[TDT PP comment]
FROM            dbo.Timer INNER JOIN
                         dbo.Spot ON dbo.Timer.ID = dbo.Spot.TimerID AND dbo.Timer.ID = dbo.Spot.TimerID AND dbo.Timer.ID = dbo.Spot.TimerID INNER JOIN
                         dbo.NPT ON dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID INNER JOIN
                         LAUNCH.PPV316 ON dbo.Spot.Number = LAUNCH.PPV316.spot
WHERE        (dbo.Spot.Number BETWEEN 30000 AND 49999) OR
                         (dbo.Spot.Number BETWEEN 4400000 AND 4419999)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'LAUNCH', @level1type = N'VIEW', @level1name = N'TDT_PPV316_FLOOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
End
', @level0type = N'SCHEMA', @level0name = N'LAUNCH', @level1type = N'VIEW', @level1name = N'TDT_PPV316_FLOOR';


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
         Begin Table = "Timer"
            Begin Extent = 
               Top = 6
               Left = 569
               Bottom = 135
               Right = 739
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Spot"
            Begin Extent = 
               Top = 6
               Left = 332
               Bottom = 135
               Right = 531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NPT"
            Begin Extent = 
               Top = 6
               Left = 777
               Bottom = 135
               Right = 947
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PPV316 (LAUNCH)"
            Begin Extent = 
               Top = 41
               Left = 0
               Bottom = 367
               Right = 256
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
         Table = 3720
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 2070
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End', @level0type = N'SCHEMA', @level0name = N'LAUNCH', @level1type = N'VIEW', @level1name = N'TDT_PPV316_FLOOR';

