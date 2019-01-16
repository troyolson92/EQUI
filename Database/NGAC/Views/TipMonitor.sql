


















CREATE VIEW [NGAC].[TipMonitor]
AS
SELECT DISTINCT top 10000  c.controller_name as 'Robot'
					      ,ISNULL(rt.Tool_Nr,1) as 'Tool_Nr' 
                          ,isnull(rt.[Date Time],getdate()) as 'Date time'
                          ,rt.[Dress_Num] as 'nDress'
                          ,rt.[Weld_Counter] as 'nWelds'
						  ,rt.TipWearRatio as 'WearRatio'
	                      ,CASE 
						     --tipalert and fixed dominant
						      WHEN (alert.alertType = 'NotChanged') AND rt.[Wear_Fixed] >= rt.[Wear_Move]  THEN ROUND((rt.[Wear_Fixed]  / rt.[Max_Wear_Fixed])*100,0) + isnull(rt.[Last%FixedWearBeforeChange],0)
			                  --tipalert and mov dominant
							  WHEN (alert.alertType = 'NotChanged') AND rt.[Wear_Fixed] <= rt.[Wear_Move]  THEN ROUND((rt.Wear_Move  / rt.Max_Wear_Move)*100,0) + isnull(rt.[Last%MovWearBeforeChange],0)
							  --NO tipalert and fixed dominant
						      WHEN  rt.[Wear_Fixed] >= rt.[Wear_Move]  THEN ROUND((rt.[Wear_Fixed]  / rt.[Max_Wear_Fixed])*100,0)
			                  --NO tipalert and mov dominant
							  WHEN  rt.[Wear_Fixed] <= rt.[Wear_Move]  THEN ROUND((rt.Wear_Move  / rt.Max_Wear_Move)*100,0) 
		                      ELSE -1
		                   END 'pWear' --in case of an alert we combine the previous wear and the current wear
                          ,CASE 
						     --if tipalert return error on remaining spots
						      WHEN (alert.alertType = 'NotChanged') THEN -1
							--if not tipalert
	                          WHEN rt.[ESTremainingspotsFixed] >= rt.[ESTremainingspotsMove] THEN rt.[ESTremainingspotsFixed]
		                      WHEN rt.[ESTremainingspotsFixed] <= rt.[ESTremainingspotsMove] THEN rt.[ESTremainingspotsMove]
							  ELSE null
		                   END 'nRspots'
                          ,CASE 
						     --if tipalert return error on remaining spots
						      WHEN (alert.alertType = 'NotChanged') THEN -1
							  --if not tipalert
	                          WHEN  rt.[ESTremainingCarsFixed] >= rt.[ESTremainingsCarsMove] THEN rt.ESTremainingCarsFixed
							  WHEN  rt.[ESTremainingCarsFixed] <= rt.[ESTremainingsCarsMove] THEN rt.ESTremainingsCarsMove
		                      ELSE null
		                   END 'nRcars'
						  ,CASE --dummy collum to help with sorting. (stuff with alerts always on top and then decending sort on nRcars)
						      WHEN (alert.alertType is not null) THEN -10000
	                          WHEN  rt.[ESTremainingCarsFixed] >= rt.[ESTremainingsCarsMove] THEN rt.ESTremainingCarsFixed
							  WHEN  rt.[ESTremainingCarsFixed] <= rt.[ESTremainingsCarsMove] THEN rt.ESTremainingsCarsMove
		                      ELSE null
		                   END 'SortCol'
						 ,DATEDIFF(hour,rt.LastTipchange,getdate()) as 'TipAge(h)'
						 ,rt.LastTipchange
						 ,rt.Time_DressCycleTime
						 ,c.id
						 ,c.LocationTree
						 ,c.hasTipchanger
						 ,ABS(ROUND(rt.DeltaNom-rt.[avgDeltaNomAfterchange],2)) as 'MagicFiXedWear'
						 ,CASE 
						     WHEN rt._timestamp is null THEN 'NO DATA' --no data!
							 WHEN rt.countWearInCalc is null THEN 'NO PREDICTION' --no wear in calc
						     WHEN alert.alertType is not null THEN alert.alertType -- pass active alert can be .. NotChanged OR Tipchanger
							 ELSE ''
						  END as 'Status'
						 ,ROUND(rt.Wear_Fixed+rt.Wear_Move,2) as 'RobotWear'


                      FROM NGAC.c_controller as c with(nolock)
					  left join [NGAC].[TipwearLast] as rt with(nolock) on rt.controller_name = c.controller_name
					  --join active tiplife alerts
					  left join (
					  select 
						 c_triggers.alertType
					    ,h_alert.locationTree 
						,h_alert.id
						from Alerts.h_alert with(nolock)
						left join Alerts.c_triggers with(nolock) on c_triggers.id = h_alert.c_tirgger_id
						left join Alerts.c_state  with(nolock) on h_alert.[state] = c_state.id
						where c_triggers.alertGroup = 'TIPLIFE'
						AND h_alert._timestamp > getdate()-3  -- limit search window
						AND c_state.[state] = 'WGK'
					  ) as alert on alert.locationTree = rt.LocationTree

                      --add 1 hour for daylight saving time.
					  where ((rt.[Date Time] < getdate()+'1900-01-01 01:00:00') or rt.[Date Time] is null) AND (c.hasspotweld = 1) --only robots with the bit set will be handed!
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TipMonitor';


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
         Begin Table = "TipwearLast (NGAC)"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 168
               Right = 376
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
', @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TipMonitor';

