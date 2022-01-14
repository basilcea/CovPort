# CovPort
A simplistic covid -19 test portal built using React Js (dotnet-react), Asp.NetCore, Fluent Validator, AutoMappers and vanilla CSS.

## Architecture
This project follows a Domain Driven Design pattern based largely on the Clean architecture (Onion). Other patterns used in the project include  Command Query Responsibility Segregation (CQRS), Builder, Mediator and Repository patterns.

## Installation

{Note} - To run this project, you should have npm package manager and  .net installed on your device. You also need a database connection string, Git and Cli.

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

 dotnet restore

 cd  ../src/server/api
 
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
| `http://localhost:5001/`  | Home - User to get details using email(pre-set user in the absence of authorization and authentication) and view userSummary  including  pending bookiing, test results and user info|
| `http://localhost:5001/createBooking`| Allows user to create a booking based on location and date|

| `http://localhost:5001/createSpaces` | Admin can create spaces for a specific location for a day using a calendar |
| `http://localhost:5001/summary`|All users can view daily covport summary of activities or can search for report of a specific date|
| `/api`  | Get the backend startup details  including health checks and swagger documentation | 



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
- Backend: Explicit use of Transactions, Dbcontext used currently embeds unitOfWork pattern and transactions, Improved logging.

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

This project was built by [Basil Ogbonna](mailto:ogbonna.basil3@gmail.com)