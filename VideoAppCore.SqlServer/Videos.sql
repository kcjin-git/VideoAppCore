-- 비디오 테이블
CREATE TABLE [dbo].[Videos]
(
	[Id] INT NOT NULL Identity(1,1) PRIMARY KEY, 
    --[Created] DATETIMEOFFSET NULL 
    --    Default(SysDateTimeOffset() at time zone 'Korea Standard Time'),

    [Created] DateTime  null Default(GetDate()),
    [CreatedBy] nvarchar(255) null,

    [Modified] DateTime null ,
    [ModifiedBy] nvarchar(255) null,

    [Title] NVARCHAR(MAX) NOT NULL, 
    [Url] NVARCHAR(MAX) NULL, 
    [Name] NVARCHAR(50) NULL,
    [Company] NVARCHAR(50) NULL
)
Go
