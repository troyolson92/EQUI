print 'init check [EqUi].[c_datasource]'
IF (SELECT count(*) FROM  [EqUi].[c_datasource]) = 0
BEGIN
Print 'init data insert [EqUi].[c_datasource]'
SET IDENTITY_INSERT [EqUi].[c_datasource] ON 

INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (1, N'$(DatabaseName)', N'$(DatabaseName) ', 1, N'user id=$(DB_C_NAME); password=$(DB_C_PASSWORD); server=$(DB_C_SERVER) Trusted_Connection=no; database=$(DatabaseName); connection timeout=15', 1)

INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (6, N'MAXIMO7rep', N'Maximo 7 reporting server. (daily refresh)', 2, N'Data Source=(description=	
            (address=	(community=tcpcomm)	(protocol=tcp)	(host=gotsvl2149.got.volvocars.net)	(port=1521))	
            (connect_data=	(server=dedicated)	(sid=dpmxarct)))
            ;User Id=ARCTVCG;Password=XXXXX;', 0)

INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (10, N'MAXIMOrt', N'Maximo 7 REAL TIME (dangerous GOBAL server)', 2, N'Data Source=(DESCRIPTION=
(ADDRESS=(PROTOCOL=TCP)(HOST=gotora1mxa.got.volvocars.net)(PORT=1521)) (CONNECT_DATA=(SID=DPMXADGP)));User Id=FDENAYER;Password=XXXXX;', 1)

SET IDENTITY_INSERT [EqUi].[c_datasource] OFF
END