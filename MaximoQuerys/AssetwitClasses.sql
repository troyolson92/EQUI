     SELECT DISTINCT
                 l1.SYSTEMID
                ,l1.LOCATION LOCATION
                ,LOCATIONS.DESCRIPTION LocationDescription
                ,ASSET.ASSETNUM 
                ,ASSET.DESCRIPTION AssetDescription
                ,(
                 NVL2(l10.PARENT , l10.PARENT ||' -> ' ,'')||
                 NVL2(l9.PARENT , l9.PARENT ||' -> ','') ||
                 NVL2(l8.PARENT , l8.PARENT ||' -> ','') ||
                 NVL2(l7.PARENT , l7.PARENT ||' -> ','') ||
                 NVL2(l6.PARENT , l6.PARENT ||' -> ','') ||
                 NVL2(l5.PARENT , l5.PARENT ||' -> ','') ||
                 NVL2(l4.PARENT , l4.PARENT ||' -> ','') ||
                 NVL2(l3.PARENT , l3.PARENT ||' -> ','') ||
                 NVL2(l2.PARENT , l2.PARENT ||' -> ','') ||
                 NVL2(l1.PARENT , l1.PARENT ||' -> ','') ||
                 l1.LOCATION
                 ) LocationTree
                 
                , c1.DESCRIPTION CLASSDESCRIPTION
                , c1.CLASSSTRUCTUREID
                , c1.CLASSIFICATIONID
                ,(
                 NVL2(c10.CLASSIFICATIONID , c10.CLASSIFICATIONID ||' -> ' ,'')||
                 NVL2(c9.CLASSIFICATIONID , c9.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c8.CLASSIFICATIONID , c8.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c7.CLASSIFICATIONID , c7.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c6.CLASSIFICATIONID , c6.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c5.CLASSIFICATIONID , c5.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c4.CLASSIFICATIONID , c4.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c3.CLASSIFICATIONID , c3.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c2.CLASSIFICATIONID , c2.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c1.CLASSIFICATIONID , c1.CLASSIFICATIONID ||' -> ','') ||
                 c1.CLASSIFICATIONID
                 ) ClassificationTree
                 
                FROM MAXIMO.LOCHIERARCHY l1
                JOIN MAXIMO.LOCATIONS LOCATIONS on LOCATIONS.LOCATION = l1.LOCATION -- to get equipment status and details 
                JOIN MAXIMO.ASSET ASSET on ASSET.LOCATION = l1.LOCATION --to get asset number

                --join next 10 levels to build up structure
                LEFT JOIN MAXIMO.LOCHIERARCHY l2 on l2.LOCATION = l1.PARENT AND l2.SYSTEMID = L1.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l3 on l3.LOCATION = l2.PARENT AND l3.SYSTEMID = L2.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l4 on l4.LOCATION = l3.PARENT AND l4.SYSTEMID = L3.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l5 on l5.LOCATION = l4.PARENT AND l5.SYSTEMID = L4.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l6 on l6.LOCATION = l5.PARENT AND l6.SYSTEMID = L5.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l7 on l7.LOCATION = l6.PARENT AND l7.SYSTEMID = L6.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l8 on l8.LOCATION = l7.PARENT AND l8.SYSTEMID = L7.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l9 on l9.LOCATION = l8.PARENT AND l9.SYSTEMID = L8.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l10 on l10.LOCATION = l9.PARENT AND l10.SYSTEMID = L9.SYSTEMID
                
                --join 10 levels to get classification of asset
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c1 on c1.CLASSSTRUCTUREID = ASSET.CLASSSTRUCTUREID
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c2 on c2.CLASSSTRUCTUREID = c1.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c3 on c3.CLASSSTRUCTUREID = c2.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c4 on c4.CLASSSTRUCTUREID = c3.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c5 on c5.CLASSSTRUCTUREID = c4.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c6 on c6.CLASSSTRUCTUREID = c5.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c7 on c7.CLASSSTRUCTUREID = c6.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c8 on c8.CLASSSTRUCTUREID = c7.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c9 on c9.CLASSSTRUCTUREID = c8.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c10 on c10.CLASSSTRUCTUREID = c9.PARENT    
                
                WHERE 
                l1.SITEID = 'VCG'
                AND
                l1.SYSTEMID = 'PRODMID';
          
                
                
                
   
