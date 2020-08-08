INSERT INTO DataSet([Name], [Description])
VALUES
	('Wages of Swedish Laborers', 'Historical wages of Swedish Laborers released by Sverige Riksbank')
DECLARE @DataSetId INT = (SELECT TOP 1 DataSetId FROM DataSet WHERE [Name] ='Wages of Swedish Laborers')
INSERT INTO Unit ([Name])
VALUES
	('Öre'),
	('Daler kopparmynt'),
	('Skilling'),
	('Skilling Banco'),
	('Grams of Silver')

INSERT INTO DataPoint ([Name], [Description], [UnitId], [DataSetId]) VALUES
('Labourer Wages in Öre', 'Öre 
1540–1623 
(1 öre = 1/8 mark örtug)', (SELECT TOP 1 Id from Unit WHERE [Name] = 'Öre'), @DataSetId),
('Labourer Wages in Daler kopparmynt', 'Daler kopparmynt 1624–1775', (SELECT TOP 1 Id from Unit WHERE [Name] = 'Daler kopparmynt'), @DataSetId),
('Labourer Wages in Skilling', 'Skilling 
1776–1803', (SELECT TOP 1 Id from Unit WHERE [Name] = 'Skilling'), @DataSetId),
('Labourer Wages in Skilling Banco', 'Skilling Banco 1804–1850', (SELECT TOP 1 Id from Unit WHERE [Name] = 'Skilling Banco'), @DataSetId),
('Labourer Wages in Real wage', 'Real wage
(1950 = 100)', NULL, @DataSetId),
('Labourer Wages in Grams of Silver', 'Wage in gram of silver', (SELECT TOP 1 Id from Unit WHERE [Name] = 'Grams of Silver'), @DataSetId)
