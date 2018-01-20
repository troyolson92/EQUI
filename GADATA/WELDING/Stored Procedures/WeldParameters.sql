-- =============================================
-- Author:		Coppejans Jens
-- Create date: 5/01/2018
-- Description:	Welding parameters
-- =============================================
CREATE PROCEDURE WELDING.WeldParameters

AS
BEGIN

	SET NOCOUNT ON;

---Update SpotID from SpotTable---




----update squeeze times---


---NPT27---


UPDATE WELDING.Weldingparameters


SET WELDING.Weldingparameters.[squeeze] = Squeeze.sqz

FROM OPENQUERY ([19.148.181.155\BOS_SQLSERV_2005],'

SELECT DISTINCT dbo.ExtSpotTable_V.spotName, dbo.ExtParamValues_V.value AS sqz
FROM            dbo.ExtSpotTable_V INNER JOIN
                         dbo.ExtParamValues_V ON dbo.ExtSpotTable_V.timerName = dbo.ExtParamValues_V.timerName AND 
                         dbo.ExtSpotTable_V.weldProgNo = dbo.ExtParamValues_V.subIndex
WHERE        (dbo.ExtParamValues_V.languageCode = 1) AND (dbo.ExtParamValues_V.param_ID = 1075) 

 ')

 AS squeeze inner JOIN
                         WELDING.Weldingparameters ON squeeze.spotname = WELDING.Weldingparameters.Spot

	
END