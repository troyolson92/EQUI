/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
print 'Start post deployement script'
:r .\Ngac\Data\c_service_setup.sql
:r .\Ngac\Data\c_controller_class.sql
:r .\Ngac\Data\c_csv_log.sql
:r .\Ngac\Data\c_error.sql
:r .\Ngac\Data\c_job.sql
:r .\Ngac\Data\c_variable.sql
:r .\Ngac\Data\c_variable_search.sql
--contains robots from GHENT line 336061 336062 (example and test robots)
:r .\Ngac\Data\c_controller.sql

:r .\Volvo\Data\c_userRoles.sql

:r .\EqUi\Data\c_areas.sql
:r .\EqUi\Data\c_datasource.sql
:r .\EqUi\Data\c_logClassSystem.sql

:r .\Alerts\Data\c_state.sql
:r .\Alerts\Data\c_smsSystem.sql
:r .\Alerts\Data\c_triggers.sql 

--users are by default disbaled. fix this 
--print 'enable users'
--GRANT CONNECT TO [EqUiAdmin]
--GRANT CONNECT TO [VASC_a]
print 'post deployement done' 