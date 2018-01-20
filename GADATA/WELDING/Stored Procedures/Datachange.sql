-- =============================================
-- Author:		coppejans jens
-- Create date: 22/12/2017
-- Description:	Datachanges
-- =============================================
CREATE PROCEDURE [WELDING].[Datachange]
	

AS
BEGIN

SET NOCOUNT ON;

----Datachange NPT01

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.189.9\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT04

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.170.244\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT05

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.datetime, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.170.103\BOS_SQLSERV_2005], 
                         ' 
                SELECT        Datetime, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            [BOS_6000_DB].dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (DATETIME >=''2017-01-01 00:00:00'')

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.datetime = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT06

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.184.10\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT07

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.185.36\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT08

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.185.37\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT09

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.179.160\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT10

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.179.161\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT11

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.182.11\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT12

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([136.20.175.212\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT13

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([136.20.175.213\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT14

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([136.20.175.254\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT16

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.176.27\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

-------Y555------V316--------

----Datachange NPT22

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.180.162\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT23

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.181.53\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT24

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.180.227\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT26

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.181.154\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)


----Datachange NPT27

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.181.155\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT28

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.180.141\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT29

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.180.149\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT30

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.180.203\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT31

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.180.39\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT32

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.192.18\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT33

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([19.148.192.33\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

-----V316 Greenfield-----------------

----Datachange NPT40

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.240.10\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT41

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.240.11\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT42

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.240.12\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

----Datachange NPT43

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.240.13\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT50

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.225.199\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT51

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.225.197\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT52

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.225.198\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT53

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.240.14\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT54

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.225.200\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT55

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.226.5\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT56

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.225.133\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT57

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.203.138\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT58

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.226.7\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)


---Datachange NPT59

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.226.8\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT70

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.227.69\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT71

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.222.197\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT72

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.225.135\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)

---Datachange NPT73

INSERT INTO WELDING.Datachanges ([Timestamp],[Timer] ,[Program],[Parameter] ,[OldValue] ,[NewValue],[CDSID])

SELECT        Datachange.Timestamp AS timestamp, Datachange.timerName AS Timer, Datachange.subIndex AS [Program/electrode], Datachange.param_status_txt, 
                         Datachange.oldValue_txt AS OldValue, Datachange.newValue_txt AS NewValue, Datachange.userName AS CDSID
FROM            OPENQUERY([10.249.222.198\BOS_SQLSERV_2005], 
                         ' 
                SELECT        CONVERT(Datetime, dateTime, 120) AS Timestamp, timerName, param_status_txt, subIndex, oldValue_txt, newValue_txt, userName
                FROM            dbo.ExtDataChangeProt_V
                WHERE        (param_status_txt <> ''Weld internal (T)'') AND (param_status_txt <> ''Weld/No weld (T)'') AND (CONVERT(Datetime, dateTime, 120) > CONVERT(DATETIME,''2017-01-01 00:00:00'', 102))

')
                          AS Datachange LEFT OUTER JOIN
                         WELDING.Datachanges ON Datachange.Timestamp = WELDING.Datachanges.Timestamp
WHERE        (WELDING.Datachanges.Timestamp IS NULL)





END