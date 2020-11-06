# BlogEngine

A blogging engine based on ASP.NET Core

<img src="BlogEngine/BlogEngine.Client/wwwroot/css/Images/project_diagram.png" width="800">

## Technologies
The project based on modern front edge technologies:
 - ASP.NET Core
 - Blazor Server
 - ASP.NET Core Web API
 - Entity Framework Core
 - AutoMapper
 - Syncfusion Blazor
 - Swagger
 - Bootstrap 5
 - BlazorFileReader
 
 ## Features
 - Ability to create and manage
   - Blog
   - Category
   - Subscriber
 - Email notification system
 - Mobile friendly UI
 - Search page with sort functionality
 - Pagination
 - Blog rating system
 - Blog article content PDF converter
 - Real-Time commenting system with replies
 - Register/Login system
 - JWT authentication
 - User’s personal account with the ability to edit user information and personal data
 - Administration through the admin panel  
 - Swagger API documentation
 - HATEOAS
 
## Run on local
- Clone the repository
- To create the database for this project, open it in Visual Studio and set the **BlogEngine.Server** as startup project and **BlogEngine.Core** as the default project in **Package Manager Console**. Then enter `Update-Database`. This should create and setup your database for the project.
- Set multiple startup projects in the following order
  - **BlogEngine.Server**
  - **BlogEngine.Client**
- Login with the default admin account:
  - **Email**: Admin@gmail.com
  - **Password**: Admin
