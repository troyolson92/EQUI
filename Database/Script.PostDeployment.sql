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

:r .\Volvo\Data\c_userRoles.sql

:r .\EqUi\Data\c_areas.sql
:r .\EqUi\Data\c_datasource.sql
:r .\EqUi\Data\c_logClassSystem.sql

:r .\Alerts\Data\c_state.sql
:r .\Alerts\Data\c_triggers.sql

print 'post deployement done' 