﻿CREATE FUNCTION [EqUi].[TrimX](@str VARCHAR(MAX)) RETURNS VARCHAR(MAX)
AS
BEGIN
RETURN equi.LTrimX(equi.RTrimX(@str))
END
GO
