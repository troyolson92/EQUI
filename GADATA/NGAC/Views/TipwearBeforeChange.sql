







CREATE VIEW [NGAC].[TipwearBeforeChange]
AS


SELECT 
 x.controller_name
,x.Tool_Nr
,x.TipchangeTimestamp
,x.FixedWearBeforeChange
,x.MoveWearBeforeChange
,x.WearBeforeChange
,x.WeldsBeforeChange
,x.DressBeforeChange 
,ROUND((X.FixedWearBeforeChange  / X.Max_Wear_Fixed)*100,0)as '%FixedWearBeforeChange' --fixed dataType cast !!
,ROUND((X.MoveWearBeforeChange  / X.Max_Wear_Move)*100,0) as '%MoveWearBeforeChange' --fixed dataType cast !!
,x.WeldsBeforeChange * (100-ROUND(((CAST(x.Max_Wear_Fixed as float)/100) * x.FixedWearBeforeChange) *100, 1)) as 'ESTnSpotsFixedWearBefore100'
,x.WeldsBeforeChange * (100-ROUND(((CAST(x.Max_Wear_Move as float)/100) * x.MoveWearBeforeChange) *100, 1)) as 'ESTnSpotsMoveWearBefore100'
,DATEDIFF(hour,x.PreviousTipchange,X.TipchangeTimestamp) as 'TipAge(h)'
FROM (
--sub select the number of welds for that tiplife************************************************************************************--
SELECT 
*
--need to sum the weldcounter between 2 tipchanges
,(
SELECT TOP 1 SUM(rt.Weld_Counter) 
FROM GADATA.NGAC.rt_TipDressLogFile as rt 
WHERE rt.rt_csv_file_id = Z.rt_csv_file_id
AND isnull(rt.[Tool_Nr],1)  = Z.Tool_Nr
AND rt.[Date Time] BETWEEN Z.PreviousTipchange AND Z.TipchangeTimestamp
) as 'WeldsBeforeChange'
FROM(
--join previous tipchange to get that timestamp************************************************************************************--
SELECT 
*
,lead(Y.TipchangeTimestamp) OVER (PARTITION BY y.controller_name, y.Tool_nr  ORDER BY y.TipchangeTimestamp desc) as 'PreviousTipchange' 
--*********************************************************************************************************************************--
FROM (
--get date from a singel tip interval**********************************************************************************************--
SELECT
 c.controller_name
,rt.rt_csv_file_id
,rt.[Date Time] as 'TipchangeTimestamp'
,isnull(rt.[Tool_Nr],1)  as 'Tool_Nr'
,rt.Dress_Num 
,rt.Weld_Counter
,rt.Max_Wear_Fixed
,rt.Max_Wear_Move
,rt.Dress_Reason
,lead(rt.Wear_Fixed) OVER (PARTITION BY c.controller_name, rt.Tool_nr  ORDER BY rt.[Date Time] desc) as 'FixedWearBeforeChange'
,lead(rt.Wear_Move) OVER (PARTITION BY c.controller_name, rt.Tool_nr  ORDER BY rt.[Date Time] desc) as 'MoveWearBeforeChange'
,lead(rt.Current_TipWear) OVER (PARTITION BY c.controller_name, rt.Tool_nr  ORDER BY rt.[Date Time] desc) as 'WearBeforeChange'
,lead(rt.Dress_num) OVER (PARTITION BY c.controller_name, rt.Tool_nr  ORDER BY rt.[Date Time] desc) as 'DressBeforeChange'
from GADATA.NGAC.rt_TipDressLogFile as rt 
left join GADATA.NGAC.rt_csv_file as rt_csv on rt.rt_csv_file_id = rt_csv.id
left join GADATA.NGAC.c_controller as c on c.id = rt_csv.c_controller_id
) AS Y
where 

--robot mounted guns
(
	Y.Dress_Num = 0 
AND Y.Dress_Reason = 'FullDress'
)
OR --stat guns
(
	Y.Dress_Num = 3
AND Y.Dress_Reason = 'InitDress'
)
AND Y.DressBeforeChange is not null 
--*********************************************************************************************************************************--
) AS Z
) AS X
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TipwearBeforeChange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[36] 2[20] 3) )"
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
         Top = -120
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
', @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'TipwearBeforeChange';

