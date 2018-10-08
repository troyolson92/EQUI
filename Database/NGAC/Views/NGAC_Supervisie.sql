﻿









CREATE VIEW [NGAC].[NGAC_Supervisie]
AS
SELECT DISTINCT 
  output.Location       AS 'Location' 
, output.Logtext		AS 'Logtext'
, output.Response		AS 'RT'
, output.Downtime		AS 'DT'
, CONVERT(char(19),output.[timestamp], 108) AS 'time' 
, output.Classification	AS 'Classification'
, output.Subgroup		AS 'Subgroup'
, output.Severity		AS 'Severity'
, output.Logcode        AS 'Logcode'
, output.Logtype		AS 'Logtype'
, ISNULL(output.refId,0)  AS 'refId'
, output.timestamp		as 'timestamp'
, output.LocationTree as 'LocationTree'
, output.ClassTree as 'ClassTree'
,timeline.Vyear
,timeline.Vweek
,timeline.Vday
,timeline.shift

FROM
(
--*******************************************************************************************************--
--NGAC error
--*******************************************************************************************************--
SELECT 
       [Location]
      ,[AssetID]
      ,[Logtype]
      ,[timestamp]
      ,[Logcode]
      ,[Severity]
      ,[Logtext]
      ,[Response]
      ,[Downtime]
      ,[Classification]
      ,[Subgroup]
      ,[refId]
      ,[LocationTree]
      ,[ClassTree]
      ,[controller_name]
      ,[controller_type]

FROM NGAC.ControllerEventLog with (NOLOCK) 
where 
ControllerEventLog.[timestamp] < getdate() --to filter out bad timestamps
and
ControllerEventLog.Category in ('Hardware')

--*******************************************************************************************************--
--NGAC breakdown
--*******************************************************************************************************--
UNION
select 
       [Location]
      ,[AssetID]
      ,[Logtype]
      ,[timestamp]
      ,[Logcode]
      ,[Severity]
      ,[Logtext]
      ,[Response]
      ,[Downtime]
      ,[Classification]
      ,[Subgroup]
      ,[refId]
      ,[LocationTree]
      ,[ClassTree]
      ,[controller_name]
      ,[controller_type]
FROM NGAC.Breakdown as breakdown with (NOLOCK) 
where 
breakdown.[timestamp]  < getdate() --to filter out bad timestamps

--*******************************************************************************************************--
--NGAC live (active breakdown)
--*******************************************************************************************************--
UNION
select 
       [Location]
      ,[AssetID]
      ,[Logtype]
      ,[timestamp]
      ,[Logcode]
      ,[Severity]
      ,[Logtext]
      ,[Response]
      ,[Downtime]
      ,[Classification]
      ,[Subgroup]
      ,[refid]
      ,[LocationTree]
      ,[ClassTree]
      ,[controller_name]
      ,[controller_type]
FROM NGAC.ActiveState as ActiveState with (NOLOCK) 
--*******************************************************************************************************--

) as output
left join Volvo.L_timeline as timeline on output.[timestamp] between timeline.starttime and timeline.endtime
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'NGAC_Supervisie';


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
', @level0type = N'SCHEMA', @level0name = N'NGAC', @level1type = N'VIEW', @level1name = N'NGAC_Supervisie';

