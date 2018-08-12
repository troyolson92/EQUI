






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
--calc production responsible and technican responsible for c_controllers
--*******************************************************************************************************************--
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



end