CREATE TABLE [dbo].[Room]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Type] NVARCHAR(50) NOT NULL, 
    [Capacity] INT NOT NULL,
    [Hotel] INT NOT NULL REFERENCES Hotel(Id)
)
