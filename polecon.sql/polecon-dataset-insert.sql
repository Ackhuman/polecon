insert into DataSet
(
	[Name]
	,[Description]
)
values
(
	'Swedish Consumer Prices'
	,'Historical price data for basic commodities from Sverige Riksbank from historical records over the last 800 years'
),
(
	'Swedish Measurements'
	,'Traditional Swedish measurements converted to metric units'
),
(
	'Swedish Currency Characteristics'
	,'Weights and metal contents of Swedish currencies from Sverige Riksbank'
)

update DataPoint set DataSetId = (select top 1 DataSetId from DataSet where [Name] = 'Swedish Consumer Prices')
where Id < 57

update DataPoint set DataSetId = (select top 1 DataSetId from DataSet where [Name] = 'Swedish Measurements')
where Id between 57 and 62

update DataPoint set DataSetId = (select top 1 DataSetId from DataSet where [Name] = 'Swedish Currency Characteristics')
where Id > 62