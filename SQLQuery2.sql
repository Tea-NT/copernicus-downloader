CREATE TABLE [dbo].[ImageFile](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[Name] [NVARCHAR](200) NULL,--文件名
	[Type] [NVARCHAR](50) NULL,--文件类型
	[Image] [IMAGE] NULL--文件
)

CREATE TABLE [dbo].[ImageFilesG](
	[QuadKey] NVARCHAR(50) NOT NULL,
	[Name] [NVARCHAR](200) NULL,--文件名
	[Type] [NVARCHAR](50) NULL,--文件类型
	[Image] [IMAGE] NULL--文件
)


INSERT dbo.ImageFiles
(
    QuadKey,
    Name,
    Type,
    Image
)
VALUES
(   NULL, -- Id - uniqueidentifier
    N'',  -- Name - nvarchar(200)
    N'',  -- Type - nvarchar(50)
    NULL  -- Image - image
    )