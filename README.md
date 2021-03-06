# CovPort
A simplistic covid -19 test portal built using React Js (dotnet-react), Asp.NetCore, Fluent Validator, AutoMappers and vanilla CSS.

## Architecture
This project follows a Domain Driven Design pattern based largely on the Clean architecture (Onion). Other patterns used in the project include  Command Query Responsibility Segregation (CQRS), Factory,  Decorator , Mediator and Repository patterns.

## Installation

```Note`` - To run this project, you should have npm package manager and  .net installed on your device. You also need a database connection string, Git and Cli.
- Copy appsettings.Example.json content into your appsettings.json file created in the api folder.

Sample SQL Server connection string used in this project.

```bash
"Data Source=database-2.cgcficszidtv.us-west-2.rds.amazonaws.com; Initial Catalog=CovPort; User ID=${userId}; Password=${password};"
```

## Running the project:

To run the project follow the following steps on the cli.

- Run ```git clone https://github.com/basilcea/CovPort.git```

- cd into the cloned folder

```bash

 cd src/client

 npm install

 cd  ../src/server/api

 dotnet restore
 
 dotnet build

 dotnet run

```

This launches both the static React files and the asp.net API




## Endpoints

- Project runs on ```http://localhost:5001``` 

- Backend can be accessed  on ```http://localhost:5000/api```

On the frontend, to navigate through the application the following routes are available:

| Routing              | Description                            |
| -------------------  | -------------------------------------- |
| `http://localhost:5001/`  | Home - User to get details using email(pre-set user in the absence of authorization and authentication) and view userSummary  including  pending booking, test results and user info |
| `http://localhost:5001/createBooking` | Allows user to create a booking based on location and date |
| `http://localhost:5001/createSpaces` | Admin can create spaces for a specific location for a day using a calendar |
| `http://localhost:5001/summary`| All users can view daily covport summary of activities or can search for report of a specific date |
| `http://localhost:5001/results`| LabAdmin can create  and update pending tests results |
| `http://localhost:5001/api`  | Get the backend startup details  including health checks and swagger documentation | 
| `http://localhost:5001/swagger`  | Swagger documentation of backend endpoints | 



## Assumptions 

Some assumptions made during the design of the project include:-
- Three roles - User, Admin , LabAdmin with different level of access. 
- LabAdmin only can create and update test results.
- Admin only can create spaces.
- All Users can view spaces, create booking
- User cannot not book more than one space in any location for a particular day
- A booking is closed if a the date of booking is past or the test has been carried out
- A booking created by a user can only be cancelled by the user.
- Reports are cumulative daily reports
- A labadmin create a test results at the time of taking the tests with the status of pending and updates the results at when the result is available
- A user can only view completed tests results (results with the test status as completed having positive or negative values)
- Admin can create spaces ahead of the current date;
- No Authentication is implemented.



## Suggested Improvements

- Frontend: Styling, More Atomic design and use of state container (redux or context api) for better state handling. Conditional routing based on userId.
- Backend: Improved logging, Unit and Integrations tests.

## Defaults Db Seeds

### Spaces

```bash
{
   
    LocationName = "Seychelles",
    Date = DateTime.Parse("2022-01-20"),
    SpacesAvailable = 30,
    SpacesCreated = 30,
    DateCreated = DateTime.Now,
    DateUpdated = DateTime.Now,
},
{
    LocationName = "Malta",
    Date = DateTime.Parse("2022-01-20"),
    SpacesAvailable = 20,
    SpacesCreated = 20,
    DateCreated = DateTime.Now,
    DateUpdated = DateTime.Now,
},
{
    LocationName = "Seychelles",
    Date = DateTime.Parse("2022-01-21"),
    SpacesAvailable = 30,
    SpacesCreated = 30,
    DateCreated = DateTime.Now,
    DateUpdated = DateTime.Now,
}
```

### Users
```bash
{
    Email = "admin.cea@covport.check",
    Name = "cea",
    UserRole = "ADMIN",
    DateCreated = DateTime.Now,
    DateUpdated = DateTime.Now
},
{
    Email = "labadmin.liwu@covport.check",
    Name = "liwu",
    UserRole = "LABADMIN",
    DateCreated = DateTime.Now,
    DateUpdated = DateTime.Now,
},
{
    Email = "me.patient@gmail.com",
    Name = "Andres",
    UserRole = "USER",
    DateCreated = DateTime.Now,
    DateUpdated = DateTime.Now
}

```
### ReportsSummary
 ```
 Default Report Summary for 2022-01-14 and 2022-01-15
 ```

## Updates to Version 1.2
- AutoGeneration of Id  (Identity) disabled for seeding purposes only. Data seeded with corresponding Id to ensure data correctness.
- Logic for SummaryRepository (UserSummary and Reports) rewritten using raw Sql and Views. Views are created on first migration only or if there are any pending Migrations (i.e Migrations Update)
- SQL Folder in Infrastructure Layer contains the various queries used in the project
- AutoMapper included in the Infrastructure layer to map Dbreader to class properties.
- space.SpacesAvailable removed from Logic to ensure thread safety. Available Spaces are now calculated, i.e ```SpaceCreated - existingBookings```
- Space Entity now has boolean property ```Closed```
- Background Worker created that closes all previous spaces of past dates and their corresponding booking.It runs every midnight (12:00). Worker runs on a new instance of Dbcontext to prevent DbContext threading issues for operations running in parallel.
- IWorker Interface and WorkerService created respectively
- To Start Worker 
```bash
 cd src/server/worker
 dotnet watch run
```
- GetSpaceById method in the EntityRepository Updated.




This project was built by [Basil Ogbonna](mailto:ogbonna.basil3@gmail.com)
