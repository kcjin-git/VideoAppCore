-- 비디오 테이블
CREATE TABLE [dbo].[Videos]
(
	[Id] INT NOT NULL Identity(1,1) PRIMARY KEY, 
    [Title] NVARCHAR(MAX)  NULL, 
    [Url] NVARCHAR(MAX) NULL, 
    [Name] NVARCHAR(50) NULL,
    [Company] NVARCHAR(50) NULL,
    [CreatedBy] nvarchar(255) null,
    [Created] DateTime Default(GetDate()),
    [ModifiedBy] nvarchar(255) null,
    [Modified] DateTime null ,

)
Go
