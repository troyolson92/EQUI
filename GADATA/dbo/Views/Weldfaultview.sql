
CREATE VIEW [dbo].[Weldfaultview]
AS
SELECT        NULL AS [Location], [Robot] AS Robot, [NPT], [DateTime] AS Date, [SPOT], [Program], [Weldfaultname], NULL AS [RobotFault], [Vday], [Vweek], [Vyear], [PLOEG], NULL 
                         AS downtime, NULL AS description, NULL AS OldValue, NULL AS NewValue, NULL AS CDSID
FROM            [GADATA].[dbo].[Qstopcomponent_analyse]
WHERE        Datetime >= GETDATE() - 1
UNION
SELECT        [location], [Robotname], NULL AS [Location], [Timestamp], NULL AS [Type], NULL AS [Errortype], NULL AS [Logtekst], [Logtekst], NULL AS [Year], NULL 
                         AS [Week], NULL AS [day], NULL AS [Shift], [DOWNTIME], NULL AS [Shift], NULL AS [Shift], NULL AS [Shift], NULL AS [Shift]
FROM            C3G.old_breakdown
WHERE        (Object = 'Spot') AND (DOWNTIME >= 2) AND (Object = 'Spot') AND Timestamp >= GETDATE() - 1
UNION
SELECT        [Location], [Robotname] AS ROBOT, NULL AS [Location], [Timestamp] AS DATE, NULL AS [Type], NULL AS [ErrorType], NULL AS [Logcode], [Logtekst], NULL 
                         AS [DOWNTIME], NULL AS [Year], NULL AS [Week], NULL AS [day], [DOWNTIME], NULL AS [Object], NULL AS [Subgroup], NULL AS [idx], NULL AS [idx]
FROM            [GADATA].[C4G].[old_breakdown]
WHERE        object = 'Spot' AND (DOWNTIME >= 2) AND (Object = 'Spot') AND Timestamp >= GETDATE() - 1
UNION
SELECT        NULL AS [Location], [ROBOT],NULL AS  [NPT], [DateTime], NULL AS [SPOT],NULL AS [Program], [ErrorText], NULL AS [ErrorText], NULL AS [Vday], NULL AS [Vday], NULL AS [Vday], NULL 
                         AS [PLOEG], NULL AS [downtime], NULL AS [Description], NULL AS [OldValue], NULL AS [NewValue], NULL AS [CDSID]
FROM            [GADATA].[dbo].[ErrorLog]
WHERE        [DateTime] >= GETDATE() - 1
UNION
SELECT        NULL AS [Robot], [Robot], [Name] AS NPT, [DateTime] AS DATE, [Number], [Program], [Description], NULL AS [OldValue], NULL AS [Number], NULL AS [Number], NULL 
                         AS [Number], NULL AS [Number], NULL AS [Number], [Description], [OldValue], [NewValue], [CDSID]
FROM            [GADATA].[dbo].[Weldfault_WM_change]
WHERE        DateTime >= GETDATE() - 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Weldfaultview';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[4] 4[13] 2[48] 3) )"
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
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Weldfaultview';

