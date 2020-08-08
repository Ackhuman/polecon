create table #Dates
(
	[date] datetime2
)
insert into #Dates
select distinct [date]
from Observation
declare @cols varchar(max) = STUFF((
	SELECT distinct ',' + QUOTENAME([date]) 
    FROM #Dates
    FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)') 
,1,1,'')

declare @displayCols varchar(max) = STUFF((
	SELECT distinct ',' + QUOTENAME([date]) + ' as ' + QUOTENAME(replace([date], '-01-01 00:00:00.0000000', ''))
    FROM #Dates
    FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)') 
,1,1,'')

declare @sql varchar(max) = '
	select ' + @displayCols + '
	from (
		select 
			 [date]
			,[value]
		from observation o
		join datapoint dp on o.datapointid = dp.id
	) x
	pivot
	(
		avg([value])
		for [date] in (' + @cols + ')
	) p
'
execute(@sql)

drop table #Dates