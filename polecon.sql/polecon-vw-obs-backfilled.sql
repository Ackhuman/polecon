drop view if exists vw_ObservationBackfilled
go

create view vw_ObservationBackfilled
as
	with cte_Backfill as
	(
		select *
		from Observation
	)
	select 
		orig.[Date]
		,orig.[DataPointId]
		,isNull(orig.[Value], backfill.[Value]) as [Value]
	from cte_Backfill orig
	outer apply
	(
		select top 1
			*
		from cte_Backfill
		where [DataPointId] = orig.[DataPointId]
			and [Date] < orig.[Date]
			and [Value] is not null
		order by [Date] desc
	) backfill