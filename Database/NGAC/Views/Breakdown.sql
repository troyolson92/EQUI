﻿

















CREATE VIEW [NGAC].[Breakdown]
AS
SELECT 
  c.controller_name		   AS 'Location' 
, c.CLassificationId     AS 'AssetID'
, 'BREAKDOWN'	  AS 'Logtype'
, rtj.ts_breakDownEnd        AS 'timestamp'

,CASE when rtjb.h_alarm_id is not null
 THEN CAST(h.Logcode as varchar(max)) 
 ELSE CAST(rt.Logcode as varchar(max)) 
 END AS 'Logcode'
,CASE when rtjb.h_alarm_id is not null
 THEN CAST(h.Severity as varchar(max))
 ELSE CAST(rt.Severity as varchar(max))
 END AS 'Severity'

,CASE when rtjb.h_alarm_id is not null
 THEN h.logtext
 ELSE rt.logtext
 END AS 'Logtext'
,CASE when rtjb.h_alarm_id is not null
 THEN h.FullLogtext
 ELSE rt.FullLogtext
 END AS 'FullLogtext'

,DATEDIFF(SECOND,rtj.ts_breakDownStart,rtj.ts_breakDownAck ) as 'Response'
,DATEDIFF(SECOND,rtj.ts_breakDownStart,rtj.ts_breakDownEnd ) as 'Downtime'

, ISNULL(h.Classification,'Undefined*')  AS 'Classification'
, ISNULL(h.Subgroup,'Undefined*')		 AS 'Subgroup'

,CASE when rtjb.h_alarm_id is not null
 THEN h.category
 ELSE rt.category
 END AS 'Category'
, rtj.id				 AS 'refId'
, c.LocationTree     As 'LocationTree'
, c.ClassificationTree as 'ClassTree'
, c.controller_name		AS 'controller_name'
, 'NGAC'		As 'controller_type'

FROM  NGAC.rt_job AS rtj 
LEFT JOIN NGAC.rt_job_breakdown as rtjb on rtjb.rt_job_active_id = rtj.id AND rtjb.[index] = 1
LEFT JOIN NGAC.ControllerEventLog as h on h.refid = rtjb.h_alarm_id
LEFT JOIN NGAC.junk_alarms as rt on rt.refid = rtjb.rt_alarm_id


LEFT JOIN NGAC.c_controller as c with (NOLOCK) on c.id = rtj.c_controller_id
--must be a breakdown not just CT
WHERE rtj.ts_breakDownStart is not null 
AND rtj.ts_breakDownEnd is not null
--AND rtjb.h_alarm_id is not null --WARNING this cause us ONLY to show the alarms joined with H_ALARM! this filters out a SHITLOAD 
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'Breakdown';


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
         Begin Table = "H"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 263
               Bottom = 135
               Right = 445
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 483
               Bottom = 135
               Right = 658
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
', @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'Breakdown';

