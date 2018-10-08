CREATE FUNCTION [NGAC].[AddifSomething](@str VARCHAR(MAX)) RETURNS VARCHAR(MAX)
AS
BEGIN
BEGIN
	if (len(ISNULL(@str,'')) > 1)
	BEGIN
		SET @str = ISNULL(@str,'') + char(10) + char(13)
	END
END
RETURN ISNULL(@str,'')
END