drop view if exists vw_BarleyBarrelNormalized
go
create view vw_BarleyBarrelNormalized
as
	select
		 trad.[Date]
		,trad.[Value]
		,trad.[Value] / metric.[Value] as [MetricValue]
		,trad.DataPointId
	from Observation trad
	join Observation metric on trad.[Date] = metric.[Date]
	where trad.DataPointId = 2
		and metric.DataPointId = 57
		and trad.[Value] is not null
		and metric.[Value] is not null
go

--Correlation query
SELECT
	[X]
	,[Y]
	,[Unit]
	,format([Correlation], 'P') as [Correlation]
	,[n=]
	,[YearRange]
FROM
(
	SELECT 
		 dpx.[Name] as [X]
		,dpy.[Name] as [Y]
		,u.[Name] as [Unit]
		,(avg(x.[Value] * y.[MetricValue]) - avg(x.[Value]) * avg(y.[MetricValue])) / 
			nullIf((sqrt(avg(x.[Value] * x.[Value]) - avg(x.[Value]) * avg(x.[Value])) 
				* sqrt(avg(y.[MetricValue] * y.[MetricValue]) - avg(y.[MetricValue]) * avg(y.[MetricValue]))), 0)
		as [Correlation] 
		,count(*) as [n=]
		,cast(datepart(year, min(y.[Date])) as varchar(4)) + '-' + cast(datepart(year, max(y.[Date])) as varchar(4)) as [YearRange]
	FROM observation x
	JOIN vw_BarleyBarrelNormalized y ON x.[date] = y.[date]
	JOIN [DataPoint] dpx on x.DataPointId = dpx.Id
	JOIN [DataPoint] dpy on y.DataPointId = dpy.Id
	JOIN [Unit] u ON dpy.UnitId = u.Id
	WHERE x.[Value] IS NOT NULL
		AND y.[MetricValue] IS NOT NULL
		AND y.[DataPointId] < 57
		AND x.[DataPointId] > 62
		AND x.[DataPointId] <> y.DataPointId
	GROUP BY dpx.[Name], dpy.[Name], u.[Name]
	HAVING count(*) > 20
) S
ORDER BY [Y] desc, [YearRange]

GO