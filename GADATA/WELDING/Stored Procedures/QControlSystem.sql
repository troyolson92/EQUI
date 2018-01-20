-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [WELDING].[QControlSystem]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

---- insert parameter values----


----UIT adaptief----

--NPT01---
----update regulation (P)---  NEW INTO dbo.WELDPARAMETER

UPDATE [dbo].[WeldParameters]

SET    [dbo].[WeldParameters].Value = uitadaptief.reg


FROM            OPENQUERY([19.148.189.9\BOS_SQLSERV_2005], 
                         '
SELECT DISTINCT dbo.ExtParamValues_V.value AS reg, dbo.ExtSpotTable_V.spotName
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')
                          AS uitadaptief INNER JOIN
                         dbo.TimerParameterName INNER JOIN
                         dbo.WeldParameters ON dbo.TimerParameterName.ID = dbo.WeldParameters.ParameterNameID INNER JOIN
                         dbo.Spot ON dbo.WeldParameters.SpotID = dbo.Spot.ID ON uitadaptief.spotName = dbo.Spot.Number
WHERE        (dbo.TimerParameterName.LocalDbID = 2327)
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.189.9\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.189.9\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot



---NPT04---						


----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.170.244\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.170.244\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.170.244\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT05---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.170.103\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.170.103\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.170.103\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT06---


----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.184.10\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.184.10\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.184.10\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
---NPT07---					

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.185.36\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.185.36\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.185.36\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT08---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.185.37\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.185.37\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.185.37\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
---NPT09---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.179.160\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.179.160\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.179.160\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT10---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.179.161\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.179.161\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.179.161\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
---NPT11---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.182.11\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.182.11\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.182.11\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	
---NPT12---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([136.20.175.212\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([136.20.175.212\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([136.20.175.212\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot								 
---NPT13---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([136.20.175.213\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([136.20.175.213\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([136.20.175.213\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	
---NPT14---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([136.20.175.254\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([136.20.175.254\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([136.20.175.254\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	
---NPT16---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.176.27\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.176.27\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.176.27\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''80000'' AND ''89999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT22---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.180.162\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.180.162\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.180.162\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT23---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.181.53\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.181.53\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.181.53\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot		
						 
---NPT24---						 				 

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.180.227\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.180.227\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.180.227\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''29999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	
						 
---NPT26---						 	

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.181.154\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.181.154\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.181.154\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot							 							 						 							 					 				
---NPT27---


----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.181.155\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.181.155\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.181.155\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT28---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.180.141\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.180.141\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.180.141\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

---NPT29---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.180.149\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.180.149\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.180.149\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
---NPT30---

----update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.180.203\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.180.203\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.180.203\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
---NPT31---

--update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.180.39\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.180.39\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.180.39\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	
---NPT32---

--update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.192.18\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.192.18\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.192.18\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	
---NPT33---

--update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([19.148.192.33\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([19.148.192.33\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([19.148.192.33\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''20000'' AND ''49999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	

						 ---NPT40---

--update regulation---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.240.10\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.240.10\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.240.10\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	

						 ---NPT41---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.240.11\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.240.11\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.240.11\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot		

						 ---NPT43---
UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.240.13\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.240.13\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.240.13\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	

						 ---NPT50---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.225.199\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.225.199\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.225.199\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	

						 ---NPT51---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.225.197\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.225.197\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.225.197\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	

						 ---NPT52---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.225.198\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.225.198\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.225.198\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

						 ---NPT53---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.240.14\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.240.14\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.240.14\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot	

						 ---NPT54---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.225.200\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.225.200\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.225.200\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
						 ---NPT55---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.226.5\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.226.5\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.226.5\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot


						 ---NPT56---


UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.225.133\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.225.133\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.225.133\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

						 ---NPT57---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.203.138\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.203.138\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.203.138\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

						---NPT58---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.226.7\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.226.7\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.226.7\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

						 ---NPT59---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.226.8\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.226.8\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.226.8\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot


						 ---NPT70---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.227.69\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.227.69\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.227.69\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

						 ---NPT71---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.222.197\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.222.197\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.222.197\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

						 ---NPT72---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.225.135\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.225.135\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.225.135\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

						 ---NPT73---

UPDATE WELDING.spot 
SET WELDING.spot.[regulation] = uitadaptief.reg

FROM OPENQUERY ([10.249.222.198\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS reg
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2327) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot
----update monitoring---
UPDATE WELDING.spot 
SET WELDING.spot.[monitoring] = uitadaptief.mon

FROM OPENQUERY ([10.249.222.198\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS mon
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2363) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot

----update tolerantieband PSF---
UPDATE WELDING.spot 
SET WELDING.spot.[cond.tol.band PSF] = uitadaptief.tol

FROM OPENQUERY ([10.249.222.198\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS tol
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 2974) AND (dbo.ExtSpotTable_V.spotName BETWEEN ''4400000'' AND ''4419999'')

 ')

 AS uitadaptief inner JOIN
                         WELDING.spot ON uitadaptief.spotname = WELDING.spot.Spot


-----de meest dringende spots gaan filteren om zeker te gaan controleren-----

---insert into the new Q control table----

-------Insert data in 'uit adaptief' column ----

UPDATE WELDING.Qcontrol

SET WELDING.Qcontrol.[Uit adaptief] = 
CASE WHEN WELDING.spot.regulation = '0' AND  WELDING.spot.monitoring = '0' THEN '1' ELSE '0' END

FROM  WELDING.spot INNER JOIN WELDING.Qcontrol ON WELDING.spot.spot = WELDING.Qcontrol.spot 

---Update losse spotmelding from Qteam and insert into Qcontrol table----
--create temp table---

 CREATE TABLE #TempTable(
 Time datetime,
 Loose bit,
 Number int)

INSERT INTO #TempTable (Time, Loose, Number) 

SELECT   MAX(dbo.UltralogInspections.InspectionTime) AS time, dbo.UltralogInspections.Loose, dbo.Spot.Number
FROM            dbo.UltralogInspections INNER JOIN
                         dbo.Spot ON dbo.UltralogInspections.SpotID = dbo.Spot.ID
WHERE        (dbo.UltralogInspections.InspectionTime >= GETDATE() - 2)
GROUP BY dbo.Spot.Number, dbo.UltralogInspections.Loose
HAVING        (dbo.UltralogInspections.Loose = 1)


UPDATE WELDING.Qcontrol

SET WELDING.Qcontrol.ColdWeldReport = 

CASE WHEN  #TempTable.Loose = '1' 

THEN  '1'  ELSE '0' END 

FROM  WELDING.Qcontrol LEFT OUTER  JOIN #TempTable ON WELDING.Qcontrol.spot = #TempTable.Number
--delete temp table----
DROP TABLE #TempTable

--- Look after big parameter changes in the datachange from the welding timers
---Datachange is fresh from 1h trigger stored procedure----                    

--Make temp table---

 CREATE TABLE #TempTable1(
 Number int,
 weldtimeReduce int)

--reduce in WeldingTime---

INSERT INTO #TempTable1
SELECT        WELDING.spot.Spot, SUM(CAST(WELDING.Datachanges.OldValue AS int) - CAST(WELDING.Datachanges.NewValue AS int)) 
                         AS WeldingTimeReduce
FROM            WELDING.Datachanges INNER JOIN
                         WELDING.spot ON WELDING.Datachanges.Timer = WELDING.spot.Timer AND WELDING.Datachanges.program = WELDING.spot.Program
WHERE        (WELDING.Datachanges.Timestamp >= GETDATE() - 1) AND (WELDING.Datachanges.Parameter = '2. Weld Time')
GROUP BY WELDING.spot.Spot
HAVING        (SUM(CAST(WELDING.Datachanges.OldValue AS int) - CAST(WELDING.Datachanges.NewValue AS int)) < - 50)


---Update the Qcontrol table---

UPDATE WELDING.Qcontrol

SET WELDING.Qcontrol.[ReduceWeldingTime] = 

CASE WHEN  #TempTable1.weldtimeReduce < '-50'

THEN  '1'  ELSE '0' END 

FROM  WELDING.Qcontrol LEFT OUTER JOIN #TempTable1 ON WELDING.Qcontrol.spot = #TempTable1.Number

--delete temp table----
DROP TABLE #TempTable1

---Changes in Current---   ----Nog NOK problemen met convert varchar to decimal----


--Make temp table---

 ---CREATE TABLE #TempTable2(
 ---Number int,
 ---CurrentReduce float )



---INSERT INTO #TempTable2
---SELECT        WELDING.spot.Spot, 10 * (try_CAST(WELDING.Datachanges.OldValue AS DECIMAL(10,5)) - try_CAST(WELDING.Datachanges.NewValue AS DECIMAL(10,5))) 
    ---                     AS CurrentReduce
---FROM            WELDING.Datachanges INNER JOIN
  ---                       WELDING.spot ON WELDING.Datachanges.Timer = WELDING.spot.Timer AND WELDING.Datachanges.program = WELDING.spot.Program
---WHERE        (WELDING.Datachanges.Timestamp >= GETDATE() - 1) AND (WELDING.Datachanges.Parameter = '2. Heat')
---GROUP BY WELDING.spot.Spot
---HAVING        (SUM(CAST(WELDING.Datachanges.OldValue AS DECIMAL(10,5)) - CAST(WELDING.Datachanges.NewValue AS DECIMAL(10,5))) < - 0.5)


---Update the Qcontrol table---

---UPDATE WELDING.Qcontrol

---SET WELDING.Qcontrol.[ReduceCurrent] = 

---CASE WHEN  #TempTable2.CurrentReduce < '-0.5'

---THEN  '1'  ELSE '0' END 

---FROM  WELDING.Qcontrol LEFT OUTER JOIN #TempTable2 ON WELDING.Qcontrol.spot = #TempTable2.Number

--delete temp table----
---DROP TABLE #TempTable2


---SBCU afwijkingen---

--- C3G robots control----
CREATE TABLE #TempTable3(

 Timer varchar(25))

INSERT INTO #TempTable3
SELECT dbo.Timer.Name As Timer
FROM   dbo.SBCUAlertsLast24H INNER JOIN
              dbo.Timer ON dbo.SBCUAlertsLast24H.Robot = dbo.Timer.Robot

UPDATE WELDING.Qcontrol

SET WELDING.Qcontrol.SBCUAlert = 

CASE WHEN  #TempTable3.Timer BETWEEN '10000WT01' AND '99999WT01'  THEN  '1'  ELSE '0' END 

FROM  WELDING.Qcontrol LEFT OUTER JOIN #TempTable3 ON WELDING.Qcontrol.Timer = #TempTable3.Timer

DROP TABLE #TempTable3


--- Search after big changes in energy drop ---- >1500 Joule decrease in a week ----

--Make temp table---

CREATE TABLE #TempTable4(
 Number int,
 EnergyDrop float )

INSERT INTO #TempTable4

SELECT        dbo.Spot.Number, MAX(dbo.WeldMeasurements.AvgEnergy) - MIN(WeldMeasurements_1.AvgEnergy) AS EnergyDifference
FROM          dbo.Spot INNER JOIN
              dbo.WeldMeasurements ON dbo.Spot.ID = dbo.WeldMeasurements.SpotId INNER JOIN
              dbo.WeldMeasurements AS WeldMeasurements_1 ON dbo.Spot.ID = WeldMeasurements_1.SpotId
WHERE        (WeldMeasurements_1.Date >= GETDATE() - 2) AND (dbo.WeldMeasurements.Date >= GETDATE() - 7)
GROUP BY      dbo.Spot.Number
HAVING       (MAX(dbo.WeldMeasurements.AvgEnergy) - MIN(WeldMeasurements_1.AvgEnergy) > 1800)
ORDER BY EnergyDifference DESC

UPDATE WELDING.Qcontrol

SET WELDING.Qcontrol.EnergyDrop = 

CASE WHEN  #TempTable4.EnergyDrop > '1800'  THEN  '1'  ELSE '0' END 

FROM  WELDING.Qcontrol LEFT OUTER JOIN #TempTable4 ON WELDING.Qcontrol.spot = #TempTable4.Number

--delete temp table----
DROP TABLE #TempTable4










						 	


END