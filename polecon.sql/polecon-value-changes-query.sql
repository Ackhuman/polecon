declare @serieyId int = (select top 1 id from datapoint)
declare @gSilverPerMarkId int = 63
declare @timelagYears int = 1

SELECT
	[X]
	,[Y]
	,format([Correlation], 'P') as [Correlation]
	,[n=]
	,[YearRange]
FROM
(
	SELECT 
		 dpx.[Name] as [X]
		,dpy.[Name] as [Y]
		,(avg(x.[Value] * y.[Value]) - avg(x.[Value]) * avg(y.[Value])) / 
			nullIf((sqrt(avg(x.[Value] * x.[Value]) - avg(x.[Value]) * avg(x.[Value])) 
				* sqrt(avg(y.[Value] * y.[Value]) - avg(y.[Value]) * avg(y.[Value]))), 0)
		as [Correlation] 
		,count(*) as [n=]
		,cast(datepart(year, min(y.[Date])) as varchar(4)) + '-' + cast(datepart(year, max(y.[Date])) as varchar(4)) as [YearRange]
	--silver content of coin
	FROM vw_ObservationsBackfilled x
	--price
	JOIN vw_ObservationsBackfilled y ON datepart(year, x.[date]) = datepart(year, y.[date]) + @timelagYears
	JOIN [DataPoint] dpx on x.DataPointId = dpx.Id
	JOIN [DataPoint] dpy on y.DataPointId = dpy.Id
	JOIN [DataSet] dsx on dpx.DataSetId = dsx.DataSetId
	JOIN [DataSet] dsy on dpy.DataSetId = dsy.DataSetId
	WHERE x.[Value] IS NOT NULL
		AND y.[Value] IS NOT NULL
		AND dsy.[Name] = 'Swedish Consumer Prices'
		AND dsx.[Name] = 'Swedish Currency Characteristics'
		AND x.[DataPointId] <> y.DataPointId
	GROUP BY dpx.[Name], dpy.[Name]
	HAVING count(*) > 20
) S
ORDER BY [Y] desc, [YearRange]


SELECT
	count(case when [Correlation] >= 0.5 then 1 end) as [Positive Correlation]
	,count(case when [Correlation] <= -0.5 then 1 end) as [Negative Correlation]
	,count(case when [Correlation] between -0.5 and 0.5 then 1 end) as [No/Weak Correlation]
	,count([Correlation]) as [#]
FROM
(
	SELECT 
		 dpx.[Name] as [X]
		,dpy.[Name] as [Y]
		,(avg(x.[Value] * y.[Value]) - avg(x.[Value]) * avg(y.[Value])) / 
			nullIf((sqrt(avg(x.[Value] * x.[Value]) - avg(x.[Value]) * avg(x.[Value])) 
				* sqrt(avg(y.[Value] * y.[Value]) - avg(y.[Value]) * avg(y.[Value]))), 0)
		as [Correlation] 
		,count(*) as [n=]
		,cast(datepart(year, min(y.[Date])) as varchar(4)) + '-' + cast(datepart(year, max(y.[Date])) as varchar(4)) as [YearRange]
	FROM vw_ObservationsBackfilled x
	JOIN vw_ObservationsBackfilled y ON x.[date] = y.[date]
	JOIN [DataPoint] dpx on x.DataPointId = dpx.Id
	JOIN [DataPoint] dpy on y.DataPointId = dpy.Id
	JOIN [DataSet] dsx on dpx.DataSetId = dsx.DataSetId
	JOIN [DataSet] dsy on dpy.DataSetId = dsy.DataSetId
	WHERE x.[Value] IS NOT NULL
		AND y.[Value] IS NOT NULL
		AND dsy.[Name] = 'Swedish Consumer Prices'
		AND dsx.[Name] = 'Swedish Currency Characteristics'
		AND x.[DataPointId] <> y.DataPointId
	GROUP BY dpx.[Name], dpy.[Name]
	HAVING count(*) > 20
) S