--Wheat prices from Rome

insert into DataSet
	([Name], [Description])
values
	('Edict on Maximum Prices', 'Edict by Diocletian that set standard prices in Rome')

declare @dataSetId int = scope_identity()

insert into Unit
	([Name], [Description], [Sigil])
values
	('Sestertius', 'Roman coin based on two and a half Asses', 'HS')

DECLARE @km int = (SELECT TOP 1 Id FROM Unit WHERE [Name] = 'Kilometer')
DECLARE @hs INT = (SELECT TOP 1 Id FROM Unit WHERE [Name] = 'Sestertius')

insert into DataPoint
	([Name], [Description], [UnitId], [DataSetId])
values
	('Rome Price', 'Price in sestertii in Rome', @hs, @dataSetId),

DECLARE @price INT = (SELECT TOP 1 Id FROM DataPoint WHERE [Name] = 'Rome Price')

INSERT INTO Observation
	([Value], [SortOrder], [DataPointId])
VALUES
	(427, -77, @distance),
	(1363, -150, @distance),
	(1510, -150, @distance)