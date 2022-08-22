# Design

# Configuration

Minimum configuration is required, but we do have a connection string to the database - containing username and password. This should be stored in a KeyVault on Azure and in a Secrets files when developing locally.

## Endpoint and Arguments

Each endpoint can return an ErrorResponse, this contains a string message with details of why it can't perform its function fully.

Note, all dates on input are all split into their components; year, month and day. We don't consider time, but usual convention is along the lines of all checkouts are before 12:00 and all checkins are after 14:00.

Also regarding date ranges ensure to check To date is after From date.

### FindHotel

- input
    - string name
- return
    - hotel Id
    - name
    - rooms (collection of)
        - id
        - number
        - style
        - capacity

### AvailableRooms

- input
    - int peopleCount
    - date from
    - date to
- output
    - collection of
        - hotel name
        - room style/type
        - room capacity

### BookRoom

- input
    - hotel id
    - room number
    - date from
    - date to
- output
    - success or fail
    - booking ref number

### Endpoint: FindBooking

- input
    - booking ref number
- output
    - hotel id
    - hotel name
    - number of people
    - date from
    - date to
    - room Id
    - room style/type

---

# Database Tables

## Hotel

- Id (hotel id) - PK, unique
- Name - unique vartext

Indexed by Id and Name.

## Room

- Id - PK, unique int32
- Number int32
- Type string
- Capacity int32
- HotelId

Indexed by HotelId and Capacity.

## Booking

- Id - PK, unique int64
- Booking ref - unique int64
- Room id
- Date from
- Date to

Indexed by Booking ref, From and To dates.
