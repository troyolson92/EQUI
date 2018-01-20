
-- =============================================
-- Author:		<Coppejans,,jens>
-- Create date: <7/03/2017,,>
-- Description:	<OptimalisationTool,,>
-- =============================================
CREATE PROCEDURE [dbo].[WeldingTool] 

  
    @spot as varchar(25) ,
	@Timer as varchar(25)
	
AS

BEGIN
	
	SET NOCOUNT ON;
--- data from weldmeasurements---
SELECT           [Date], [Name] AS Timer, Number As Spotnr, dbo.spot.Program,Null As TimerFault,Null As ProductionLoss,
	
	                 Zone,
	                 CONVERT(DECIMAL(5, 1), 100 * CAST(dbo.WeldMeasurements.NbrSplash AS decimal(10, 5))/ CAST(dbo.WeldMeasurements.NbrWeld AS decimal(10, 5))) AS [Spatters%], CONVERT(DECIMAL(5, 1), 100 * CAST(dbo.WeldMeasurements.NbrReweld AS decimal(10,  5)) / CAST(dbo.WeldMeasurements.NbrWeld AS decimal(10, 5))) AS PsfReweld, [NbrWeld], [AvgEnergy], [AvgPSF],
					 [StdPSF] As [%AfwijkingPSF], [AvgWeldTIme], NULL AS UltralogTime, NULL AS [Losse spot?], NULL AS [OK spot?], 
					 NULL AS [SmallNugget?], NULL AS [StickWeld?], NULL AS [BadTroughWeld?], NULL  AS [MeasuredThickness], NULL AS [TotalThickness],
					 NULL AS [InspectorComment], NULL AS bodynummer, NULL AS ParameterChange, NULL AS before, NULL AS after,
					 NULL AS CDSID, NULL AS NPT,

			               NULL AS [Pressure], NULL Squeeze, NULL AS [KA_1], NULL AS [WELD1], NULL AS [COOL_1],
		                   NULL AS [KA_2], NULL AS [WELD2], NULL AS [COOL_2], NULL AS [KA_3], NULL AS [WELD3],
		                   NULL AS [HOLD], NULL AS [PRprofile ON ?], NULL AS [thickness1], NULL AS [Material1], NULL AS [Coating1],
		                   NULL AS [Thickness2], NULL AS [Material2], NULL AS [Coating2], NULL AS [thickness3], NULL AS [Material3],
		                   NULL AS [Coating3], NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]

FROM            [GADATA].[dbo].[WeldMeasurements] INNER JOIN
                         dbo.Spot ON dbo.WeldMeasurements.SpotId = dbo.spot.id INNER JOIN
                         [GADATA].[dbo].[Timer] ON [GADATA].[dbo].[Spot].TimerID = [GADATA].[dbo].[Timer].ID
WHERE        [Date] >= GETDATE() - 365   AND Number = @spot AND Name = @Timer

UNION
--- data from ultralog---
SELECT        dbo.UltralogInspections.InspectionTime, dbo.Timer.Name AS timer, dbo.Spot.Number, dbo.Spot.Program, NULL AS TimerFault, NULL AS ProductionLoss, NULL 
                         AS Loose, NULL AS spot, NULL AS Expr1, NULL AS Expr2, NULL AS Expr3, NULL AS Expr4, NULL AS Expr5, NULL AS Expr6, 
                         dbo.UltralogInspections.InspectionTime AS Expr7, dbo.UltralogInspections.Loose AS Expr8, dbo.UltralogInspections.OK, dbo.UltralogInspections.SmallNugget, 
                         dbo.UltralogInspections.StickWeld, dbo.UltralogInspections.BadTroughWeld, dbo.UltralogInspections.MeasuredThickness, dbo.UltralogInspections.TotalThickness, 
                         dbo.UltralogInspections.InspectorComment, dbo.UltralogInspections.BodyNbr, NULL AS Expr9, NULL AS Expr10, NULL AS ParameterChange, NULL AS before, NULL 
                         AS NPT, NULL AS Expr11, NULL AS Expr12, NULL AS Expr13, NULL AS Expr14, NULL AS Expr15, NULL AS Expr16, NULL AS Expr17, NULL AS Expr18, NULL 
                         AS Expr19, NULL AS Expr20, NULL AS Expr21, NULL AS Expr22, NULL AS Expr23, NULL AS Expr24, NULL AS Expr25, NULL AS Expr26, NULL AS Expr27, NULL 
                         AS Expr28, NULL AS Expr29, NULL AS Expr30, NULL AS Expr31, NULL AS thickness4, NULL AS Material4, NULL AS Coating4
FROM            dbo.UltralogInspections INNER JOIN
                         dbo.Spot ON dbo.UltralogInspections.SpotID = dbo.Spot.ID INNER JOIN
                         dbo.Timer ON dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID
WHERE        [InspectionTime] >= GETDATE() - 365   AND Number = @spot And dbo.Timer.Name = @Timer


UNION
--- data from dbo.datachange----data from schema Welding not possible => different data types used ---

SELECT        dbo.SpotDataChange.DateTime, dbo.Timer.Name, dbo.Spot.Number, dbo.Spot.Program, NULL AS Expr1, NULL AS spot, 

                         dbo.Spot.Zone,
						 NULL AS spot1, NULL AS spot2, NULL AS spot3, NULL AS spot4, NULL AS spot5, 
						 NULL AS spot7, NULL AS spot8, NULL AS spot9, NULL AS spot10, NULL AS spot11, 
						 NULL AS spot13, NULL AS spot14, NULL AS spot15, NULL AS spot16, NULL AS spot17, 
						 NULL AS spot19, null as spot100, dbo.TimerParameterName.Description AS parmameter, dbo.SpotDataChange.OldValue, dbo.SpotDataChange.NewValue,
						 dbo.Users.CDSID, dbo.NPT.Name AS NPT,
						 
						   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]

FROM            dbo.Spot INNER JOIN
                         dbo.Timer ON dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID AND dbo.Spot.TimerID = dbo.Timer.ID INNER JOIN
                         dbo.SpotDataChange ON dbo.Spot.ID = dbo.SpotDataChange.SpotID INNER JOIN
                         dbo.TimerParameterName ON dbo.SpotDataChange.ParameterID = dbo.TimerParameterName.ID INNER JOIN
                         dbo.Users ON dbo.SpotDataChange.UserID = dbo.Users.ID INNER JOIN
                         dbo.NPT ON dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID AND dbo.Timer.NptId = dbo.NPT.ID
WHERE        (dbo.SpotDataChange.DateTime >= GETDATE() - 365)  AND dbo.Spot.Number = @spot AND dbo.Timer.Name = @Timer

UNION
--- data from datachange on timer level --only timer data---

SELECT        dbo.TimerDataChange.DateTime AS Date, dbo.Timer.Name AS Timer, @spot, null as spot,Null As TimerFault,Null As ProductionLoss,

                         
						  null as spot,
						  NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, 
                          NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, 
						  NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
                          NULL AS Spot, NULL AS Spot, dbo.TimerParameterName.Description, dbo.TimerDataChange.OldValue, dbo.TimerDataChange.NewValue ,
						  dbo.Users.CDSID,dbo.NPT.Name AS NPT,

						 
						   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]

FROM            dbo.TimerParameterName INNER JOIN
                         dbo.TimerDataChange ON dbo.TimerParameterName.ID = dbo.TimerDataChange.ParameterID INNER JOIN
                         dbo.Users ON dbo.TimerDataChange.UserID = dbo.Users.ID INNER JOIN
                         dbo.Timer ON dbo.TimerDataChange.TimerID = dbo.Timer.ID INNER JOIN
                         dbo.NPT ON dbo.Timer.NptId = dbo.NPT.ID


WHERE        datetime >= GETDATE() - 365 AND dbo.Timer.Name= @Timer

UNION
--- plate and parameter combination---

SELECT        [Date],[Timer],Number,[Program] ,Null As TimerFault,Null As ProductionLoss,
      
	   NULL AS Spot, 
	    NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		 NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot,
		  NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		   NULL AS Spot, NULL AS Spot
	 
     
                       
		                ,[Pressure],[Squeeze],[KA_1],[WELD1],[COOL_1]
						,[KA_2],[WELD2],[COOL_2],[KA_3],[WELD3]
						,[HOLD] ,[PRprofile ON ?] ,[thickness1],[Material1],[Coating1]
						,[Thickness2],[Material2],[Coating2],[thickness3],[Material3]
						,[Coating3],[thickness4] ,[Material4] ,[coating4]
						
      
     FROM [GADATA].[LAUNCH].[JoiningParameterPlatesGA]   
    WHERE    Number = @spot AND Timer = @Timer
	

	UNION
--- Show all TimerFaults ----

SELECT        Datetime , Timer, spot , program AS Expr44, TimerFault, NULL AS ProductionLoss,null as  WeldmasterComment, NULL AS spot, NULL 
                         AS Loose, NULL AS Expr1, NULL AS Expr2, NULL AS Expr3, NULL AS Expr4, NULL AS Expr5, NULL AS Expr6, NULL AS Expr9, NULL AS Expr10, NULL 
                         AS Expr12, NULL AS Expr13, NULL AS Expr14, NULL AS Expr15, NULL AS Expr16, NULL AS Expr17, NULL AS Expr18, NULL AS Expr19, NULL AS Expr20, NULL 
                         AS Expr21, NULL AS Expr22, NULL AS Expr23, NULL AS Expr24, NULL AS Expr25, NULL AS Expr26, NULL AS Expr27, NULL AS Expr28, NULL AS Expr29, NULL 
                         AS Expr30, NULL AS Expr31, NULL AS thickness4, NULL AS Material4, NULL AS Coating4, NULL AS Expr7, NULL AS Expr8, NULL AS Expr32, NULL AS Expr33, NULL 
                         AS Expr34, NULL AS Expr35, NULL AS Expr11, NULL AS Expr36, NULL AS Expr37, NULL AS Expr38, NULL AS Expr39, NULL AS Expr40, NULL AS Expr41
FROM            WELDING.Weldfaults
WHERE    datetime >= GETDATE() - 365 AND spot = @spot AND Timer = @Timer 

	  UNION

--- TDT ---

SELECT 
[TDT build MTO 19 april 2017],[timer],[Number],[Program], 
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
[TDT build MTO 19 april 2017],
NULL AS Spot,NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot,[nom diameter],[diameter MTO],null as TDTMTOComment,NULL AS Spot,
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,Name As NPT,
NULL AS Spot,NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as spot,null as spot,null as spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]
		                 

      
  FROM [GADATA].[LAUNCH].[TDT_MTO_V316_FLOOR]
  WHERE  GADATA.LAUNCH.TDT_MTO_V316_FLOOR.[TDT build MTO 19 april 2017] >= GETDATE() - 600  AND  Number = @spot And timer = @timer

  	  UNION

SELECT 
[TDT BUILD TT 26 mei 2017],[timer],[spot],null as[Program], 
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
[TDT BUILD TT 26 mei 2017],
NULL AS Spot,NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot,[nom diameter],[diameter TT],null as TDTMTOComment,NULL AS Spot,
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,Name As NPT,
NULL AS Spot,NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as spot,null as spot,null as spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]
		                 

      
  FROM [GADATA].[LAUNCH].[TDT_TTV316_FLOOR]
  WHERE  [GADATA].[LAUNCH].[TDT_TTV316_FLOOR].[TDT BUILD TT 26 mei 2017] >= GETDATE() - 600  AND  Spot = @spot And timer = @timer

  	  UNION

SELECT 
[TDT PP BUILD 30 augustus 2017],[timer],[Number],null as [Program], 
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
[TDT PP BUILD 30 augustus 2017],
NULL AS Spot,NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot,[nom diameter],[diameter PP],null as TDTMTOComment,NULL AS Spot,
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,Name As NPT,
NULL AS Spot,NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as spot,null as spot,null as spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]
		                 

      
  FROM [GADATA].[LAUNCH].[TDT_PPV316_FLOOR]
  WHERE  [GADATA].[LAUNCH].[TDT_PPV316_FLOOR].[TDT PP BUILD 30 augustus 2017] >= GETDATE() - 600  AND  Number = @spot And timer = @timer

    UNION

  SELECT 
[TDTSIBOV316MTO].[date],[LAUNCH].[TDTSIBOV316MTO].[Name],[Spot],[Program], 
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
[TDTSIBOV316MTO].[date],
NULL AS Spot,NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot,[nom_dia],[diameter],null as TDTMTOComment,NULL AS Spot,
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as Name ,
NULL AS Spot,NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as spot,null as spot,null as spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]
		                 

      
  FROM [GADATA].[LAUNCH].[TDTSIBOV316MTO]
  WHERE  [GADATA].[LAUNCH].[TDTSIBOV316MTO].Date >= GETDATE() - 600  AND [TDTSIBOV316MTO]. spot = @spot And [TDTSIBOV316MTO].Name = @timer

    UNION

  SELECT 
[LAUNCH].[TDTSIBOTTV316].[date],[Name], [Spot],[LAUNCH].[TDTSIBOTTV316].[Program], 
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
[LAUNCH].[TDTSIBOTTV316].[date],
NULL AS Spot,NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot,[nom_dia],[diameter],null as TDTMTOComment,NULL AS Spot,
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as Name ,
NULL AS Spot,NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as spot,null as spot,null as spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]
		                 

      
  FROM [LAUNCH].[TDTSIBOTTV316]
  WHERE  [LAUNCH].[TDTSIBOTTV316].Date >= GETDATE() - 600  AND [LAUNCH].[TDTSIBOTTV316].spot = @spot And [LAUNCH].[TDTSIBOTTV316].Name = @timer

      UNION

  SELECT 
[LAUNCH].[TDTSIBOPPV316].[date],[Name], [Spot],[LAUNCH].[TDTSIBOPPV316].[Program], 
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
[LAUNCH].[TDTSIBOPPV316].[date],
NULL AS Spot,NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot,[nom_dia],[diameter],null as TDTMTOComment,NULL AS Spot,
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as Name ,
NULL AS Spot,NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as spot,null as spot,null as spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]
		                 

      
  FROM[LAUNCH].[TDTSIBOPPV316]
  WHERE [LAUNCH].[TDTSIBOPPV316].Date >= GETDATE() - 600  AND [LAUNCH].[TDTSIBOPPV316].spot = @spot And [LAUNCH].[TDTSIBOPPV316].Name = @timer

  UNION
  ----SIBO V316 W49D3
  SELECT 
LAUNCH.TDTSIBOV316W49D3.date, dbo.Timer.Name, dbo.Spot.Number, dbo.Spot.Program, 
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
LAUNCH.TDTSIBOV316W49D3.date,
NULL AS Spot,NULL AS Spot, NULL AS Spot,NULL AS Spot, NULL AS Spot,LAUNCH.TDTSIBOV316W49D3.demand, LAUNCH.TDTSIBOV316W49D3.measure,null as TDTMTOComment,NULL AS Spot,
NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as Name ,
NULL AS Spot,NULL AS Spot,NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,
		                   NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot, NULL AS Spot,null as spot,null as spot,null as spot, NULL AS [thickness4], NULL AS [Material4],
		                   NULL AS [Coating4]
		                 

      
  FROM            dbo.Spot INNER JOIN
                         dbo.Timer ON dbo.Spot.TimerID = dbo.Timer.ID INNER JOIN
                         LAUNCH.TDTSIBOV316W49D3 ON dbo.Spot.AlternativeNumber = LAUNCH.TDTSIBOV316W49D3.spot
  WHERE LAUNCH.TDTSIBOV316W49D3.Date >= GETDATE() - 600  AND dbo.Spot.Number = @spot And dbo.Timer.Name = @timer






                        



      
   END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[WeldingTool] TO [AASPOT_a]
    AS [dbo];

