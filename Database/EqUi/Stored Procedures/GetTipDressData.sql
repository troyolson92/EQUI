

CREATE PROCEDURE [EqUi].[GetTipDressData]
--timeparameters
	   @StartDate as DATETIME = null,
	   @EndDate as DATETIME = null,
	   @daysBack as int = null,
	   @locations as varchar(20) = '%',
       @Tool as varchar(25) = '%',
	   @Tipchangedatamode as bit = 0
AS
BEGIN

---------------------------------------------------------------------------------------
--set first day of the week to monday (german std)
---------------------------------------------------------------------------------------
SET DATEFIRST 1
---------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------
--Set default values of start and end date
---------------------------------------------------------------------------------------
if ((@StartDate is null) OR (@StartDate = '1900-01-01 00:00:00:000'))
BEGIN
SET @StartDate = GETDATE()-'1900-01-01 12:00:00'
END

if ((@EndDate is null) OR (@EndDate = '1900-01-01 00:00:00:000'))
BEGIN
SET @EndDate = GETDATE()
END
--for days back mode
if (@daysBack is not null)
BEGIN
SET @StartDate = GETDATE() - @daysBack
SET @EndDate = GETDATE()
END 
---------------------------------------------------------------------------------------

--mode to get all tipdressdata 
if (@Tipchangedatamode = 0)
BEGIN

SELECT *
  FROM [NGAC].[TipDressLogFile] with (nolock)
  where controller_name like @locations and Tool_Nr like '%'+@Tool+'%'
  and [Date Time] between @startdate and @enddate
 
END

--mode to get all tipchange data 
if (@Tipchangedatamode = 1)
BEGIN

SELECT *
  FROM [NGAC].TipwearBeforeChange with (nolock)
  where controller_name like @locations and Tool_Nr like '%'+@Tool+'%'
  and TipchangeTimestamp between @startdate and @enddate
 
END


END