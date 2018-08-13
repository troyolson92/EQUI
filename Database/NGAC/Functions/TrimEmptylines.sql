CREATE FUNCTION [NGAC].[TrimEmptylines](@str VARCHAR(MAX)) RETURNS VARCHAR(MAX)
AS
BEGIN
BEGIN
  SET @str =  replace(@str,'      ' , '') --Abb f up 1
  SET @str =  replace(@str,'   ', '') --stupid f up 2
  SET @str =  replace(@str, char(10) + char(13), '') --normal empty line

END

RETURN @str
END