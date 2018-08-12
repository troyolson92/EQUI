






CREATE PROCEDURE [EqUi].[sp_LinkAssets]

AS
BEGIN
--clear the table
 DELETE EqUi.ASSETS FROM EqUi.ASSETS
 
 --run new transfer
                INSERT INTO EqUi.ASSETS
                SELECT [SYSTEMID]
                      ,assets.[LOCATION]
                      ,assets.[ASSETNUM]
                      ,assets.[AssetDescription]
                      ,assets.[LocationTree]
                      ,assets.[ClassDescription]
                      ,assets.[ClassStructureId]
                      ,assets.[CLassificationId]
                      ,assets.[ClassificationTree]
                      ,assets.[Station]
                      ,assets.[Area]
                      ,assets.[Team]
                      ,ISNULL(r.controller_name,ra.controller_name) as 'controller_name'
                      ,ISNULL(r.controller_type,ra.controller_type) as 'controller_type'
                      ,ISNULL(r.id,ra.id) as 'controller_id'
                      ,ROW_NUMBER() OVER (PARTITION BY 
                          ISNULL(r.controller_type,ra.controller_type)
                        , ISNULL(r.id,ra.id), assets.classificationid 
                        ORDER BY assets.location ASC) AS 'controller_ToolID'
					  ,null as 'ResponsibleTechnicianTeam'
					  ,null as 'ResponsibleProductionTeam'
                  FROM [Equi].[ASSETS_fromMX7] as assets
--***********************************************ROBOT CONTROLLER JOIN BLOCK***********************************************--
                  --join robot assets with there controller
                  left join volvo.Robots as r on 
                  r.controller_name = assets.LOCATION
                  AND
                  (assets.ASSETNUM like 'URC%' OR assets.ASSETNUM like 'URA%') --COMAU and ABB assets
--***********************************************ROBOT CONTROLLED ASSET JOIN BLOCK*******************************************-- 
                  --join robot controller assets with there controller
                  left join VOLVO.Robots as ra on 
                  --Grippers
                  (
                  REPLACE(REPLACE(REPLACE(assets.LOCATION,'GH','R'),'GP','R'),'GD','R') LIKE ra.controller_name+'%'
                  )
                  OR
                  --Weld WS (func pack spot) WN (nutweld)
                  (
                  REPLACE(REPLACE(REPLACE(assets.LOCATION,'WS','R'),'WT','R'),'WN','R') LIKE ra.controller_name+'%'
                  )
                    OR
                  -- WT (tucker) Pistool, Toevoer, Lasbron, algemene zaken
                  (
                  REPLACE(REPLACE(REPLACE(REPLACE(assets.LOCATION,'WTP','R'),'WTT','R'),'WTL','R'),'WTA','R') LIKE ra.controller_name+'%'
                  )
                  OR
                  --Dispense (and quis)
                  (
                  REPLACE(REPLACE(assets.LOCATION,'SH','R'),'QF','R') LIKE ra.controller_name+'%'
                  )
                  OR
                  --Nutrunners
                  (
                  REPLACE(assets.LOCATION,'JB','R') LIKE ra.controller_name+'%'
                  )
--**********************************************only join for GA GB********************************************************--
                where 
			    (assets.LocationTree like 'VCG -> A%')
                OR
                (assets.LocationTree like 'VCG -> B%' AND assets.ASSETNUM like 'U%')
             


--*******************************************************************************************************************--
--new way Direct joining the location root in the c_controllers
--*******************************************************************************************************************--
--NGAC
UPDATE NGAC.c_controller
SET c_controller.LocationTree = mx.LocationTree 
   ,c_controller.Assetnum = mx.ASSETNUM
   ,c_controller.ProductionTeam = mx.Team
   ,c_controller.ClassificationTree = mx.ClassificationTree
   ,c_controller.CLassificationId = mx.CLassificationId
FROM NGAC.c_controller as C 
LEFT JOIN EqUi.ASSETS_fromMX7 as mx on mx.LOCATION = C.controller_name AND mx.SYSTEMID = 'PRODMID'

--C3G
UPDATE C3G.c_controller
SET c_controller.LocationTree = mx.LocationTree 
   ,c_controller.Assetnum = mx.ASSETNUM
   ,c_controller.ProductionTeam = mx.Team
  -- ,c_controller.ClassificationTree = mx.ClassificationTree
  -- ,c_controller.CLassificationId = mx.CLassificationId
FROM C3G.c_controller as C 
LEFT JOIN EqUi.ASSETS_fromMX7 as mx on mx.LOCATION = C.controller_name AND mx.SYSTEMID = 'PRODMID'

--C4G
UPDATE C4G.c_controller
SET c_controller.LocationTree = mx.LocationTree 
   ,c_controller.Assetnum = mx.ASSETNUM
   ,c_controller.ProductionTeam = mx.Team
  -- ,c_controller.ClassificationTree = mx.ClassificationTree
  -- ,c_controller.CLassificationId = mx.CLassificationId
FROM C4G.c_controller as C 
LEFT JOIN EqUi.ASSETS_fromMX7 as mx on mx.LOCATION = C.controller_name AND mx.SYSTEMID = 'PRODMID'

--STO
--run 1 direct match 
UPDATE  STO.c_controller
SET c_controller.[LOCATION] = mx.[LOCATION]
   ,c_controller.LocationTree = mx.LocationTree 
   ,c_controller.Assetnum = mx.ASSETNUM
   ,c_controller.ProductionTeam = mx.Team
   ,c_controller.ClassificationTree = mx.ClassificationTree
   ,c_controller.CLassificationId = mx.CLassificationId
FROM STO.c_controller as C 
LEFT JOIN EqUi.ASSETS as mx on 
mx.SYSTEMID = 'PRODMID'
and c.ALARMOBJECT  not like '%ZM%' and c.ALARMOBJECT  not like '%Mode%' --not of this type 
and c.ALARMOBJECT like mx.[LOCATION] + '%'

--run 2 for ZM and Mode where still null
UPDATE STO.c_controller
SET c_controller.[LOCATION] = mx.[LOCATION]
   ,c_controller.LocationTree = mx.LocationTree 
   ,c_controller.Assetnum = mx.ASSETNUM
   ,c_controller.ProductionTeam = mx.Team
   ,c_controller.ClassificationTree = mx.ClassificationTree
   ,c_controller.CLassificationId = mx.CLassificationId
FROM STO.c_controller as C 
LEFT JOIN EqUi.ASSETS as mx on 
mx.SYSTEMID = 'PRODMID'
and (c.ALARMOBJECT  like '%ZM%' or c.ALARMOBJECT  like '%Mode%') --if the alarm object from type
and mx.[LOCATION] like '%STN%' --join only Station assets
and mx.[LOCATION] like '%' + SUBSTRING(c.SUBZONENAME,0,CHARINDEX('ZMS',  c.SUBZONENAME)) + '%' --make to match
where c.[location] is null

--*******************************************************************************************************************--


--*******************************************************************************************************************--
--calc production responsible and technican responsible for c_controllers
--*******************************************************************************************************************--
--NGAC
UPDATE NGAC.c_controller
set ResponsibleTechnicianTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'TechnicianTeams'
and c.LocationTree like c_ownership.LocationTree 

UPDATE  NGAC.c_controller
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'ProductionTeams'
and c.LocationTree like c_ownership.LocationTree 

--C3G
UPDATE  C3G.c_controller
set ResponsibleTechnicianTeam = c_ownership.[Ownership]
from  C3G.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'TechnicianTeams'
and c.LocationTree like c_ownership.LocationTree 

UPDATE  C3G.c_controller
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  C3G.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'ProductionTeams'
and c.LocationTree like c_ownership.LocationTree 

--C4G
UPDATE  C4G.c_controller
set ResponsibleTechnicianTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'TechnicianTeams'
and c.LocationTree like c_ownership.LocationTree 

UPDATE  C4G.c_controller
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  NGAC.c_controller as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'ProductionTeams'
and c.LocationTree like c_ownership.LocationTree 

--Asset table
UPDATE  EqUi.ASSETS
set ResponsibleTechnicianTeam = c_ownership.[Ownership]
from  EQUI.ASSETS as c
left join  EqUi.c_ownership on c_ownership.optgroup = 'TechnicianTeams'
and c.LocationTree like c_ownership.LocationTree 

UPDATE  EqUi.ASSETS
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  EQUI.ASSETS as c
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
  FROM [GADATA].[NGAC].[c_controller]
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