print 'init check [EqUi].[c_datasource]'
IF (SELECT count(*) FROM  [EqUi].[c_datasource]) = 0
Print 'init data insert [EqUi].[c_datasource]'
BEGIN
SET IDENTITY_INSERT [EqUi].[c_datasource] ON 

INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (1, N'GADATA', N'GADATA ', 1, N'user id=EqUiAdmin; password=EqUiAdmin; server=SQLA001.gen.volvocars.net Trusted_Connection=no; database=gadata; connection timeout=15', 1)

INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (6, N'MAXIMO7rep', N'Maximo 7 reporting server. (daily refresh)', 2, N'Data Source=(description=	
            (address=	(community=tcpcomm)	(protocol=tcp)	(host=gotsvl2149.got.volvocars.net)	(port=1521))	
            (connect_data=	(server=dedicated)	(sid=dpmxarct)))
            ;User Id=ARCTVCG;Password=vcg$tokfeb2017;', 0)
INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (7, N'DBI', N'DBI database. (owned by IT and used for BI) ', 2, N'Data Source= (DESCRIPTION=
    (ADDRESS=
      (COMMUNITY=tcp.world)
      (PROTOCOL=TCP)
      (HOST=SDBIGP)
      (PORT=49949)
    )
    (CONNECT_DATA=
      (SERVER=dedicated)
      (SID=dbi)
    )
  )
;User Id=DBI_TABLEAU;Password=dbi_tableau;', 1)

INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (9, N'DST', N'DST database. (STO siemens TIA)', 2, N'Data Source= (DESCRIPTION=
            (ADDRESS=
              (COMMUNITY=tcp.world)
              (PROTOCOL=TCP)
              (HOST=nvr.gent.vcc.ford.com)
              (PORT=49970)
            )
            (CONNECT_DATA=
              (SID=DST)
            )
          )
        ;User Id=STO_SYS_READONLY;Password=sto_sys_readonly1', 1)
INSERT [EqUi].[c_datasource] ([Id], [Name], [Description], [Type], [ConnectionString], [isAlertSource]) VALUES (10, N'MAXIMOrt', N'Maximo 7 REAL TIME (dangerous GOBAL server)', 2, N'Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=gotora1mxa.got.volvocars.net)(PORT=1521)) (CONNECT_DATA=(SID=DPMXADGP)));User Id=FDENAYER;Password=volvo321;', 1)

SET IDENTITY_INSERT [EqUi].[c_datasource] OFF
END