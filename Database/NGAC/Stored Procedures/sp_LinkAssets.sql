CREATE PROCEDURE [NGAC].[sp_LinkAssets]

AS
BEGIN
--*******************************************************************************************************************--
--new way Direct joining the location root in the c_controllers
--*******************************************************************************************************************--
--NGAC
UPDATE NGAC.c_controller
SET c_controller.LocationTree = mx.LocationTree 
   ,c_controller.Assetnum = mx.ASSETNUM
   ,c_controller.ProductionTeam = mx.Team
   --geoloc
	,c_controller.Asset_x =  ISNULL(ISNULL(assetlevelXY.X_pos, assetlevelXY2.X_pos),assetlevelXY3.X_pos) 
	,c_controller.Asset_y = ISNULL(ISNULL(assetlevelXY.Y_pos, assetlevelXY2.Y_pos),assetlevelXY3.Y_pos) 
	,c_controller.Asset_png = ISNULL(ISNULL(assetlevelXY.PNG,assetlevelXY2.PNG),assetlevelXY3.PNG) 
	,c_controller.Station_x =  stationlevelXY.X_pos 
	,c_controller.Station_y = stationlevelXY.Y_pos
	,c_controller.Station_png = stationlevelXY.PNG 
FROM NGAC.c_controller as C 
LEFT JOIN EqUi.ASSETS_fromMX7 as mx on mx.LOCATION = C.controller_name AND mx.SYSTEMID = 'PRODMID'
--for geoloc
LEFT OUTER JOIN EqUi.Assets_XY as assetlevelXY ON assetlevelXY.[location] = mx.[LOCATION]
--Asset that are null and have a know controller can join the controller 
LEFT OUTER JOIN EqUi.Assets_XY assetlevelXY2 on assetlevelXY2.[location] = c.controller_name
--Asset that are still null plot them on the station
LEFT OUTER JOIN EqUi.Assets_XY assetlevelXY3 on assetlevelXY3.[location] = mx.Station 
--join station level
LEFT OUTER JOIN EqUi.Assets_XY as stationlevelXY ON stationlevelXY.[location] = mx.Station


--*******************************************************************************************************************--
--calc production responsible and technican responsible for c_controllers
--*******************************************************************************************************************--
--NGAC
UPDATE  NGAC.c_controller
set ResponsibleTechnicianTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.Optgroup = 'TechnicianTeams'
and c.LocationTree like c_ownership.LocationTree 

UPDATE  NGAC.c_controller
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.Optgroup = 'ProductionTeams'
and c.LocationTree like c_ownership.LocationTree 

--*******************************************************************************************************************--
--update hasspotweld bit for ngac
--*******************************************************************************************************************--
/*update  NGAC.c_controller
 set hasspotweld = case
                  when x.controller_id is not null  then 1
                  else 0
                 end
from  NGAC.c_controller
left join (select distinct controller_id from  NGAC.h_TipWearBeforeChange) as x on x.controller_id = c_controller.id*/
--*******************************************************************************************************************--


--*******************************************************************************************************************--
--Shit area !! 
--*******************************************************************************************************************--
--temp for AASPOT guys
update  NGAC.c_controller
set CLassificationId = 'UAWN+UAWB'
  FROM [NGAC].[c_controller]
  where controller_name in
  (
'325010R01',
'325020R01',
'325030R01',
'325040R01',
'326100R01',
'326060R01',
'321010R03'
  )
end