/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 10 [QuadKey]
      ,[Name]
      ,[Type]
      ,[Image]
  FROM [tests].[dbo].[ImageFilesG]


SELECT	* FROM dbo.ImageFilesG WHERE LEN(QuadKey)>15

SELECT	COUNT(*) FROM dbo.ImageFilesG WHERE LEN(QuadKey)>15

SELECT	COUNT(*) FROM dbo.ImageFilesG WHERE LEN(QuadKey)>15

SELECT	COUNT(*) FROM dbo.ImageFilesG WHERE LEN(QuadKey)=15


SELECT	COUNT(*) FROM dbo.ImageFilesG WHERE LEN(QuadKey)=16

SELECT	COUNT(*) FROM dbo.ImageFilesG WHERE LEN(QuadKey)=17




--SELECT * FROM dbo.ImageFilesG WHERE CHARINDEX()

SELECT CHARINDEX('_','16_53444_27547_',10)

SELECT SUBSTRING('16_53444_27547_',0, LEN('16_53444_27547_'))


--UPDATE dbo.ImageFilesG SET Name = SUBSTRING(Name,0,LEN(name))

SELECT * FROM dbo.ImageFilesG WHERE LEN(QuadKey) = 15 ORDER BY Name
SELECT * FROM dbo.ImageFilesG WHERE QuadKey LIKE '2%' AND LEN(QuadKey)=2

