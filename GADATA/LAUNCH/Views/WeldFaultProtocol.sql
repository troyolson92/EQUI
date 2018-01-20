CREATE VIEW LAUNCH.WeldFaultProtocol
AS
SELECT        TOP (100) PERCENT Volvo.L_timeline.Vyear, Volvo.L_timeline.Vweek, Volvo.L_timeline.Vday, dbo.WeldFaultLog.DateTime, dbo.NPT.Name AS NPT, dbo.Timer.Name, 
                         dbo.Spot.Number AS Spot, dbo.Spot.Program, dbo.WeldFaultName.WeldFaultName AS TimerFault, dbo.WeldFaultLog.WeldMasterComment, dbo.WeldFaultLog.ID, 
                         dbo.ResponsibilityWM.responsibilityWM
FROM            dbo.NPT INNER JOIN
                         dbo.WeldFaultLog INNER JOIN
                         dbo.WeldFaultName ON dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND 
                         dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND 
                         dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND 
                         dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND 
                         dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID AND dbo.WeldFaultLog.WeldFaultID = dbo.WeldFaultName.ID INNER JOIN
                         dbo.Spot ON dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND 
                         dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND 
                         dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND dbo.WeldFaultLog.SpotID = dbo.Spot.ID AND 
                         dbo.WeldFaultLog.SpotID = dbo.Spot.ID INNER JOIN
                         dbo.Timer ON dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID AND
                          dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID ON dbo.NPT.ID = dbo.Timer.NptId AND 
                         dbo.NPT.ID = dbo.Timer.NptId AND dbo.NPT.ID = dbo.Timer.NptId AND dbo.NPT.ID = dbo.Timer.NptId AND dbo.NPT.ID = dbo.Timer.NptId AND 
                         dbo.NPT.ID = dbo.Timer.NptId AND dbo.NPT.ID = dbo.Timer.NptId INNER JOIN
                         dbo.ResponsibilityWM ON dbo.NPT.ID = dbo.ResponsibilityWM.NPTID LEFT OUTER JOIN
                         Volvo.L_timeline ON dbo.WeldFaultLog.DateTime BETWEEN Volvo.L_timeline.starttime AND Volvo.L_timeline.endtime
WHERE        (dbo.WeldFaultLog.DateTime >= GETDATE() - 730)
UNION
SELECT DISTINCT 
                         TOP (100) PERCENT Volvo.L_timeline.Vyear, Volvo.L_timeline.Vweek, Volvo.L_timeline.Vday, dbo.TimerErrorLog.DateTime, dbo.NPT.Name AS NPT, 
                         dbo.Timer.Name AS Timer, NULL AS Expr1, NULL AS Expr2, dbo.TimerErrorText.ErrorText, NULL AS Expr3, dbo.TimerErrorLog.ID, 
                         dbo.ResponsibilityWM.responsibilityWM
FROM            dbo.Timer INNER JOIN
                         dbo.NPT ON dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID AND 
                         dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID INNER JOIN
                         dbo.TimerErrorLog ON dbo.Timer.ID = dbo.TimerErrorLog.TimerID AND dbo.Timer.ID = dbo.TimerErrorLog.TimerID INNER JOIN
                         dbo.TimerErrorText ON dbo.TimerErrorLog.ErrorID = dbo.TimerErrorText.ID INNER JOIN
                         dbo.ResponsibilityWM ON dbo.NPT.ID = dbo.ResponsibilityWM.NPTID LEFT OUTER JOIN
                         Volvo.L_timeline ON dbo.TimerErrorLog.DateTime BETWEEN Volvo.L_timeline.starttime AND Volvo.L_timeline.endtime
WHERE        (dbo.TimerErrorLog.DateTime >= GETDATE() - 365) AND (dbo.TimerErrorText.ErrorText = N'weld Fault') AND (dbo.NPT.Name BETWEEN N'NPT40' AND N'NPT73')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'LAUNCH', @level1type = N'VIEW', @level1name = N'WeldFaultProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[3] 4[26] 2[60] 3) )"
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
         Left = -1363
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 870
         Width = 1590
         Width = 1320
         Width = 2730
         Width = 945
         Width = 1095
         Width = 2265
         Width = 1125
         Width = 2220
         Width = 2385
         Width = 1200
         Width = 1200
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 5040
         Alias = 3900
         Table = 2445
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 2865
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'LAUNCH', @level1type = N'VIEW', @level1name = N'WeldFaultProtocol';

