define _assets = 'UR%';
define _locations ='%';
define _StartDate = '2017-01-01 00:00:00';
define _EndDate = '2017-01-30 00:00:00';
define _ChangedAfter = '2017-01-13 00:00:00';
define _Owngergroup = '%';
define _Reportedby = '%';
define _Wonum = '%';
define _WoType = 'CD%';

--WO DETAILS
select 
 to_number(WORKORDER.WONUM) WONUM
,WORKORDER.LOCATION
,WORKORDER.STATUS ActStatus
,WORKORDER.DESCRIPTION
,WORKORDER.SCHEDSTART
,WORKORDER.ESTDUR 
,(WOSTATUSwappr.CHANGEDATE || ' U:' || WOSTATUSwappr.CHANGEBY) Swppr
,(WOSTATUSinprg.CHANGEDATE || ' U:' || WOSTATUSinprg.CHANGEBY) Sinprg
,(WOSTATUScomp.CHANGEDATE || ' U:' || WOSTATUScomp.CHANGEBY) Scomp

,(WPLABOR1a.LABORCODE || ' h:' ||WPLABOR1a.LABORHRS) Planned1st
,(LABTRANS1a.LABORCODE || ' h:' || LABTRANS1a.REGULARHRS)  Actual1st

,WORKORDER.STATUSDATE lastChanged
,WORKORDER.OWNERGROUP
,WORKORDER.ASSETNUM
,WORKORDER.ASSIGNEDOWNERGROUP
,WORKORDER.CXMATERIALBOXID
,WORKORDER.WORKTYPE
from MAXIMO.WORKORDER WORKORDER 

left join MAXIMO.WPLABOR WPLABOR1a on (WPLABOR1a.WONUM = WORKORDER.WONUM)
left outer join MAXIMO.WPLABOR WPLABOR1b on (WPLABOR1b.WONUM = WORKORDER.WONUM) and (WPLABOR1a.WPLABORID > WPLABOR1b.WPLABORID ) 

left join MAXIMO.LABTRANS LABTRANS1a on (LABTRANS1a.REFWO = WORKORDER.WONUM)
left outer join MAXIMO.LABTRANS LABTRANS1b on (LABTRANS1b.REFWO = WORKORDER.WONUM) and (LABTRANS1a.LABTRANSID > LABTRANS1b.LABTRANSID )  

left join MAXIMO.WOSTATUS WOSTATUSwappr on (WOSTATUSwappr.WONUM = WORKORDER.WONUM) AND (WOSTATUSwappr.STATUS = 'WAPPR')
left join MAXIMO.WOSTATUS WOSTATUSinprg on (WOSTATUSinprg.WONUM = WORKORDER.WONUM) AND (WOSTATUSinprg.STATUS = 'INPRG')
left join MAXIMO.WOSTATUS WOSTATUScomp on (WOSTATUScomp.WONUM = WORKORDER.WONUM) AND (WOSTATUScomp.STATUS = 'COMP')
where 
 WORKORDER.ASSETNUM LIKE '&_assets'
 AND
 WORKORDER.LOCATION LIKE '&_locations'
 AND
 WORKORDER.REPORTDATE > TO_TIMESTAMP('&_StartDate', 'yyyy/mm/dd hh24:mi:ss')
 AND
 WORKORDER.STATUSDATE < TO_TIMESTAMP('&_EndDate', 'yyyy/mm/dd hh24:mi:ss')
 AND
 WORKORDER.CHANGEDATE > TO_TIMESTAMP('&_ChangedAfter', 'yyyy/mm/dd hh24:mi:ss')
 AND
 WORKORDER.OWNERGROUP LIKE '&_Owngergroup'
 AND
 WORKORDER.REPORTEDBY LIKE '&_Reportedby'
 AND
 WORKORDER.WONUM LIKE  '&_Wonum' 
 AND 
 WORKORDER.WORKTYPE LIKE '&_WoType'
 AND 
 (LABTRANS1b.LABTRANSID IS NULL)
 AND 
 (WPLABOR1b.WPLABORID IS NULL)
 
 ORDER BY WORKORDER.STATUSDATE;