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

--key 632A64BA78FF63171857E26B49EBAA50DC425073CFF24ACBB58DADFD42788C34

:r .\NGAC\PostDeploy_c_service_setup.sql
:r .\Volvo\PostDeploy_InitalDataInsert.sql
