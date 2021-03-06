print 'init check [Alerts].[c_triggers]'

IF (SELECT count(*) FROM  [Alerts].[c_triggers]) = 0
BEGIN
Print 'init data insert [Alerts].[c_triggers]'
SET IDENTITY_INSERT [Alerts].[c_triggers] ON 
--Sample NGAC robot speed low
INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [c_schedule_id], [continueOnJobFailure], [ordinal], [interval], [intervalCounter], [alertGroup], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel], [OptValueLabels]) VALUES (1, 1, N'NGAC SlowSpeed  robots not at 100% speed (>60min)', 0, N'SELECT  
  x._timestamp      AS ''timestamp''
, ''ROBOT SPEED LOW value: '' +  CAST(x.[value] as varchar(3)) + '' since @''+CONVERT(char(19),x._timestamp, 120)   AS ''info''
, c.LocationTree     As ''LocationTree''
, c.ClassificationTree as ''ClassTree''
, c.controller_name		AS ''Location''
, c.controller_name     AS  ''alarmobject''
from (--nested to optimize return result for next join
select 
 rt.*
,ROW_NUMBER() OVER (PARTITION BY rt.c_controller_id, rt.c_variable_id ORDER BY rt._timestamp DESC) AS ''rnDesc''
from GADATA.NGAC.[rtu_SpeedOvr] as rt 
) as x
left join GADATA.NGAC.c_controller as c on c.id = x.c_controller_id
where x.[value] <> 100 AND x.rnDesc = 1
and x._timestamp < GETDATE()-''1900-01-01 01:00:00''', 2, 1, 5, 1, 10, 0, 0, N'Production', N'SlowSpeed', 1, 1, 1, 0, 1, 0, N'select getdate() as ''test2''', N'test ', N'ani_BlinkPink', 0, 1, NULL, NULL)
--Sample Dispense barrel low
INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [c_schedule_id], [continueOnJobFailure], [ordinal], [interval], [intervalCounter], [alertGroup], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel], [OptValueLabels]) VALUES (3, 1, N'NGAC / C3G / C4G BarrelLow Checks warning level for dispense system barrel', 0, N'--FOR NGAC
SELECT  
  x._timestamp      AS ''timestamp''
, ''Dispense Barrel LOW NGAC since @''+CONVERT(char(19),x._timestamp, 120)   AS ''info''
, x.id				 AS ''refId''
, c.LocationTree     As ''LocationTree''
, ''UST'' as ''ClassTree''
, c.controller_name		AS ''Location''
, c.controller_name     AS  ''alarmobject''
from (--nested to optimize return result for next join
select 
 rt.*
,ROW_NUMBER() OVER (PARTITION BY rt.c_controller_id, rt.c_variable_id ORDER BY rt._timestamp DESC) AS ''rnDesc''
from GADATA.NGAC.rt_Disp_BarrelLow as rt 
) as x
left join GADATA.NGAC.c_controller as c on c.id = x.c_controller_id
where x.[value] = 1 AND x.rnDesc = 1', 2, 1, 5, 1, 10, 0, 0, N'Production', N'BarrelLow', 1, 0, 1, 0, 1, 0, NULL, NULL, N'ani_PulseGreen', 0, 1, NULL, NULL)
--Sample no tipchange detected
INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [c_schedule_id], [continueOnJobFailure], [ordinal], [interval], [intervalCounter], [alertGroup], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel], [OptValueLabels]) VALUES (13, 1, N'NGAC looks for no tip change deteced warning', 0, N'SELECT  
  rt.[timestamp]      AS ''timestamp''
, ''No tip change is detected tipNOTchanged@'' + CONVERT(char(19),twbc.TipchangeTimestamp, 120)  AS ''info''
, rt.refId				 AS ''refId''
, rt.LocationTree     As ''LocationTree''
, ''UAWS'' AS ''ClassTree''
, rt.controller_name		AS ''Location''
, rt.controller_name    AS  ''alarmobject''
,twbc.TipchangeTimestamp
,rt.Logcode
,rt.FullLogtext
from GADATA.NGAC.ErrDispLog as rt 
--join last tipchange
left join GADATA.NGAC.TipwearBeforeChange as twbc on twbc.controller_name = rt.controller_name and twbc.tipchangeindex = 1
WHERE  rt.Logcode in (237,261) 
--237 => statgun (with confirm) en robotmounted (no confirm)
--261 => robmounted (confirm)
and rt.FullLogtext like ''%Confirm%'' --trigger only on the confirm event 
and rt.FullLogtext not like ''%Response: NO%'' --on statgun they can pick NO if no change is detected. Filter this out
and rt.[timestamp] between GETDATE()-''1900-01-03 00:00:00'' and GETDATE()--use last 3 days of data on the event side.
and rt.[timestamp] + ''1900-01-01 00:03:00'' > twbc.TipchangeTimestamp --leave alert active until new tipchange is detected', 2, 1, 5, 1, 50, 0, 0, NULL, N'TIPLIFE', 1, 0, 0, 0, 1, 0, NULL, NULL, NULL, 0, 1, NULL, NULL)
--Sample wear ratio alert
INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [c_schedule_id], [continueOnJobFailure], [ordinal], [interval], [intervalCounter], [alertGroup], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel], [OptValueLabels]) VALUES (20, 1, N'NGAC RatioAlert (kijkt of de tipwear ratio de verkeerde kant op gaat)', 0, N'--run alert statement
SELECT 
  rt.[Date Time]      AS ''timestamp''
, ''WearRatioAlert   | ''  + (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) + '' ratio:'' + CAST(ROUND(rt.TipWearRatio,1) as varchar(6)) + ''  [ UCL:'' + CAST(ROUND(limits.UpperLimit,1)  as varchar(16)) + ''| LCL:''  + CAST(ROUND(limits.LowerLimit,1)  as varchar(16)) + '' ]''  AS ''info''
, rt.id				 AS ''refId''
, rt.LocationTree     As ''LocationTree''
, ''UAWS'' as ''ClassTree''
, rt.controller_name		AS ''Location''
, (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) AS  ''alarmobject''
,rt.TipWearRatio
FROM NGAC.TipwearLast as rt
--join controlLimits
left join Alerts.l_controlLimits as limits on 
limits.c_trigger_id = 20 --Get correct set.
AND
limits.alarmobject = (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) --correct object
AND 
rt.[Date Time] BETWEEN limits.CreateDate and ISNULL(limits.ChangeDate,getdate())
where 
rt.TipWearRatio is not null 
and
(
rt.TipWearRatio > UpperLimit
or 
rt.TipWearRatio < LowerLimit
)

', 2, 1, 5, 1, 30, 0, 0, NULL, N'WearRatio', 1, 0, 0, 0, 1, 1, NULL, N'SELECT  
 (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2)))  AS  ''alarmobject''
,rt.[Date Time] AS ''Timestamp''
,rt.TipWearRatio as ''Value''
,CASE 
WHEN @optDatanum = 1 THEN CAST((rt.[Dress_Num]) as float)
ELSE null
END  as ''OptValue''
,rt.id as ''id''
,limits.UpperLimit
,limits.LowerLimit
,limits.id as ''l_controlLimits_id''
,limits.Comment
,limits.CreateDate

FROM NGAC.TipDressLogFile as rt
--join controlLimits
left join GADATA.Alerts.l_controlLimits as limits on 
limits.c_trigger_id = @c_trigger_id
AND
limits.alarmobject = (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) --correct object
AND 
rt.[Date Time] BETWEEN limits.CreateDate and ISNULL(limits.ChangeDate,getdate())
WHERE
(rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) = @alarmobject
order by [Date Time] desc ', N'ani_PulseGreenOnce', 0, 1, N'<%=value%>% ', N'Dress_Num')
--sample wear alert
INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [c_schedule_id], [continueOnJobFailure], [ordinal], [interval], [intervalCounter], [alertGroup], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel], [OptValueLabels]) VALUES (21, 1, N'NGAC WearAlert  FIXTIP (kijkt of de tipwear niet te hoog of te laag is alert sluit als 5 punten na elkaar OK zijn)', 0, N'--run alert statement
SELECT 
  rt.[Date Time]      AS ''timestamp''
, ''WEARING NOK  | ''  + (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) + ''  FIXwear/dress:'' + CAST(ROUND((rt.DiffFrLastWear_Fixed / (rt.Dress_Num - rt.PrevDress_Num)),3) as varchar(6)) + ''  [ UCL:'' + CAST(ROUND(limits.UpperLimit,3)  as varchar(16)) + '' | LCL:''  + CAST(ROUND(limits.LowerLimit,3)  as varchar(16)) + '' ]''  AS ''info''
, rt.id				 AS ''refId''
, rt.LocationTree     As ''LocationTree''
, ''UAWS'' as ''ClassTree''
, rt.controller_name		AS ''Location''
, (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) AS  ''alarmobject''
,rt.DiffFrLastWear_Fixed
,rt.DiffFrLastWear_Move
,rt.Dress_Num
,rt.PrevDress_Num
FROM 

(
select 
 x.*
,LEAD(x.Dress_Num) OVER (PARTITION BY x.controller_name, x.Tool_nr ORDER BY x.[Date Time] DESC) as ''PrevDress_Num''
from(--nested to optimize return result for next join
select
 rt.*
,ROW_NUMBER() OVER (PARTITION BY rt.controller_name, rt.Tool_nr ORDER BY rt.[Date Time] DESC) AS ''rnDesc''
from NGAC.TipDressLogFile as rt with(nolock)
where
--limit the data range we search. (qry performance) If no data for 48 hours we lose it
rt._timestamp between GETDATE() - 2 and GETDATE()
) as x 
where x.rnDesc < 5 --last 5 point must be op to close alert
and x.rnDesc > 2 --2 point in sequence must be out to open alert
) as rt

--join controlLimits
left join Alerts.l_controlLimits as limits on 
limits.c_trigger_id = 21 --Get correct set.
AND
limits.alarmobject = (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) --correct object
AND 
rt.[Date Time] BETWEEN limits.CreateDate and ISNULL(limits.ChangeDate,getdate())
where 
rt.Dress_Num not in (0,1,2,3) --hide init dress
AND 
rt.PrevDress_Num not in (0,1,2,3)
AND
(
--test fixed wearing conf 0.025
(rt.DiffFrLastWear_Fixed / (rt.Dress_Num - rt.PrevDress_Num)) > UpperLimit
or 
(rt.DiffFrLastWear_Fixed / (rt.Dress_Num - rt.PrevDress_Num)) < LowerLimit
)
', 2, 1, 5, 1, 40, 0, 0, N'Qalert', N'WearAlert', 1, 0, 0, 0, 0, 1, NULL, N'SELECT * FROM (
SELECT  
 (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2)))  AS  ''alarmobject''
,rt.[Date Time] AS ''Timestamp''
,ISNULL(ROUND(rt.DiffFrLastWear_Fixed / nullif(rt.Dress_Num - LEAD(rt.Dress_Num) OVER (PARTITION BY rt.controller_name, rt.Tool_nr ORDER BY rt.[Date Time] DESC),0) ,3),0) as ''Value''
,NULL as ''OptValue''
,rt.id as ''id''
,limits.UpperLimit
,limits.LowerLimit
,limits.id as ''l_controlLimits_id''
,limits.Comment
,limits.CreateDate
FROM NGAC.TipDressLogFile as rt
--join controlLimits
left join Alerts.l_controlLimits as limits on 
limits.c_trigger_id = @c_trigger_id
AND
limits.alarmobject = (rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) --correct object
AND 
rt.[Date Time] BETWEEN limits.CreateDate and ISNULL(limits.ChangeDate,getdate())
WHERE
(rt.controller_name +''_Tool'' + CAST(rt.Tool_Nr as varchar(2))) = @alarmobject
) as X
WHERE X.[Value] > 0
order by X.[Timestamp] desc ', N'ani_PulseGreenOnce', 0, 1, N'<%=value%> mm ', NULL)
--Sample SBCU alert
INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [c_schedule_id], [continueOnJobFailure], [ordinal], [interval], [intervalCounter], [alertGroup], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel], [OptValueLabels]) VALUES (24, 0, N'NGAC SBCUalert checks Sbcu measurements', 0, N'--trigger normalisation of setupdata
exec [NGAC].[sp_nom_BeamSetUpLog]
--run alert statement
SELECT 
  max(x.[Timestamp])      AS ''timestamp''
, max(x.info)  AS ''info''
, max(x.refID)		AS ''refId''
, x.LocationTree     As ''LocationTree''
, x.ClassificationTree as ''ClassTree''
, x.controller_name		AS ''Location''
, x.alarmobject AS  ''alarmobject''
FROM
(
SELECT  
 ISNULL(v.[Date Time],DATEADD(Day, DATEDIFF(Day, 0, GETDATE()), 0)) AS ''Timestamp''
,CASE 
WHEN V.ID is not null THEN
''SBCU  | ''  + v.alarmobject + '' DeltaRef:'' + CAST(ROUND(v.DeltaRef,1) as varchar(6)) + ''  [ UCL:'' + CAST(ROUND(limits.UpperLimit,1)  as varchar(16)) + ''| LCL:''  + CAST(ROUND(limits.LowerLimit,1)  as varchar(16)) + '' ]''  
ELSE
''SBCU  |alarmobject:'' + limits.alarmobject + '' HAS NO DATA! (last 30 days)'' 
END as ''info''
,v.ID as ''refID''
,ROW_NUMBER() OVER (PARTITION BY v.alarmobject ORDER BY v.[Date Time] desc) AS ''rnDESC''
,v.DeltaRef
,limits.UpperLimit
,limits.LowerLimit
,ISNULL(v.LocationTree,(select top 1 LocationTree from NGAC.c_controller where controller_name = SUBSTRING (limits.alarmobject ,0 , CHARINDEX(''_gun'',limits.alarmobject))))  as ''LocationTree''
,v.ClassificationTree
,ISNULL(v.controller_name,SUBSTRING (limits.alarmobject ,0 , CHARINDEX(''_gun'',limits.alarmobject))) AS ''controller_name''
,limits.alarmobject AS  ''alarmobject''
,limits.id
,limits.isdead
,limits.CreateDate
FROM Alerts.l_controlLimits as limits 
--join data
left join [NGAC].TCP_LOG  as v  on limits.alarmobject = v.alarmobject --correct object
AND V.[Date Time] between limits.CreateDate and ISNULL(limits.changedate,getdate()) --limit must be older than data
AND v.[Date Time] between getdate()-30 and getdate() --meas data range. (improtant for no data warning)
where 
limits.c_trigger_id = 24 --correct limit set
) as x 

where 
(
x.rnDESC in(1,2,3) --last 3 points
AND
(
x.DeltaRef > x.UpperLimit
or 
x.DeltaRef < x.LowerLimit
)
)
OR 
(
x.refID is null --no data
and
x.isdead = 0 --active limit
and 
x.CreateDate < getdate()-10 --active limit needs to be older than 10 days to be able to make no data alerts
)
GROUP BY
 x.controller_name
,x.LocationTree
,x.ClassificationTree
,x.alarmobject
', 2, 1, 5, 1, 80, 0, 0, NULL, N'SBCUalert', 1, 0, 0, 0, 1, 1, N'SELECT * FROM (
SELECT  
  v.[Date Time] AS ''Timestamp''
, v.ID as ''refID''
, ROW_NUMBER() OVER (PARTITION BY v.alarmobject ORDER BY v.[Date Time] desc) AS ''rnDESC''
, limits.id as ''limitID''
, v.controller_name
, v.alarmobject  AS  ''alarmobject''
FROM [NGAC].TCP_LOG as V 
--join controlLimits
left join GADATA.Alerts.l_controlLimits as limits on 
limits.c_trigger_id = 24 --Get correct set.
AND
limits.alarmobject = v.alarmobject --correct object
AND 
limits.isdead = 0
WHERE
v.[Date Time] BETWEEN GETDATE()-30 and GETDATE() --limit view window
) as x where x.rnDESC = 1 and x.limitID is null', N'SELECT  
 v.alarmobject  AS  ''alarmobject''
,v.[Date Time] AS ''Timestamp''
,v.DeltaRef as ''Value''
,CASE 
WHEN @optDatanum = 1 THEN CAST((v.[New TCP X]) as float)
WHEN @optDatanum = 2 THEN CAST((v.[New TCP Y]) as float)
WHEN @optDatanum = 3 THEN CAST((v.[New TCP Z]) as float)
ELSE null
END  as ''OptValue'' 
,v.id as ''id''
,limits.UpperLimit
,limits.LowerLimit
,limits.id as ''l_controlLimits_id''
,limits.Comment
,limits.CreateDate

FROM NGAC.TCP_LOG as V 
--join controlLimits
left join GADATA.Alerts.l_controlLimits as limits on 
limits.c_trigger_id = @c_trigger_id
AND
limits.alarmobject = v.alarmobject --correct object
AND 
V.[Date Time] BETWEEN limits.CreateDate and ISNULL(limits.ChangeDate,getdate())
WHERE
v.alarmobject = @alarmobject
order by [Date Time] desc ', N'ani_PulseBlueRepeat', 0, 1, N'<%=value%> mm', N'ToolX;Tooly;ToolZ')

END