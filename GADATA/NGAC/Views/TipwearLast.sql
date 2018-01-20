


CREATE VIEW [NGAC].[TipwearLast]
AS
select 
 y.*
,avgESTspots.avgESTnSpotsFixedWearBefore100
,avgESTspots.avgESTnSpotsMoveWearBefore100
,avgESTspots.maxWearInCalc
,avgESTspots.minWearInCalc
,avgESTspots.countWearInCalc
,avgESTspots.avgESTnSpotsFixedWearBefore100 - y.Weld_Counter as 'ESTremainingspotsFixed'
,avgESTspots.avgESTnSpotsMoveWearBefore100 - y.Weld_Counter as 'ESTremainingspotsMove'
,ROUND(((avgESTspots.avgESTnSpotsFixedWearBefore100 - y.Weld_Counter) / nSpotsCar.nSpots44),0) as 'ESTremainingCarsFixed'
,ROUND(((avgESTspots.avgESTnSpotsMoveWearBefore100 - y.Weld_Counter) / nSpotsCar.nSpots44),0) as 'ESTremainingsCarsMove'
,nSpotsCar.nSpots44
,avgESTspots.LastTipchange

from(
select x.*  from(--nested to optimize return result for next join
select
 rt.*
,ROW_NUMBER() OVER (PARTITION BY rt.controller_name, rt.Tool_nr ORDER BY rt.[Date Time] DESC) AS 'rnDesc'
from GADATA.NGAC.TipDressLogFile as rt 
) as x 
where x.rnDesc =1
) as y
--**************************************************************************--
--join estemation of welds for 100 wear based on last 100 days of data 
--**************************************************************************--
left join (
SELECT 
  twBc.controller_name
 ,twBc.Tool_Nr
 ,ROUND(AVG(twBc.ESTnSpotsFixedWearBefore100),0) as 'avgESTnSpotsFixedWearBefore100'
 ,ROUND(AVG(twBC.ESTnSpotsMoveWearBefore100),0) as 'avgESTnSpotsMoveWearBefore100'
 --extra info 
 ,MAX(twBc.[WearBeforeChange]) as 'maxWearInCalc'
 ,MIN(twBc.[WearBeforeChange]) as 'minWearInCalc'
 ,COUNT(twBc.[WearBeforeChange]) as 'countWearInCalc'
 ,MAX(twBc.TipchangeTimestamp) as 'LastTipchange'

  FROM [GADATA].[NGAC].[TipwearBeforeChange] as twBc
  WHERE 
   --last 100 days of data
   twBc.TipchangeTimestamp between getdate()-100 and getdate()
   AND 
  --The tipwear must be more than x% to include it in the calculation
   twBc.[%FixedWearBeforeChange] >= 0
   AND 
   twBc.[%MoveWearBeforeChange] >= 0
  GROUP BY 
   twBc.controller_name
  , twBc.Tool_Nr
  ) as avgESTspots on 
  avgESTspots.controller_name = y.controller_name
  AND 
  avgESTspots.Tool_Nr = y.Tool_Nr
--**************************************************************************--
--join number of spots per car for this tip. 
--**************************************************************************--
left join (
  select 
  t.Robot
  --Electrode nummer nog niet gekoppled !!
 ,count(s44.Number) as 'nSpots44'
  from gadata.dbo.Timer as t
  left join GADATA.dbo.Spot as s44 on t.ID = s44.TimerID
  where s44.Number like '44%'
  group by t.Robot
  ) as nSpotsCar on nSpotsCar.Robot = y.controller_name
  --AND
  -- MUST JOIN ELECTRODE !! 
--**************************************************************************--
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TipwearLast';


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
', @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TipwearLast';

