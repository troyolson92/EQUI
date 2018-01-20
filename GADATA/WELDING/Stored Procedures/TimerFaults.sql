-- =============================================
-- Author:		Coppejans Jens
-- Create date: 23/12/2017
-- Description:	Insert all NPT timerfaults
-- =============================================
CREATE PROCEDURE [WELDING].[TimerFaults] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

---NPT01---

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.189.9\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT04

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.170.244\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT05

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.170.103\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT06

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.184.10\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT07

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.185.36\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT08

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.185.37\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT09

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.179.160\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT10

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.179.161\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT11

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.182.11\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT12

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([136.20.175.212\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT13

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([136.20.175.213\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT14

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([136.20.175.254\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT16

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.176.27\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

-------Y555------V316--------

----NPT22

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.180.162\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT23

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer,TimerFaults.program,TimerFaults.Spot , TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.181.53\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V
WHERE protRecord_ID <>''102''

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT24

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.180.227\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT26

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.181.154\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)


---- NPT27

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.181.155\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT28

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.180.141\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT29

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.180.149\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT30

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.180.203\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT31

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.180.39\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT32

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.192.18\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT33

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([19.148.192.33\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

-----V316 Greenfield-----------------

---- NPT40

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.240.10\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT41

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.240.11\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT42

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.240.12\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---- NPT43

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.240.13\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT50
INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.225.199\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT51

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.225.197\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

---Datachange
INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.225.198\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT53

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.240.14\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT54

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.225.200\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT55

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.226.5\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT56

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.225.133\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT57

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.203.138\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

-- NPT58

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.226.7\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)


--- NPT59

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.226.8\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT70

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.227.69\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT71

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.222.197\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT72

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.225.135\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)

--- NPT73

INSERT INTO WELDING.WeldFaults ([Datetime],[Timer],[program],[spot],[TimerFault],[WeldmasterComment])

SELECT        TimerFaults.dateTime, TimerFaults.Timer, TimerFaults.program, TimerFaults.Spot, TimerFaults.TimerFault,TimerFaults.WeldmasterComment
FROM            OPENQUERY([10.249.222.198\BOS_SQLSERV_2005], 
                         ' 
SELECT        dateTime, timerName AS Timer, progNo AS program, spotName AS Spot, monitorState_txt AS TimerFault, null as WeldmasterComment
FROM            dbo.ExtWeldFaultProt_V

')
                          AS TimerFaults LEFT OUTER JOIN
                         WELDING.Weldfaults ON TimerFaults.dateTime = WELDING.Weldfaults.Datetime

WHERE        (WELDING.Weldfaults.Datetime IS NULL)


------------------Make TOP10 Weldfaults GA & insert into T10 Table-------------------------

-----DELETE TOP10 Table------



----Insert new TOP10-------
DELETE FROM [WELDING].[TOP10TimerfaultsLast24H]
INSERT INTO [WELDING].[TOP10TimerfaultsLast24H] ([NPT],[Timer],[TimerFault],[Count of Timerfaults],[WeldingMaster Action])

SELECT        TOP (10) dbo.NPT.Name AS NPT, WELDING.Weldfaults.Timer, WELDING.Weldfaults.TimerFault, COUNT(WELDING.Weldfaults.TimerFault) AS [Aantal TimerFaults] 
               , WELDING.Weldfaults.WeldmasterComment         

FROM            dbo.ResponsibilityWM RIGHT OUTER JOIN
                dbo.NPT ON dbo.ResponsibilityWM.NPT = dbo.NPT.Name LEFT OUTER JOIN
                dbo.Timer ON dbo.NPT.ID = dbo.Timer.NptId AND dbo.NPT.ID = dbo.Timer.NptId AND dbo.NPT.ID = dbo.Timer.NptId RIGHT OUTER JOIN
                WELDING.Weldfaults ON dbo.Timer.Name = WELDING.Weldfaults.Timer
WHERE        (WELDING.Weldfaults.Datetime >= GETDATE() - 1)

GROUP BY WELDING.Weldfaults.Timer, dbo.NPT.Name, dbo.ResponsibilityWM.responsibilityWM, WELDING.Weldfaults.TimerFault, WELDING.Weldfaults.WeldmasterComment  

ORDER BY [Aantal TimerFaults] DESC



END