CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Surname] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [BirthYear] INT NULL, 
    [Description] NVARCHAR(50) NULL, 
    [OrderDate] NVARCHAR(50) NOT NULL, 
    [OrderTime] INT NOT NULL, 
    [Phone] NVARCHAR(50) NULL
)
