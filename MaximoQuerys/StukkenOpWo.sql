define _assets = 'UR%';
define _locations ='99%';
define _StartDate = '2015-01-01 00:00:00';
define _EndDate = '2018-01-01 00:00:00';
define _StatusDate = '1900-01-01 00:00:00';
define _Owngergroup = '%';
define _Reportedby = '%';
define _Wonum = '%';
define _Itemnum = '%';

--uitgeschreven stukken join WO NuM 
select 
 WORKORDER.WONUM
,WORKORDER.DESCRIPTION
,WORKORDER.ASSETNUM
,WORKORDER.LOCATION
,WORKORDER.REPORTDATE
,WPITEM.ITEMNUM
,WPITEM.DESCRIPTION PARTDESCRIPTION
,WPITEM.ITEMQTY
,WPITEM.LOCATION MagLocation
,WPITEM.LINECOST
,WPITEM.REQUESTBY
from MAXIMO.WORKORDER WORKORDER  
JOIN MAXIMO.WPITEM WPITEM on WPITEM.WONUM = WORKORDER.WONUM
where 
 WORKORDER.ASSETNUM LIKE '&_assets'
 AND
 WORKORDER.LOCATION LIKE '&_locations'
 AND
 WPITEM.ITEMNUM LIKE '&_Itemnum'
 AND
 WORKORDER.REPORTDATE BETWEEN TO_TIMESTAMP('&_StartDate', 'yyyy/mm/dd hh24:mi:ss') AND TO_TIMESTAMP('&_EndDate', 'yyyy/mm/dd hh24:mi:ss')
 AND
 WORKORDER.STATUSDATE > TO_TIMESTAMP('&_StatusDate', 'yyyy/mm/dd hh24:mi:ss')
 AND
 WORKORDER.OWNERGROUP LIKE '&_Owngergroup'
 AND
 WORKORDER.REPORTEDBY LIKE '&_Reportedby'
-- AND
-- _Wonum 
 ORDER BY WORKORDER.STATUSDATE;