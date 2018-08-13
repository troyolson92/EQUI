CREATE ROLE [db_accessEquiAdmin]
    AUTHORIZATION [dbo];







GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [EqUiAdmin];


GO
ALTER ROLE [db_accessEquiAdmin] ADD MEMBER [VASC_a];

