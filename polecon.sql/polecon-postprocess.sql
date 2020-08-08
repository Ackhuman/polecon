begin tran

	create table #Dedupe (DupeId int, DedupeId int)

	insert into #Dedupe (DupeId, DedupeId)
	select u.id as dupeId, min(dedupe.[id]) as dedupeId
	from unit u
	join unit dedupe on u.[name] = dedupe.[name]
	group by dedupe.[name], u.id
	having u.id <> min(dedupe.id)
	
	select *
	from DataPoint dp
	join Unit u on dp.UnitId = u.Id

	update dp set UnitId = DedupeId
	from DataPoint dp
	join #Dedupe dd on dp.UnitId = dd.DupeId

	delete u
	from Unit u
	join #Dedupe dd on u.Id = dd.DupeId
	
	select *
	from DataPoint dp
	join Unit u on dp.UnitId = u.Id

	drop table #Dedupe

commit tran