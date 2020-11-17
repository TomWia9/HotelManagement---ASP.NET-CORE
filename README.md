# HotelManagement - API
This repository conatins API for hotel management.

## Description
This project is a **.NET 5** implemented Web API for managing hotel rooms, clients and bookings.

**HotelManagement - API** enables communication with the **SQL database** consisting of sending and receiving data regarding bookings, rooms and clients. Hotel can be managed by administrators who may make bookings for clients. Each administrator has unique email and password that have to be used to login to the system.  
(Default login data: *email: admin@admin, password: admin*)

It uses **Entity Framework Core** to communicate with a database, which contains required data tables like:
* Clients - where informations about clients are stored 
* Rooms - where informations about rooms are stored 
* Bookings-  where informations about bookings made by clients are stored.
* Administrators- where informations about administrators of hotel and theirs login data are stored  

Other tools used in project:
* **JwtBearer** - for authentication
* **Open API** - for documentation
* **AutoMapper** - for mapping DTO-s and EntityModels data
* **FluentValidation** - for data validation

## Controllers and theirs operations:
### Clients - Controller for clients management
<details>
  <summary>Click to expand!</summary>
  
* **POST a new client**  
 ```/api/clients/```
* **GET a single client**  
 ```/api/clients/{id}```
* **GET all clients**  
 ```/api/clients/```
* **DELETE a client**  
 ```/api/clients/{id}```
* **PUT (full patch) a client**  
 ```/api/clients/{id}```
* **PATCH (partial patch) a client**  
 ```/api/clients/{id}```
</details>

### Rooms - Controller for rooms management
<details>
  <summary>Click to expand!</summary>
  
* **POST a new room**  
 ```/api/rooms/```
* **GET a single room**  
 ```/api/room/{id}```
* **GET all rooms**  
 ```/api/rooms/```
* **DELETE a room**  
 ```/api/rooms/{id}```
* **PUT (full patch) a room**  
 ```/api/rooms/{id}```
* **PATCH (partial patch) a room**  
 ```/api/rooms/{id}```
</details>

### Bookings - Controller for bookings management
<details>
  <summary>Click to expand!</summary>
  
* **POST a new booking**  
 ```/api/bookings/```
* **GET a single booking**  
 ```/api/bookings/{id}```
* **GET all bookings**  
 ```/api/bookings/```
* **DELETE a booking**  
 ```/api/bookings/{id}```
* **PUT (full patch) a booking**  
 ```/api/bookings/{id}```
</details>

### Administrators - Controller for administrators management
<details>
  <summary>Click to expand!</summary>
  
* **POST the new administrator**  
 ```/api/administrators/```
* **GET a single administrator**  
 ```/api/administrators/{id}```
* **GET all administrators**  
 ```/api/administrators/```
* **DELETE the administrator**  
 ```/api/administrators/{id}```
* **PUT (full patch) the administrator**  
 ```/api/administrators/{id}```
</details>

### Statistics - Controller for statistics reciving
<details>
  <summary>Click to expand!</summary>
  
* **GET the most popular rooms**  
 ```/api/statistics/rooms```
* **GET the clients with the most bookings**  
 ```/api/statistics/bookings```
* **GET the total earned money from bookings from defined peroid of time**  
 ```/api/statistics/money```
</details>

### Identity - Controller for administrators authentication
<details>
  <summary>Click to expand!</summary>
  
* **POST authenticate**  
 ```/api/identity/authenticate```
</details>

For more documentation data, visit 
```/swagger/HotelManagementOpenAPISepcification/swagger.json```  
(or ```/index.html``` for documentation UI)

## Installation
Make sure you have the **.NET 5.0 SDK** installed on your machine. Then do:  
>`git clone https://github.com/TomWia9/HotelManagement---ASP.NET-CORE.git`  
`cd HotelManagement---ASP.NET-CORE\HotelManagement\`  
`dotnet run`

## Configuration
This will need to be perfored before running the application for the first time
1. You have to change ConnectionString in **appsettings.json** for ConnectionString that allow you to connect with database in your computer.
2. Issue the Entity Framework command to update the database  
`dotnet ef database update`
 
## License
[MIT](https://choosealicense.com/licenses/mit/)
