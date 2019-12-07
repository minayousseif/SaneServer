# SaneServer
A simple dotnet core web application that shares a Brother scanner over the network using [SANE (Scanner Access Now Easy](http://www.sane-project.org/)

### Description
This is a re-write of a golang web application with bindings to the SANE scanner API.

### What is new comparing to the golang version?
The golang version was only an API, Now a React frontend will be added to interact with the backend.

### EF Migrations

dotnet build
dotnet ef migrations add Initial -o ./Server/Data/Migrations
dotnet ef database update
