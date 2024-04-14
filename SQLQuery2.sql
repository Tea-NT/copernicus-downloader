CREATE TABLE [dbo].[ImageFile](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[Name] [NVARCHAR](200) NULL,--�ļ���
	[Type] [NVARCHAR](50) NULL,--�ļ�����
	[Image] [IMAGE] NULL--�ļ�
)

CREATE TABLE [dbo].[ImageFilesG](
	[QuadKey] NVARCHAR(50) NOT NULL,
	[Name] [NVARCHAR](200) NULL,--�ļ���
	[Type] [NVARCHAR](50) NULL,--�ļ�����
	[Image] [IMAGE] NULL--�ļ�
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