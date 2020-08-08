select
	o1.[Date]
	,dp1.[Name] as [PriceName]
	,o1.[value] as [Price]
	,u1.[name] as [PriceUnit]
	,dp2.[Name] as [WageName]
	,o2.[value] as [Wage]
	,u2.[name] as [WageUnit]
	,o1.[value] / o2.[value] as [Hours To Afford]
from vw_observationBackfilled o1
join vw_observationBackfilled o2 on o1.[date] = o2.[date]
join datapoint dp1 on o1.DataPointId = dp1.Id
join datapoint dp2 on o2.DataPointId = dp2.Id
join unit u1 on dp1.unitid = u1.id
join unit u2 on dp2.unitid = u2.id
join dataset ds1 on dp1.datasetId = ds1.datasetId
join dataset ds2 on dp2.datasetid = ds2.datasetid
where ds1.datasetid = 1
	and ds2.datasetid = 4
	and o1.datapointid <> o2.datapointid
	and u1.[name] like '%'+u2.[name]+'%'
	and o1.[value] is not null
	and o2.[value] is not null
order by o1.[DataPointId], [Hours To Afford] desc