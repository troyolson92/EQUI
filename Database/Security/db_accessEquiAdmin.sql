CREATE ROLE [db_accessEquiAdmin] AUTHORIZATION [dbo];

GO
GRANT SELECT TO [db_accessEquiAdmin];
GO
GRANT ALTER TO [db_accessEquiAdmin];
GO
GRANT DELETE TO [db_accessEquiAdmin];
GO
GRANT EXECUTE TO [db_accessEquiAdmin];
GO
GRANT EXECUTE TO [db_accessEquiAdmin];

GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [EqUiAdmin];


GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [VASC_a];

