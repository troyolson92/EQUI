



CREATE PROCEDURE [EqUi].[sp_LinkAssets]

AS
BEGIN
 print 'make temp table for robots'
 if (OBJECT_ID('tempdb..#Robots') is not null) drop table #Robots

 print 'adding NGAC robots'
	select distinct
	 'NGAC' as controller_type
	,'NGAC.c_controller' as 'table'
	,NGAC.id
	,NGAC.controller_name
	,null as 'location'
	,null as'ownership'
	,NULL as 'Plant'
	,NULL as 'Area'
	,NULL as 'SubArea'
	,Null as 'server'
	,NGAC.LocationTree as 'Locationtree'
	into #robots
	from NGAC.c_controller as NGAC
	where class_id <> 8 -- exclude S4C class
/*
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'NGAC' 
                 AND  TABLE_NAME = 'c_controller'))
BEGIN
  print 'adding S4C robots'
	insert INTO #Robots
	select
	 'S4C' as controller_type
	,'NGAC.c_controller' as 'table'
	,NGAC.id
	,NGAC.controller_name
	,null as 'location'
	,null as'ownership'
	,NULL as 'Plant'
	,NULL as 'Area'
	,NULL as 'SubArea'
	,Null as 'server'
	,NGAC.locationtree as 'locationtree'
	from NGAC.c_controller as NGAC
	where class_id = 8 -- s4C only
END

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'c4g' 
                 AND  TABLE_NAME = 'c_controller'))
BEGIN
 print 'adding c4g robots'
	insert INTO #Robots
	select distinct 
	 'c4g' as controller_type
	,'c4g.c_controller' as 'table'
	,c4g.id
	,c4g.controller_name
	,c4g.location
	,c4g.ownership
	,c4g.Plant
	,c4g.Area
	,C4g.SubArea
	,lop.Vcsc_name as 'server'
	,c4g.locationtree as 'locationtree'
	from c4g.c_controller as c4g
	left join C4G.L_operation as lop on lop.controller_id = c4g.id and lop.code = 3
	WHERE controller_name not like '%REC' --added this because elsde CBM controllers clones show in front end
END

 IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'c4g' 
                 AND  TABLE_NAME = 'c_controller'))
BEGIN
  print 'adding c3g robots'
	insert INTO #Robots
	select distinct 
	 'c3g' as controller_type
	,'c3g.c_controller' as 'table'
	,c3g.id
	,c3g.controller_name
	,c3g.location
	,c3g.ownership as 'ownership' 
	,c3g.Plant
	,c3g.Area
	,c3g.SubArea
	,lop.Vcsc_name as 'server'
	,c3g.locationtree as 'locationtree'
	from c3g.c_controller as c3g
	left join C3G.L_operation as lop on lop.controller_id = c3g.id and lop.code = 3
END
*/
--************************************************************************************************--

 print 'clear the table'
 DELETE EqUi.ASSETS FROM EqUi.ASSETS
 print 'run new transfer'
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
                  left join #Robots as r on 
                  r.controller_name = assets.LOCATION
                  AND
                  (assets.ASSETNUM like 'URC%' OR assets.ASSETNUM like 'URA%') --COMAU and ABB assets
--***********************************************ROBOT CONTROLLED ASSET JOIN BLOCK*******************************************-- 
                  --join robot controller assets with there controller
                  left join #Robots as ra on 
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
left join  EqUi.c_ownership on c_ownership.Optgroup = 'TechnicianTeams'
and c.LocationTree like c_ownership.LocationTree 

UPDATE  EqUi.ASSETS
set  ResponsibleProductionTeam = c_ownership.[Ownership]
from  EQUI.ASSETS as c
left join  EqUi.c_ownership on c_ownership.Optgroup = 'ProductionTeams'
and c.LocationTree like c_ownership.LocationTree 



end