-------------------------------------------------------------------------------
CREATE PROCEDURE [WELDING].[SpatterDATA]
	-- Add the parameters for the stored procedure here


  ------------------------------------------------------------------------------
AS
BEGIN
SET NOCOUNT ON;

---NPT26

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([19.148.181.154\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT28

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname COLLATE database_default  , Expulsions.wear , Expulsions.tipDressCounter , Expulsions.partIdentString, 
                         Expulsions.WeldTime , Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([19.148.180.141\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT29

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([19.148.180.149\BOS_SQLSERV_2005 ],' 
                         
SELECT [dateTime],spotname   ,null as [wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT30

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([19.148.180.203\BOS_SQLSERV_2005 ],' 
                     
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)   
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT31

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([19.148.180.39\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT32

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([19.148.192.18\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT33

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([19.148.192.33\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT70

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([10.249.227.69\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT71

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([10.249.222.197\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 

---NPT72

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([10.249.225.135\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL)

---NPT73

INSERT INTO WELDING.expulsionDATA ([DateTime],[spot] ,[Wear],[Tipdresscounter] ,[PartIdent] ,[WeldTime],[Energy],[UIP],[PSF],[ExpulsionTime],[Resistance])

SELECT        Expulsions.dateTime, Expulsions.spotname, Expulsions.wear, Expulsions.tipDressCounter, Expulsions.partIdentString, 
                         Expulsions.WeldTime, Expulsions.Energy, Expulsions.uip, Expulsions.PSF, Expulsions.ExpulsionTime, 
                         Expulsions.Resistance
FROM            OPENQUERY ([10.249.222.198\BOS_SQLSERV_2005],' 
                         
SELECT [dateTime],spotname   ,[wear] ,[tipDressCounter] ,[partIdentString]  ,[weldTimeActualValue] As WeldTime  ,[energyActualValue] As Energy  ,[uipActualValue] As uip  ,[stabilisationFactorActValue] As PSF
 ,[uirExpulsionTime] As ExpulsionTime ,[resistanceActualValue] As Resistance
 
FROM [BOS_6000_DB].[dbo].[ExtWeldMeasureProtDDW_V]
WHERE (uirExpulsionTime > 0)
')

                          AS Expulsions INNER JOIN
                         WELDING.ExpulsionZones ON Expulsions.spotname = WELDING.ExpulsionZones.Number LEFT OUTER JOIN
                         WELDING.ExpulsionDATA ON Expulsions.dateTime = WELDING.ExpulsionDATA.DateTime
WHERE        (WELDING.ExpulsionDATA.DateTime IS NULL) 



--------------------------------------------------------------------------------------------

--DELETE records older than 5 days

DELETE from Welding.ExpulsionDATA
WHERE datetime <DATEADD(day,-5,GETDATE())


---------------------------------------------------------------------------------------------
--When imported Query JOIN with zones & comments

---delete null values-----


  DELETE 
  FROM [GADATA].[WELDING].[ExpulsionDATA]
  WHERE Datetime is null




END