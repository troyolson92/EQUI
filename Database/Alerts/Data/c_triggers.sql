print 'init check [Alerts].[c_triggers]'

IF (SELECT count(*) FROM  [Alerts].[c_triggers]) = 0
Print 'init data insert [Alerts].[c_triggers]'
BEGIN
SET IDENTITY_INSERT [Alerts].[c_triggers] ON 

INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [Cron], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel]) VALUES (1, 1, N'NGAC SlowSpeed  robots not at 100% speed (>60min)', 0, N'SELECT  
  x._timestamp      AS ''timestamp''
, ''ROBOT SPEED LOW value: '' +  CAST(x.[value] as varchar(3)) + '' since @''+CONVERT(char(19),x._timestamp, 120)   AS ''info''
, x.id				 AS ''refId''
, c.LocationTree     As ''LocationTree''
, c.ClassificationTree as ''ClassTree''
, c.controller_name		AS ''Location''
, c.controller_name     AS  ''alarmobject''
from (--nested to optimize return result for next join
select 
 rt.*
,ROW_NUMBER() OVER (PARTITION BY rt.c_controller_id, rt.c_variable_id ORDER BY rt._timestamp DESC) AS ''rnDesc''
from NGAC.[rtu_SpeedOvr] as rt 
) as x
left join NGAC.c_controller as c on c.id = x.c_controller_id
where x.[value] <> 100 AND x.rnDesc = 1
and x._timestamp < GETDATE()-''1900-01-01 01:00:00''', 2, 1, N'*/5 * * * *', N'SlowSpeed', 1, 1, 1, 0, 1, 0, N'select getdate() as ''test2''', N'test ', N'ani_BlinkPink', 0, 1, NULL)

INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [Cron], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel]) VALUES (3, 1, N'NGAC BarrelLow Checks warning level for dispense system barrel', 0, N'SELECT  
  x._timestamp      AS ''timestamp''
, ''Dispense Barrel LOW since @''+CONVERT(char(19),x._timestamp, 120)   AS ''info''
, x.id				 AS ''refId''
, c.LocationTree     As ''LocationTree''
, ''UST'' as ''ClassTree''
, c.controller_name		AS ''Location''
, c.controller_name     AS  ''alarmobject''
from (--nested to optimize return result for next join
select 
 rt.*
,ROW_NUMBER() OVER (PARTITION BY rt.c_controller_id, rt.c_variable_id ORDER BY rt._timestamp DESC) AS ''rnDesc''
from NGAC.rt_Disp_BarrelLow as rt 
) as x
left join NGAC.c_controller as c on c.id = x.c_controller_id
where x.[value] = 1 AND x.rnDesc = 1', 4, 1, N'0 * * * *', N'BarrelLow', 1, 0, 1, 0, 1, 0, NULL, NULL, N'ani_PulseGreen', 0, 1, NULL)

INSERT [Alerts].[c_triggers] ([id], [enabled], [discription], [RunAgainst], [sqlStatement], [smsSystem], [initial_state], [Cron], [alertType], [AutoSetStateTechComp], [smsOnRetrigger], [enableSMS], [isDowntime], [isInReport], [hasControlLimits], [controllimitSqlStatement], [controlChartSqlStatement], [Animation], [isDebugmode], [c_datasource_id], [controlChartYlabel]) VALUES (13, 1, N'NGAC looks for no tip change deteced warning', 0, N'SELECT  
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
and rt.[timestamp] + ''1900-01-01 00:03:00'' > twbc.TipchangeTimestamp --leave alert active until new tipchange is detected', 2, 1, N'*/5 * * * *', N'TIPLIFE', 1, 0, 0, 0, 1, 0, NULL, NULL, NULL, 0, 1, NULL)
END