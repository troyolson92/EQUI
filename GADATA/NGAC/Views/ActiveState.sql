




/*c.controller_name LIKE '%336020%'*/
CREATE VIEW [NGAC].[ActiveState]
AS
SELECT TOP (1000) 
       c.controller_name
    --  ,rtai.[rt_job_id]
      ,rtai.[_timestamp]
      ,rtai.[program_number]
    --  ,rtai.[body_number]
      ,rtai.[application_error]
      ,rtai.[operating_mode]
      ,rtai.[execution_status]
    --  ,rtai.[controller_state]
    --  ,rtai.[master_state]
      ,rtai.[at_home]
    --  ,rtai.[vasc_state]
      ,rtai.[task_execution_status]
	, isnull(LTRIM(RTRIM(Le.[Title])),'#No Title available') + CHAR(13)+CHAR(10) +  
	  isnull(ld.CleanDescription ,'#No Description available') AS 'TrigEv'
	,rtj.ts_breakDownStart
	,DATEDIFF(SECOND,rtj.ts_breakDownStart,rtj.ts_breakDownAck ) as 'Response'
    ,DATEDIFF(SECOND,rtj.ts_breakDownStart,rtj.ts_breakDownEnd ) as 'Downtime'
	 ,(SELECT top 1 ControllerEventLog.FullLogtext from GADATA.NGAC.ControllerEventLog where ControllerEventLog.controller_name = c.controller_name order by ControllerEventLog.refid desc) as 'LastEv'
  FROM [GADATA].[NGAC].[rt_active_info] as rtai

LEFT JOIN GADATA.NGAC.h_alarm as h on h.id = rtai.h_alarm_id
LEFT JOIN GADATA.NGAC.L_error as Le on Le._id = h.L_error_id
LEFT OUTER JOIN GADATA.NGAC.L_description AS ld  ON ld.id = le.l_description_id

LEFT JOIN GADATA.NGAC.c_controller as c on c.id = rtai.c_controller_id

LEFT JOIN GADATA.NGAC.rt_job as rtj on rtj.id = rtai.rt_job_id

--WHERE rtai.application_error = 0 AND rtai.program_number <> 0-- OR rtai.task_execution_status = 
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'ActiveState';


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
         Begin Table = "rtai"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "h"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 135
               Right = 462
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Le"
            Begin Extent = 
               Top = 6
               Left = 500
               Bottom = 135
               Right = 687
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ld"
            Begin Extent = 
               Top = 6
               Left = 725
               Bottom = 135
               Right = 904
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 942
               Bottom = 135
               Right = 1117
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
', @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'ActiveState';

