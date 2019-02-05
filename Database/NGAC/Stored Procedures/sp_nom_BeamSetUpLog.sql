


CREATE PROCEDURE [NGAC].[sp_nom_BeamSetUpLog]
AS
BEGIN

delete [NGAC].[L_BeamSetUpLog]
insert into  [NGAC].[L_BeamSetUpLog]
select * from (
select 
 c.id as 'c_controller_id'
,rt.*
,ROW_NUMBER() OVER (PARTITION BY c.controller_name, rt.[SetupNo] ORDER BY rt.[Date Time] DESC) AS 'rnDesc'
from NGAC.rt_BeamSetUpLog as rt 
left join NGAC.rt_csv_file as rt_csv on rt.rt_csv_file_id = rt_csv.id
left join NGAC.c_controller as c on c.id = rt_csv.c_controller_id
) as x

END