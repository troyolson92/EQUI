

CREATE PROCEDURE [NGAC].[sp_Housekeeping]

AS
BEGIN
---------------------------------------------------------------------------------------------------------------------
Print '--Running [NGAC].[sp_Housekeeping]'
---------------------------------------------------------------------------------------------------------------------

--MOST HOUSEKEEPING IS DONE USING EQUI SYSTEM in c_houskeeping!!

--******************************************************************--
--cleanout of job and breakdown system. / alarm system
--******************************************************************--
DECLARE @maxTimestamp as datetime
set @maxTimestamp = GETDATE()-100
--******************************************************************--
print'unlink rt_job_breakdown with rt_alarm for older than 100 days'
--******************************************************************--
update NGAC.rt_job_breakdown
set rt_alarm_id = null
from NGAC.rt_job_breakdown as rtjb with(nolock)
left join NGAC.rt_alarm as rt with(nolock) on rt.id = rtjb.rt_alarm_id
WHERE rt._timestamp < @maxTimestamp
--******************************************************************--
print @@rowcount

--******************************************************************--
print'unlink rt_job_breakdown with h_alarm ONLY! operational subgroup older than 100 days'
--******************************************************************--
update NGAC.rt_job_breakdown
set rt_alarm_id = null
from  NGAC.rt_job_breakdown as rtjb  with (nolock)
LEFT OUTER JOIN NGAC.h_alarm AS H with (NOLOCK) on h.id = rtjb.h_alarm_id
LEFT OUTER JOIN NGAC.L_error AS Le with (NOLOCK)  ON Le._id = H.L_error_id 
LEFT OUTER JOIN VOLVO.c_Subgroup as cs with (NOLOCK)  on cs.id = Le.c_SubgroupId
WHERE h._timestamp < @maxTimestamp
and cs.Subgroup in('Operational**','Operational')
print @@rowcount

--******************************************************************--
print'delete rt_job_breakdown with no h_alarm or rt_alarm links older than 100 days'
--******************************************************************--
DELETE NGAC.rt_job_breakdown
FROM NGAC.rt_job_breakdown as rtjb  with (nolock)
WHERE rtjb.ts_breakdownStart < @maxTimestamp
and rtjb.h_alarm_id is null
and rtjb.rt_alarm_id is null
print @@rowcount

--******************************************************************--
print'delete rt_jobs older than 100 days.'
--******************************************************************--
DELETE NGAC.rt_job 
FROM NGAC.rt_job as rtj with(nolock)
LEFT JOIN NGAC.rt_job_breakdown as rtjb with(nolock) on rtjb.rt_job_active_id = rtj.id
where ts_start < @maxTimestamp
and rtjb.id is null
print @@rowcount

--******************************************************************--
print'delete h_alarm in operational subgroup older than 100 days'
--******************************************************************--
DELETE NGAC.h_alarm FROM  NGAC.h_alarm AS H with (NOLOCK) 
LEFT OUTER JOIN NGAC.L_error AS Le with (NOLOCK)  ON Le._id = H.L_error_id 
LEFT OUTER JOIN VOLVO.c_Subgroup as cs with (NOLOCK)  on cs.id = Le.c_SubgroupId
LEFT OUTER JOIN NGAC.rt_job_breakdown as rtjb with (NOLOCK) on h.id = rtjb.h_alarm_id
WHERE h._timestamp < @maxTimestamp
and cs.Subgroup in('Operational**','Operational')
and rtjb.h_alarm_id is null
print @@rowcount

--******************************************************************--
print'delete rt_alarm  older than 100 days'
--******************************************************************--
DELETE NGAC.rt_alarm FROM  NGAC.rt_alarm AS rt with (NOLOCK) 
LEFT OUTER JOIN NGAC.rt_job_breakdown as rtjb with (NOLOCK) on rt.id = rtjb.rt_alarm_id
WHERE rt._timestamp < @maxTimestamp
and rtjb.rt_alarm_id is null
print @@rowcount

--*********************************************************************************************************************--


Print 'check for unused records in L_*'
--******************************************************************--
--select count(*) as 'L_actions'
DELETE GADATA.NGAC.L_actions
FROM GADATA.NGAC.L_actions as l with(nolock)
left join GADATA.NGAC.L_error as le on le.l_actions_id = l.id
where le._id is null
print @@rowcount
--select count(*) as 'L_causes'
DELETE GADATA.NGAC.L_causes
FROM GADATA.NGAC.L_causes as l with(nolock)
left join GADATA.NGAC.L_error as le on le.l_causes_id = l.id
where le._id is null
print @@rowcount
--select count(*) as 'L_consequences'
DELETE GADATA.NGAC.L_consequences
FROM GADATA.NGAC.L_consequences as l with(nolock)
left join GADATA.NGAC.L_error as le on le.l_consequences_id = l.id
where le._id is null
print @@rowcount
--select count(*) as 'L_description'
DELETE GADATA.NGAC.L_description
FROM GADATA.NGAC.L_description as l with(nolock)
left join GADATA.NGAC.L_error as le with(nolock) on le.l_description_id = l.id
where le._id is null
print @@rowcount
--select count(*) as 'L_error'
DELETE GADATA.NGAC.L_error
FROM GADATA.NGAC.L_error as l with(nolock)
left join GADATA.NGAC.h_alarm as h with(nolock) on l._id = h.L_error_id
where h.L_error_id  is null
print @@rowcount


END