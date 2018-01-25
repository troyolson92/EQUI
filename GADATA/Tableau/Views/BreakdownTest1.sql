﻿
CREATE VIEW [Tableau].[BreakdownTest1]
AS
select 
c.controller_name
,rt.jobNo
,rt.ts_Start as 'cycle start'
,DATEDIFF(SECOND,rt.ts_breakDownStart,rt.ts_breakDownEnd ) as 'DT'
,DATEDIFF(SECOND,rt.ts_breakDownStart,rt.ts_breakDownAck ) as 'RT'
,rtb.h_alarm_id
,e.FullLogtext
,e.Classification
,e.Subgroup
,a.PNG
,a.X_pos
,a.Y_pos
from GADATA.NGAC.rt_job as rt
LEFT JOIN GADATA.NGAC.c_controller as c on c.id = rt.c_controller_id 
LEFT JOIN GADATA.NGAC.rt_job_breakdown as rtb on rtb.rt_job_active_id = rt.id
LEFT JOIN GADATA.NGAC.ControllerEventLog as e on e.refId = rtb.h_alarm_id
LEFT JOIN GADATA.EqUi.Assets_XY as a on a.location = c.controller_name

where c.controller_name like '33%'
and rt.ts_breakDownStart is not null
and rt._timestamp between GETDATE()-3 and GETDATE()
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'Tableau', @level1type = N'VIEW', @level1name = N'BreakdownTest1';


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
               Bottom = 135
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 263
               Bottom = 135
               Right = 438
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rtb"
            Begin Extent = 
               Top = 6
               Left = 476
               Bottom = 135
               Right = 668
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 6
               Left = 706
               Bottom = 135
               Right = 881
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 919
               Bottom = 135
               Right = 1089
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
', @level0type = N'SCHEMA', @level0name = N'Tableau', @level1type = N'VIEW', @level1name = N'BreakdownTest1';
