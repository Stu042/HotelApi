## Introduction

This technical test is designed to see how you approach a complex problem we are looking to test
how you break down your solution, what you consider when you are making your decisions, how
you write and structure code. We are not necessarily looking for a fully working solution or a bug
free solution. After your code test is submitted you will have the opportunity to talk us through your
approach, what you found challenging and why you made the decisions you did.

We think that a reasonable solution for discussion should be produced within 5-8 hours but feel free
to spend as long or as short as you like on this.

## Brief

Create a hotel room booking API using ASP.NET Core and written in C# following RESTful principles.
The solution should be committed to an online repo and access shared with Screenmedia.

If possible, it should be hosted in a Microsoft Azure App Service (free 30 days trials available), please
note this is not a critical requirement.

Use the database solution of your choice.

## Pre-Requisites

- Hotels have 3 room types: single, double, deluxe
- Hotels have 6 rooms
- A room cannot be double booked for any given night.
- Any booking at the hotel must not require guests to change rooms at any point during their
stay.
- Booking numbers should be unique. There should not be overlapping at any given time
- A room cannot be occupied by more people than its capacity

## Core Requirements

The system should provide the following core functionality:

## Business Functionality:

- Find a hotel based on its name
- Find available rooms between two dates for a given number of people
- Book Room
- Find booking details based on booking ref number

## Technical Requirements:

- The API must be testable.
    - For testing purposes the API should expose functionality to allow for seeding and
resetting the data
    - Seeding: Populate database with just enough data for testing
    - Resetting: Remove all data ready for seeding
- The API Requires no authentication

---

## Design

### Notes

Find a hotel based on its name

- standard find in table
- case insenitive?

Find available rooms between two dates for a given number of people

- How many people, more than can fit in a single room?
    - assuming no more than can fit in a single room

### Important initial apects:

- Multiple Hotels
- Each hotel has 6 rooms (fixed)
- Room types: single, double, deluxe
    - Assume capacity of 1 person, 2 people and 2+ people - but no mention of capacity so leave as an option
- Guests not allowed to change rooms within a booking
    - booking must be full duration in same room
- Booking ref **number** must be unique - based on hotel and (booking count)1
    - By numbers do we mean Ids, could they include letters?

---

### Endpoints

- Find a hotel based on its name
    - FindHotel
    - input
        - string name
    - return
        - hotels uint id
- Find available rooms between two dates for a given number of people
    - Note, no requirement for how people are grouped in rooms
    - input
        - uint peopleCount
        - date from
        - date to
    - output
        - collection of
            - hotel id
            - collection of room numbers
- Book Room
    - input
        - hotel id
        - room number
        - date from
        - date to
    - output
        - success or fail
        - booking ref number
- Find booking details based on booking ref number
    - input
        - booking ref number
    - output
        - hotel id
        - hotel name
        - number of people
        - date from
        - date to
        - collection of room numbers

---
### Data

#### Hotel

- id (hotel id) - unique
- name - unique vartext
- fk to room1 - unique
- fk to room2 - unique
- fk to room3 - unique
- fk to room4 - unique
- fk to room5 - unique
- fk to room6 - unique

#### Room

- id - unique uint32
- type string
- capacity uint32

#### Booking

- id - unique uint64
- booking ref (replicated in table) uint64
- Room id
- date from
- date to

