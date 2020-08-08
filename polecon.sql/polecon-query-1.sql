declare @series1Id int = (select top 1 id from datapoint)
declare @xAxisId int = 63

--select * from datapoint

select 
s1.[date]
--, s1.[Value], dp1.[name], u1.[name], x.[value], dpx.[name], ux.[name], 
,s1.[value] * x.[value] as [g silver per last]
from observation s1
join datapoint dp1 on s1.datapointid = dp1.id
join unit u1 on dp1.unitid = u1.id

join observation x on s1.[date] = x.[date]
join datapoint dpx on x.datapointid = dpx.id
join unit ux on dpx.unitid = ux.id

where s1.datapointid = @series1Id
and x.datapointid = @xAxisId
and s1.[value] is not null
and x.[value] is not null
order by [g silver per last]