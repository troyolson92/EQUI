



CREATE VIEW [VISIO].[Q_line]
AS

SELECT DISTINCT 
 a.Area
,a.line AS 'Lijn'

,(
SELECT ISNULL(CAST( 
(select top 1 datediff(hour,CHANGEDATE,getdate())
from gadata.VISIO.[QiconRawMX7] where ANCESTOR = a.line and JPNUM = 'PQAGEO' AND datediff(hour,CHANGEDATE,getdate()) <= 100
order by changedate desc) 
as varchar(3)),'100')+'%'
)AS 'GEO'

,(
SELECT ISNULL(CAST( 
(select top 1 datediff(hour,CHANGEDATE,getdate())
from gadata.VISIO.[QiconRawMX7] where ANCESTOR = a.line and WORKTYPE = 'CI' AND datediff(hour,CHANGEDATE,getdate()) <= 100
order by changedate desc) 
as varchar(3)),'100')+'%'
)AS 'Maint'

,(
isnull(CAST(
(SELECT top 1 'True'
FROM   Volvo.ia_Alert
LEFT OUTER JOIN equi.ASSETS as la on 
la.controller_type = 'c3g' AND la.controller_id = ia_Alert.controller_id AND  la.CLassificationId LIKE '%URC%'
where ia_Alert.AlertStatus  in(0,1) AND la.line = a.line) as varchar(5)),'False')
)AS 'Alert'

, '100%' AS 'Parameter'
FROM     EqUi.ASSETS AS a
--WHERE  (Area LIKE '%A FLOOR S%')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'VISIO', @level1type = N'VIEW', @level1name = N'Q_line';


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
', @level0type = N'SCHEMA', @level0name = N'VISIO', @level1type = N'VIEW', @level1name = N'Q_line';

