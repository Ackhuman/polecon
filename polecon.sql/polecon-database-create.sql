
drop table if exists ObservationSeries
go
drop table if exists Observation
go
drop table if exists Series
go
drop table if exists DataPoint
go
drop table if exists UnitComposition
go
drop table if exists Unit
go
drop table if exists DataSetSource
go
drop table if exists DataSet
go
drop table if exists [Source]

create table [Source]
(
	SourceId int identity(1,1)
		constraint PK_Source primary key
	,[Name] varchar(max)
	,[Description] varchar(max)
	,[IsPrimary] bit not null
		constraint DF_Source default(1)
	,[Date] datetime2
	,[Href] varchar(200)
)

create table DataSet
(
	 DataSetId int identity(1,1)
		constraint PK_DataSet primary key
	,[Name] varchar(max)
	,[Description] varchar(max)
)

create table DataSetSource
(
	DataSetId int not null
		constraint FK_DataSetSource_DataSet foreign key
		references DataSet(DataSetId),
	SourceId int not null
		constraint FK_DataSetSource_Source foreign key
		references [Source](SourceId)
)

create table Unit
(
	 Id int identity(1,1)
		constraint PK_Unit primary key
	,[Name] varchar(max)
	,[Description] varchar(max)
	,[Sigil] nvarchar(10)
	,[IsPrefix] bit not null
		constraint DF_UnitIsPrefix default(0)
	,[DefaultMagnitude] int not null
		constraint DF_DefaultMagnitude default(0)
	,[FormulaJson] varchar(max) null
)
go

create table UnitComposition
(
	UnitA int
		constraint FK_UnitA foreign key
		references Unit(Id)
	,UnitB int
		constraint FK_UnitB foreign key
		references Unit(Id)
)
go

create table DataPoint
(
	 Id int identity(1,1)
		constraint PK_DataPoint primary key
	,[Name] varchar(max)
	,[Description] varchar(max)
	,[UnitId] int null
		constraint FK_DataPoint_Unit foreign key
		references Unit(Id)
	,[DataSetId] int null
		constraint FK_DataPoint_DataSet foreign key
		references DataSet(DataSetId)
)
go

create table Series
(
	 Id int identity(1,1)
		constraint PK_Series primary key
	,[Name] varchar(max)
	,[Description] varchar(max)
	,[Date] datetime2 null
	,[SortOrder] int null
)
go


create table Observation
(
	 Id int identity(1,1)
		constraint PK_Observation primary key
	,[Value] numeric(16, 8)
	,[Date] datetime2 null
	,[SortOrder] int null
	,[DataPointId] int not null
		constraint FK_Observation_DataPoint foreign key
		references DataPoint(Id)
)
alter table Observation 
add constraint EnsureDataIsOrdered 
check([Date] is not null or [SortOrder] is not null)


create table ObservationSeries
(
	SeriesId int not null
	,DataPointId int not null
		constraint FK_ObservationSeries_DataPoint foreign key
		references DataPoint(Id)
)
go