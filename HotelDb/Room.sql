CREATE TABLE [dbo].[Room]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Style] NVARCHAR(50) NOT NULL, 
    [Capacity] INT NOT NULL,
    [HotelId] INT NOT NULL REFERENCES Hotel(Id), 
    [Number] INT NOT NULL
)

GO
CREATE NONCLUSTERED INDEX [RoomCapacity]
    ON [dbo].[Room] ([Capacity]);
GO
CREATE NONCLUSTERED INDEX [RoomHotelId]
    ON [dbo].[Room] (HotelId);
GO
