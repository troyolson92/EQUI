﻿
CREATE PROCEDURE [NGAC].[sp_update_cleanLogteksts]
    @Update as bit = 0 -- 0 = only update records that currently have NULL (new records) | 1 = recalc ALL records
AS
BEGIN

--precalculate 'clean' discription text 
UPDATE NGAC.L_description
SET CleanDescription = NGAC.TrimX(NGAC.TrimEmptylines(L_description.[Description]))  
FROM NGAC.L_description 
WHERE 
--only new ones
(
CleanDescription is null
AND @Update = 0
)
OR
--recalc all
(
CleanDescription is null
AND @Update = 1
)

--precalculate 'clean' title text 
UPDATE NGAC.L_error
SET CleanTitle = NGAC.TrimX(NGAC.TrimEmptylines(L_error.Title))  
FROM NGAC.L_error 
WHERE 
--only new ones
(
CleanTitle is null
AND @Update = 0
)
OR
--recalc all
(
CleanTitle is null
AND @Update = 1
)


END