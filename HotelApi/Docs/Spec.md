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

