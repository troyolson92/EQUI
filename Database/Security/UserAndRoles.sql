﻿--passwords will not match if you auto create on other server! set op logins by hand!
--Make users
--
CREATE LOGIN [VASC_a] WITH PASSWORD=N'JweDpPYdey5/9ke2D/IYUuTzR2KqfUB4I23iL+2GV1o=', DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [VASC_a] FOR LOGIN [VASC_a];
GO
GRANT CONNECT TO [VASC_a]
GO
--
CREATE LOGIN [EqUiAdmin] WITH PASSWORD=N'akpC7iTebRcNCl/n/xVVP7g9IMn99R5idbGD11isOSk=', DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [EqUiAdmin] FOR LOGIN [EqUiAdmin];
GO
GO
GRANT CONNECT TO [EqUiAdmin]
GO
--
CREATE LOGIN [GADATA] WITH PASSWORD=N'akpC7iTebRcNCl/n/xVVP7g9IMn99R5idbGD11isOSk=', DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [GADATA] FOR LOGIN [GADATA];
GO
GRANT CONNECT TO [GADATA]
GO
--
CREATE LOGIN [EqUi] WITH PASSWORD=N'akpC7iTebRcNCl/n/xVVP7g9IMn99R5idbGD11isOSk=', DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [EqUi] FOR LOGIN [EqUi];
GO
GRANT CONNECT TO [EqUi]
GO
--
CREATE USER [VCCNET\BPPEQDB1] FOR LOGIN [VCCNET\BPPEQDB1]
    WITH DEFAULT_SCHEMA = [EqUi];


GO

--Make roles
--Administrator access role
CREATE ROLE [db_accessEquiAdmin]
    AUTHORIZATION [dbo];
GO
GRANT SELECT TO [db_accessEquiAdmin];
GO
GRANT INSERT TO [db_accessEquiAdmin];
GO
GRANT UPDATE TO [db_accessEquiAdmin];
GO
GRANT ALTER TO [db_accessEquiAdmin];
GO
GRANT DELETE TO [db_accessEquiAdmin];
GO
GRANT EXECUTE TO [db_accessEquiAdmin];
GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [EqUiAdmin];
GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [VASC_a];
GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [VCCNET\BPPEQDB1];
GO

--Read only role
CREATE ROLE [db_accesEquiReadonly]
    AUTHORIZATION [dbo];
GO
GRANT SELECT TO [db_accesEquiReadonly];
GO
--needed for VSTO plugin to run commands

--add users to roles
GO
ALTER ROLE [db_accesEquiReadonly] ADD MEMBER [EqUi];
GO
ALTER ROLE [db_accesEquiReadonly] ADD MEMBER [GADATA];

