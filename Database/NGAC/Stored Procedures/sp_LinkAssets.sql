






CREATE PROCEDURE [ngac].[sp_LinkAssets]

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
  -- ,c_controller.ClassificationTree = mx.ClassificationTree
  -- ,c_controller.CLassificationId = mx.CLassificationId
FROM NGAC.c_controller as C 
LEFT JOIN EqUi.ASSETS_fromMX7 as mx on mx.LOCATION = C.controller_name AND mx.SYSTEMID = 'PRODMID'


--*******************************************************************************************************************--
--calc production responsible and technican responsible for c_controllers
--*******************************************************************************************************************--
--NGAC
UPDATE  NGAC.c_controller
set ResponsibleTechnicianTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'TechnicianTeams'
and c.LocationTree like c_ownership.LocationTree 

UPDATE  NGAC.c_controller
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'ProductionTeams'
and c.LocationTree like c_ownership.LocationTree 

--*******************************************************************************************************************--
--update hasspotweld bit for ngac
--*******************************************************************************************************************--
update  NGAC.c_controller
 set hasspotweld = case
                  when x.controller_id is not null  then 1
                  else 0
                 end
from  NGAC.c_controller
left join (select distinct controller_id from  NGAC.h_TipWearBeforeChange) as x on x.controller_id = c_controller.id 
--*******************************************************************************************************************--


--*******************************************************************************************************************--
--Shit area !! 
--*******************************************************************************************************************--
--temp for AASPOT
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