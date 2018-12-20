CREATE PROCEDURE [NGAC].[sp_LinkAssets]

AS
BEGIN
--*******************************************************************************************************************--
--new way Direct joining the location root in the c_controllers
--*******************************************************************************************************************--
--NGAC
UPDATE NGAC.c_controller
SET c_controller.LocationTree = g.LocationTree 
   ,c_controller.Assetnum = g.ASSETNUM
   ,c_controller.ProductionTeam = g.Team
   ,c_controller.Station = g.Station
   ,c_controller.Line = g.Area
   --geoloc
	,c_controller.Asset_x =  g.Asset_x
	,c_controller.Asset_y =  g.Asset_y
	,c_controller.Asset_png = g.Asset_png

	,c_controller.Station_x =  g.Station_x
	,c_controller.Station_y = g.Station_y
	,c_controller.Station_png = g.Station_png

    ,c_controller.Line_x =  g.Line_x
	,c_controller.Line_y = g.Line_y
	,c_controller.Line_png = g.Line_png
FROM NGAC.c_controller as C 
LEFT JOIN EqUi.GeoAssets as g on g.LOCATION = c.controller_name


--*******************************************************************************************************************--
--calc production responsible and technican responsible for c_controllers
--*******************************************************************************************************************--
--NGAC
UPDATE  NGAC.c_controller
set ResponsibleTechnicianTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'TechnicianTeams'
and c.LocationTree like '%'+c_ownership.LocationTree+'%'

UPDATE  NGAC.c_controller
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'ProductionTeams'
and c.LocationTree like '%'+c_ownership.LocationTree+'%'

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