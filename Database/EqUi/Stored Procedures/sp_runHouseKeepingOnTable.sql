




CREATE PROCEDURE [EqUi].[sp_runHouseKeepingOnTable]
       @nDaysKeepHistory as int = 5,
	   @nDeleteBatchSize as int = 1000,
	   @SchemaName as nvarchar(50) = 'WELDING2',
	   @TableName as nvarchar(50) = 'rt_weldmeasureprotddw',
	   @IdColName as nvarchar(50) = 'ID',
	   @DateTimeColName as nvarchar(50) = '_timestamp',
	   @NoHousekeeping as bit = 0

--gives the right permission when a low level user only has execution rights
with execute as owner
AS
BEGIN
--
DECLARE @maxTimestamp as datetime
set @maxTimestamp = GETDATE()-@nDaysKeepHistory
--******************************************************************--
print'delete all Xtable older than x days. @maxTimestamp=' +  CONVERT(char(19),@maxTimestamp, 120) + ' |'  + CONVERT(char(19),getdate(), 120) 
--******************************************************************--
DECLARE @SampleTable TABLE(Value nvarchar(max));
DECLARE @DeleteStatement nvarchar(max) = '';  
--
DECLARE @BatchStatement as nvarchar(max);
SET @BatchStatement = '';
--
DECLARE @loopCount as int = 0;
DECLARE @LastCount as int = 0;
DECLARE @StartCount as int = 0;
DECLARE @EndCount as int = 0;
--
set nocount on
--
SET @StartCount = (SELECT SUM(row_count) FROM sys.dm_db_partition_stats WHERE object_name(object_id) = @TableName  AND OBJECT_SCHEMA_NAME(object_id) = @SchemaName AND (index_id < 2))
print 'Startcount: ' + CAST(@startcount as varchar(max)) + ' |' + CONVERT(char(19),getdate(), 120) 

if @NoHousekeeping = 1
BEGIN
  print '@NoHousekeeping is set only tablesize check'
END

if @NoHousekeeping = 0
BEGIN
	WHILE 1 = 1
	BEGIN  
		--
		SEt @BatchStatement = 'SELECT TOP (' + CAST(@nDeleteBatchSize as varchar(max)) + ') CAST('+@IdColName + ' AS VARCHAR(50)) + char(44) FROM [' + @SchemaName + '].[' + @TableName + 
		'] WHERE ' +   @DateTimeColName +  ' < ' + char(39) + CONVERT(char(19),@maxTimestamp, 120) + char(39)
		--
		--print @BatchStatement
		--break
		--
		DELETE @SampleTable FROM @SampleTable
		INSERT INTO @SampleTable (Value)
		EXEC sp_executesql @BatchStatement
		--
		--select * from @SampleTable
		--
		SET @LastCount = @@rowcount
		--
		SET @DeleteStatement = ''
		SELECT @DeleteStatement = @DeleteStatement + Value FROM @SampleTable;
		SET @DeleteStatement = 'DELETE FROM  [' + @SchemaName + '].[' + @TableName +  + '] WHERE ' + @IdColName + ' in('+ @DeleteStatement + '0)'
		--
		--print @DeleteStatement
		--break
		--
		IF @LastCount = 0
		BEGIN
			print 'EXIT loopcount: ' + Cast(@loopcount as varchar(max)) + ' batch: ' + Cast(@nDeleteBatchSize as varchar(max)) + ' lastcount: ' + Cast(@LastCount as varchar(max)) + ' |' + CONVERT(char(19),getdate(), 120) 
			break
		END
		--
		EXEC sp_executesql  @DeleteStatement
		set @loopCount = @loopCount+1
		print 'loopcount: ' + Cast(@loopcount as varchar(max)) + ' batch: ' + Cast(@nDeleteBatchSize as varchar(max)) + ' lastcount: ' + Cast(@LastCount as varchar(max)) + ' |' + CONVERT(char(19),getdate(), 120) 
		--
	END   
END
--
SET @EndCount = (SELECT SUM(row_count) FROM sys.dm_db_partition_stats WHERE object_name(object_id) = @TableName  AND OBJECT_SCHEMA_NAME(object_id) = @SchemaName AND (index_id < 2))
print 'EndCount: ' + CAST(@EndCount as varchar(max)) + ' |' + CONVERT(char(19),getdate(), 120) 
--

--return data for logging
SELECT 
 @loopCount as 'LoopCount'
,@nDeleteBatchSize as 'nDeleteBatchSize'
,@LastCount as 'Lastcount'
,@StartCount as 'StartCount'
,@EndCount as 'EndCount'
,@EndCount-@StartCount as 'DeleteCount'


END