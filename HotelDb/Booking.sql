CREATE TABLE [dbo].[Booking]
(
	[Id] BIGINT NOT NULL PRIMARY KEY,
	[Ref] BIGINT NOT NULL,
	[RoomId] INT NOT NULL REFERENCES Room(Id),
	[GuestCount] INT NOT NULL,
	[From] DATE NOT NULL,
	[To] DATE NOT NULL
);

GO
CREATE NONCLUSTERED INDEX [BookingFrom]
    ON [dbo].[Booking] ([From]);
GO
CREATE NONCLUSTERED INDEX [BookingTo]
    ON [dbo].[Booking] ([To]);
GO
