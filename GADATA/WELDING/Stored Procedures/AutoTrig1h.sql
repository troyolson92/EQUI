
-------------------------------------------------------------------------------
CREATE PROCEDURE [WELDING].[AutoTrig1h]
AS
BEGIN
exec GADATA.WELDING.Datachange
exec GADATA.WELDING.TimerFaults
exec GADATA.WELDING.SpatterDATA
exec GADATA.Welding.WeldingTimers

END