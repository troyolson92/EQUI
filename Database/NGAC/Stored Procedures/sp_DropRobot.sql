


CREATE PROCEDURE [NGAC].[sp_DropRobot]
  @controller_id as int = -1 
AS
BEGIN


--breakdown system
delete NGAC.rt_job_breakdown 
from NGAC.rt_job_breakdown 
left join  NGAC.rt_job on rt_job.id = rt_job_breakdown.rt_job_active_id 
where c_controller_id = @controller_id
delete NGAC.rt_job from NGAC.rt_job where c_controller_id = @controller_id

--h_alarm system
delete NGAC.rt_alarm from NGAC.rt_alarm where controller_id = @controller_id
delete NGAC.h_alarm from NGAC.h_alarm where controller_id = @controller_id

--variable logging 
delete NGAC.rt_value from NGAC.rt_value where c_controller_id = @controller_id
delete NGAC.rt_search_value from NGAC.rt_search_value where c_controller_id = @controller_id
delete NGAC.rtu_SpeedOvr from NGAC.rtu_SpeedOvr where c_controller_id = @controller_id
delete NGAC.rt_Disp_BarrelLow from NGAC.rt_Disp_BarrelLow where c_controller_id = @controller_id

--csv file system
delete NGAC.rt_TipDressLogFile 
from NGAC.rt_TipDressLogFile 
left join  NGAC.rt_csv_file on rt_csv_file.id = rt_TipDressLogFile.rt_csv_file_id 
where rt_csv_file.c_controller_id = @controller_id

delete NGAC.rt_ErrDispLog 
from NGAC.rt_ErrDispLog 
left join  NGAC.rt_csv_file on rt_csv_file.id = rt_ErrDispLog.rt_csv_file_id 
where rt_csv_file.c_controller_id = @controller_id

delete NGAC.rt_SpotErr 
from NGAC.rt_SpotErr 
left join  NGAC.rt_csv_file on rt_csv_file.id = rt_SpotErr.rt_csv_file_id 
where rt_csv_file.c_controller_id = @controller_id

delete NGAC.rt_TCP_LOG 
from NGAC.rt_TCP_LOG 
left join  NGAC.rt_csv_file on rt_csv_file.id = rt_TCP_LOG.rt_csv_file_id 
where rt_csv_file.c_controller_id = @controller_id

delete NGAC.rt_BeamSetUpLog 
from NGAC.rt_BeamSetUpLog 
left join  NGAC.rt_csv_file on rt_csv_file.id = rt_BeamSetUpLog.rt_csv_file_id 
where rt_csv_file.c_controller_id = @controller_id

delete NGAC.rt_csv_file from NGAC.rt_csv_file where c_controller_id = @controller_id

--device info
delete NGAC.rt_device_info from NGAC.rt_device_info where c_controller_id = @controller_id

--pjv
delete NGAC.rt_pjv_file from NGAC.rt_pjv_file where c_controller_id = @controller_id

--active tables
delete NGAC.rt_active_info from NGAC.rt_active_info where c_controller_id = @controller_id
delete NGAC.rt_controller from NGAC.rt_controller where c_controller_id = @controller_id
delete NGAC.rt_event from NGAC.rt_event where c_controller_id = @controller_id

--l_operation
delete NGAC.L_operation from NGAC.L_operation where controller_id = @controller_id

--drop the controller
delete NGAC.c_controller from NGAC.c_controller where id = @controller_id
END