﻿CREATE FUNCTION [NGAC].[AddifSomething](@str VARCHAR(MAX)) RETURNS VARCHAR(MAX)
AS
BEGIN
BEGIN
	if (len(ISNULL(@STR,'')) > 1)
	BEGIN
		SET @STR = ISNULL(@STR,'') + char(10) + char(13)
	END
END
RETURN ISNULL(@str,'')
END