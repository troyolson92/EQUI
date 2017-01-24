--workorders
select * 
from MAXIMO.WORKORDER WORKORDER  
where WORKORDER.WONUM = '8085851';

--workorder change history
select *
from MAXIMO.WOSTATUS WOSTATUS
where WOSTATUS.WONUM = '8085851';

--uitgeschreven stukken
select * 
from MAXIMO.WPITEM WPITEM
--where WPITEM.ITEMNUM LIKE '%6062870020%';
where WPITEM.WONUM = '8085851';

--lange wo tekst 
--HTML ! 
select * 
from MAXIMO.LONGDESCRIPTION LONGDESCRIPTION;

--stukken magazijn
select *
from MAXIMO.ITEM ITEM
where ITEM.ITEMNUM LIKE '%6062870020%';

--ik denk taakplan 
select * 
from MAXIMO.CXITEMREQ CXITEMREQ
WHERE CXITEMREQ.CXPLANGROUP LIKE '%AAOSR%';

--stock (location = welk magazijn)
select * 
from maximo.inventory inventory
where inventory.ITEMNUM like '%6062870020%';

--locatie hierary (assset three)
select * 
from maximo.lochierarchy lochierarchy
where lochierarchy.location like '%99070R01%';

--assets
select *
from MAXIMO.LOCATIONS locations
where locations.location like '%99070R01%';

--waar maken we boms ?

--gepland werk
select *
from MAXIMO.WPLABOR WPLABOR 
where WPLABOR.WONUM = '8563831';

--act werk 
select *
from MAXIMO.LABTRANS  LABTRANS 
where LABTRANS.REFWO  = '8563831';

