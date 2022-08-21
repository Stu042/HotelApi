# Risks and Assumptions

## What we **know**

- Multiple Hotels
- Each hotel has 6 rooms (fixed)
- Room types: single, double, deluxe
    - Assume capacity of 1 person, 2 people and 2+ people - but no mention of capacity so leave as an option
    - "Hotels have 3 room types: single, double, deluxe", does this mean every hotel has at least one of each type?
- Guests not allowed to change rooms within a booking
    - booking must be full duration in same room
- Booking ref **number** must be unique - based on hotel and (booking count)

## Endpoints

### Endpoint: FindHotel

Find a hotel based on its name.

- No mention of what to return, quck converstion from Name to Id? Or full information required?
    - Probably most information; name, rooms not bookings as could be many.
- Case sensitive or insensitive search?

### Endpoint: AvailableRooms

Find available rooms between two dates for a given number of people.

- How many people, more than can fit in a single room?
    - If so how are they grouped, 3 people = 3 singles or 1 double and 1 single?
    - Assuming no more than can fit in a single room.
        - Will shine a light on this, return error if guest count is greater than max capacity of any room.
- Searching all rooms in all hotels.
- Hotels can usually turn a room over on the same day, check out in the morning book in same afternoon/evening.
    - So "from" date can be same date the room is last used and "to" date can be same date a future booking will start.

### Endpoint: BookRoom

Book a room for a number of guests.

- For a booking, guests must stay in one room for full duration.
- No matter how we call this the room could of been taken, even in the short duration between calls.
    - so lock db - this rooms row, check availability and book if available, else return a room not available error.

### BookingDetails

Find booking details based on booking ref number.

- Should be standard db search and return details.
- Allow for returning not found error.

---

## Issues with Data

Currently we don't enforce maximum of six rooms per hotel.
Id's should all be unique with auto increment.
