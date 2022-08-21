﻿CREATE TABLE [dbo].[Room]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Type] NVARCHAR(50) NOT NULL, 
    [Capacity] INT NOT NULL,
    [HotelId] INT NOT NULL REFERENCES Hotel(Id), 
    [Number] INT NOT NULL
)
