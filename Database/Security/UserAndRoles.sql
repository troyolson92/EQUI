--do passwords will not match if you autocreate on other server! set op logins by hand!

CREATE LOGIN [VASC_a] WITH PASSWORD=N'JweDpPYdey5/9ke2D/IYUuTzR2KqfUB4I23iL+2GV1o=', DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [VASC_a] FOR LOGIN [VASC_a];
GO
CREATE LOGIN [EqUiAdmin] WITH PASSWORD=N'akpC7iTebRcNCl/n/xVVP7g9IMn99R5idbGD11isOSk=', DEFAULT_DATABASE=[$(DatabaseName)], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
CREATE USER [EqUiAdmin] FOR LOGIN [EqUiAdmin];
GO
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

---GO
--ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [VCCNET\BPPEQDB1];



GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [VCCNET\BPPEQDB1];

