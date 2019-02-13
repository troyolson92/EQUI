

CREATE PROCEDURE [UL].[sp_norm_InspectionPlans]
  @Daysback as int = 30 --limit the search window on the rt table
AS
BEGIN
---------------------------------------------------------------------------------------
Print'--add new completed plans to ul.h_CompletedPlans'
---------------------------------------------------------------------------------------
INSERT INTO ul.h_CompletedPlans
select 
 rt.[InspectionPlanname]
,rt.[planStartDT]
,rt.[planEndDT]
,rt.[Inspector]
,rt.[InspectionLaptop]
,rt.[Planlength]
,rt.[TotalMeasurements]
,rt.[UniqueMeasurePoints]
,rt.[Remarks]
from (
--**************************---
--query extracts completed plans with some kpi's from raw UL datatable
--**************************---
select 
  planStartEnd.*
,(select top 1 count(*) 
from Ul.rt_UltralogData as measurements
where planStartEnd.InspectionPlanname = measurements.InspectionPlanname and planStartEnd.InspectionLaptop = measurements.InspectionLaptop 
and measurements.ULDateTime between planStartEnd.planStartDT and planStartEnd.planEndDT
) as 'TotalMeasurements'
,(select top 1 COUNT(*) 
from (select distinct IndexOfTestsequence
      from ul.rt_UltralogData as measurements
	  where planStartEnd.InspectionPlanname = measurements.InspectionPlanname and planStartEnd.InspectionLaptop = measurements.InspectionLaptop 
	  and measurements.ULDateTime between planStartEnd.planStartDT and planStartEnd.planEndDT
     ) as distinctmeasurements 
) as 'UniqueMeasurePoints'
,(select top 1 count(*) 
from Ul.rt_UltralogData as measurements
where planStartEnd.InspectionPlanname = measurements.InspectionPlanname and planStartEnd.InspectionLaptop = measurements.InspectionLaptop 
and measurements.ULDateTime between planStartEnd.planStartDT and planStartEnd.planEndDT
and (measurements.EvaluationClass not in ('OK') or measurements.Autocomment != '' or measurements.InspectorComment != '')
) as 'Remarks'
from
(
select 
 planend.InspectionPlanname
,(select top 1 planstart.ULDateTime from UL.rt_UltralogData as planstart 
where planstart.IndexOfTestsequence = 1 and planstart.InspectionPlanname = planend.InspectionPlanname and planstart.InspectionLaptop = planend.InspectionLaptop
order by planstart.ULDateTime asc
) as 'planStartDT'
,planend.ULDateTime as 'planEndDT'
,planend.Inspector
,planend.InspectionLaptop
,planend.Planlength
--make row number on plan end (in case multible measurements on last plan point.)
,ROW_NUMBER() OVER (PARTITION BY planend.IndexOfTestsequence  ORDER BY planend.ULDateTime DESC) AS 'rnDesc'
from [UL].[rt_UltralogData] as planend
where planend.IndexOfTestsequence = planend.Planlength
) as planStartEnd
--only take last measurement of plan (in case multible measurements on last plan point.)
where planStartEnd.rnDesc = 1
--**************************---
) as rt
--**************************---
--join historian to prevent dupplicat inserts
--**************************---
Left join ul.h_CompletedPlans as h on 
rt.InspectionPlanname = h.InspectionPlanname 
and rt.InspectionLaptop = h.InspectionLaptop
and rt.planStartDT = h.planStartDT
where h.id IS NULL --only add new records
and rt.planStartDT between getdate()-@daysback and getdate() --limit search window on rt table


---------------------------------------------------------------------------------------
Print'--add measurements with remarks to h_CompletedPlansRemarks'
---------------------------------------------------------------------------------------
INSERT INTO ul.h_measurementRemarks
select 
 rt.[CompletedPlans_id]
,rt.[spotname]
,rt.[EvaluationClass]
,rt.[Autocomment]
,rt.[InspectorComment]
,rt.[Partname]
,rt.[measuredThickness]
,rt.[NominalDiameter]
,rt.[ULDateTime]
,rt.[InspectionPlanname]
,rt.[InspectionLaptop]
,rt.[Inspector]
from (
--**************************---
--query extracts measurements with a remark from ul raw data
--**************************---
select 
 CompletedPlans.id as 'CompletedPlans_id'
,measurements.spotname
,measurements.EvaluationClass
,measurements.Autocomment
,measurements.InspectorComment
,measurements.Partname
,measurements.measuredThickness
,measurements.NominalDiameter
,measurements.ULDateTime
,measurements.InspectionPlanname
,measurements.InspectionLaptop
,measurements.Inspector
from Ul.rt_UltralogData as measurements
left join ul.h_CompletedPlans as CompletedPlans on 
measurements.InspectionPlanname = CompletedPlans.InspectionPlanname 
and measurements.InspectionLaptop = CompletedPlans.InspectionLaptop
and measurements.ULDateTime between CompletedPlans.planStartDT and CompletedPlans.planEndDT
where (measurements.EvaluationClass not in ('OK') or measurements.Autocomment != '' or measurements.InspectorComment != '')
--**************************---
) as rt
--**************************---
--join historian to prevent dupplicat inserts
--**************************---
Left join ul.h_measurementRemarks as h on 
rt.InspectionPlanname = h.InspectionPlanname 
and rt.InspectionLaptop = h.InspectionLaptop
and rt.ULDateTime = h.ULDateTime
where h.id IS NULL --only add new records
and rt.CompletedPlans_id is not null --only remarks made in a full plan
and rt.ULDateTime between getdate()-@daysback and getdate() --limit search window on rt table
END