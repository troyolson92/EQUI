CREATE VIEW WELDING.ExtraUltralogInspections_WMuitadaptief
AS
SELECT        dbo.Timer.Name AS [Timer/locatie], WELDING.ExtraUltralogUitadaptief2.spot AS Spotnummer, WELDING.ExtraUltralogUitadaptief1.Name AS Ultralogplan, 
                         WELDING.ExtraUltralogUitadaptief1.IndexOfTestSeq AS plannummer, dbo.NPT.Name, dbo.ResponsibilityWM.responsibilityWM
FROM            WELDING.Qcontrol INNER JOIN
                         dbo.Timer ON WELDING.Qcontrol.Timer = dbo.Timer.Name INNER JOIN
                         WELDING.ExtraUltralogUitadaptief1 INNER JOIN
                         WELDING.ExtraUltralogUitadaptief2 ON WELDING.ExtraUltralogUitadaptief1.[last inspection] = WELDING.ExtraUltralogUitadaptief2.Expr1 ON 
                         WELDING.Qcontrol.spot = WELDING.ExtraUltralogUitadaptief2.spot INNER JOIN
                         dbo.NPT ON dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID INNER JOIN
                         dbo.ResponsibilityWM ON dbo.NPT.Name = dbo.ResponsibilityWM.NPT
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'WELDING', @level1type = N'VIEW', @level1name = N'ExtraUltralogInspections_WMuitadaptief';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 4635
         Width = 1500
         Width = 2235
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2460
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
', @level0type = N'SCHEMA', @level0name = N'WELDING', @level1type = N'VIEW', @level1name = N'ExtraUltralogInspections_WMuitadaptief';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[31] 2[20] 3) )"
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
         Begin Table = "ExtraUltralogUitadaptief1 (WELDING)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 186
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ExtraUltralogUitadaptief2 (WELDING)"
            Begin Extent = 
               Top = 24
               Left = 305
               Bottom = 218
               Right = 475
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Timer"
            Begin Extent = 
               Top = 52
               Left = 729
               Bottom = 229
               Right = 899
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Qcontrol (WELDING)"
            Begin Extent = 
               Top = 27
               Left = 463
               Bottom = 156
               Right = 662
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NPT"
            Begin Extent = 
               Top = 6
               Left = 937
               Bottom = 135
               Right = 1107
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ResponsibilityWM"
            Begin Extent = 
               Top = 57
               Left = 1116
               Bottom = 169
               Right = 1297
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
   ', @level0type = N'SCHEMA', @level0name = N'WELDING', @level1type = N'VIEW', @level1name = N'ExtraUltralogInspections_WMuitadaptief';

