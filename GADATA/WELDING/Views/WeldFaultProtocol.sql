CREATE VIEW WELDING.WeldFaultProtocol
AS
SELECT        dbo.NPT.Name AS NPT, WELDING.Weldfaults.Datetime, WELDING.Weldfaults.Timer, WELDING.Weldfaults.program, WELDING.Weldfaults.spot, 
                         WELDING.Weldfaults.TimerFault, WELDING.Weldfaults.WeldmasterComment, Volvo.L_timeline.Vyear, Volvo.L_timeline.Vweek, Volvo.L_timeline.Vday, 
                         WELDING.Weldfaults.ID, dbo.ResponsibilityWM.responsibilityWM
FROM            Volvo.L_timeline LEFT OUTER JOIN
                         WELDING.Weldfaults ON WELDING.Weldfaults.Datetime BETWEEN Volvo.L_timeline.starttime AND Volvo.L_timeline.endtime INNER JOIN
                         dbo.Timer INNER JOIN
                         dbo.NPT ON dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID ON WELDING.Weldfaults.Timer = dbo.Timer.Name INNER JOIN
                         dbo.ResponsibilityWM ON dbo.NPT.Name = dbo.ResponsibilityWM.NPT
WHERE        (WELDING.Weldfaults.Datetime > GETDATE() - 62)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'WELDING', @level1type = N'VIEW', @level1name = N'WeldFaultProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2175
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
', @level0type = N'SCHEMA', @level0name = N'WELDING', @level1type = N'VIEW', @level1name = N'WeldFaultProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[24] 2[32] 3) )"
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
         Begin Table = "L_timeline (Volvo)"
            Begin Extent = 
               Top = 9
               Left = 771
               Bottom = 197
               Right = 941
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Weldfaults (WELDING)"
            Begin Extent = 
               Top = 6
               Left = 465
               Bottom = 197
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Timer"
            Begin Extent = 
               Top = 3
               Left = 243
               Bottom = 191
               Right = 413
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "NPT"
            Begin Extent = 
               Top = 6
               Left = 12
               Bottom = 195
               Right = 182
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ResponsibilityWM"
            Begin Extent = 
               Top = 170
               Left = 628
               Bottom = 282
               Right = 809
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
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 2340
         Width = 1500
         Width = 1170
         Width = 1500
         Width = 2325
         Width = 3270
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
  ', @level0type = N'SCHEMA', @level0name = N'WELDING', @level1type = N'VIEW', @level1name = N'WeldFaultProtocol';

