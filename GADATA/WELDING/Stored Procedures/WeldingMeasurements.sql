-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [WELDING].[WeldingMeasurements]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN

	SET NOCOUNT ON;

---Look after new Timers ON linked NPT servers---

INSERT INTO WELDING.dbotesttimer ([Name],[NptId],[Robot])
SELECT timerName,null as NPTID,null as robot
FROM 
(SELECT DISTINCT Timername
FROM [19.148.189.9\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.222.197\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.222.198\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.225.133\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.225.135\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [10.249.225.197\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.225.198\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.225.199\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [10.249.225.200\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [10.249.226.5\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [10.249.226.7\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.226.8\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [10.249.227.69\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.240.10\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.240.11\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [10.249.240.12\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.240.13\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [10.249.240.14\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [136.20.175.212\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [136.20.175.213\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [136.20.175.254\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.170.103\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.170.244\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.176.27\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.179.160\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.179.161\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.180.141\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.180.149\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.180.162\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.180.203\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.180.227\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.180.39\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.181.154\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.181.155\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.181.53\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.182.11\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.184.10\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.185.36\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.185.37\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT
FROM [19.148.189.9\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.192.18\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT 
FROM [19.148.192.33\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]

						  
						  ) AS timers LEFT OUTER JOIN

                         WELDING.dbotesttimer ON timers.Timername = WELDING.dbotesttimer.Name
WHERE        (WELDING.dbotesttimer.Name IS NULL)




---- Update the SpotTable----and insert into dbo.spot----

INSERT INTO WELDING.dbotestspot ( [Number]
      ,[Zone]
      ,[Comment1]
      ,[Comment2]
      ,[Comment3]
      ,[PlateCombinationtId]
      ,[Program]
      ,[TimerID]
      ,[ElectrodeDia]
      ,[AlternativeNumber])
SELECT [spotname]
      ,null as [Zone]
      ,null as [Comment1]
      ,null as [Comment2]
      ,null as [Comment3]
      ,null as [PlateCombinationtId]
      ,[WeldProgNo]
      ,[TimerID]
      ,null as [ElectrodeDia]
      ,null as [AlternativeNumber]
FROM
 (
SELECT DISTINCT Timername,SpotName,WeldProgNo
FROM [19.148.189.9\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo
FROM [10.249.222.197\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.222.198\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.225.133\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.225.135\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.225.197\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.225.198\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.225.199\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.225.200\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.226.5\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.226.7\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.226.8\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.227.69\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.240.10\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.240.11\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.240.12\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.240.13\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [10.249.240.14\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [136.20.175.212\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [136.20.175.213\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [136.20.175.254\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.170.103\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.170.244\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.176.27\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.179.160\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.179.161\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.180.141\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.180.149\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.180.162\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.180.203\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.180.227\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.180.39\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.181.154\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.181.155\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.181.53\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.182.11\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.184.10\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.185.36\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.185.37\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.189.9\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.192.18\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]
UNION
SELECT DISTINCT TimerName COLLATE DATABASE_DEFAULT,SpotName COLLATE DATABASE_DEFAULT,WeldProgNo 
FROM [19.148.192.33\BOS_SQLSERV_2005].[BOS_6000_DB].[dbo].[ExtSpotTable_V]

						  
						  ) AS spottable LEFT OUTER JOIN

WELDING.dbotestspot ON spottable.SpotName = WELDING.dbotestspot.Number 



--------------Midair Measurements Greenfield------------------------


---insert all new midair data into welding.midair table-----


--NPT40
insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.240.10\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)




--NPT41

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.240.11\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


--NPT43

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.240.13\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)




--NPT50

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.225.199\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)



--NPT51
insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.225.197\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)




---NPT52
insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.225.198\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT53
insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.240.14\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)



---NPT54
insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.225.200\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT55
insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.226.5\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT56

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.225.133\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT57

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.203.138\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT58
insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.226.7\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT59

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.226.8\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT70

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.227.69\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)



---NPT71

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.222.197\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT72

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.225.135\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)


---NPT73

insert into WELDING.midairs ( [date] ,[Timer],[Program],[wear] ,[regulation] ,[DressCounter],[ElectrodeNr] ,[WeldTime] ,[Energy] ,[ResistanceActual] ,[ResistanceRef] ,[UIRMeasuring] ,[UIRmonitoring])

SELECT      Midairsgreenfield.dateTime, Midairsgreenfield.timerName, Midairsgreenfield.progNo, Midairsgreenfield.wear, Midairsgreenfield.regulation2_txt, Midairsgreenfield.tipDressCounter, Midairsgreenfield.electrodeNo,Midairsgreenfield.weldTimeActualValue, Midairsgreenfield.energyActualValue, Midairsgreenfield.resistanceActualValue, 
                         Midairsgreenfield.resistanceRefValue, Midairsgreenfield.uirMeasuringActive, Midairsgreenfield.uirMonitoringActive
FROM            OPENQUERY([10.249.222.198\BOS_SQLSERV_2005], 
                         '
SELECT        dateTime, timerName, progNo, wear, regulation2_txt, tipDressCounter, electrodeNo, weldTimeActualValue, energyActualValue, resistanceActualValue, 
                         resistanceRefValue, uirMeasuringActive, uirMonitoringActive
FROM            dbo.ExtWeldMeasureProtDDW_V
WHERE        (progNo = ''253'') OR
                         (progNo = ''248'')
')
                          AS Midairsgreenfield LEFT OUTER JOIN
                         WELDING.Midairs ON Midairsgreenfield.dateTime = WELDING.Midairs.date
WHERE        (WELDING.Midairs.date IS NULL)

	
END