## Getting started

The solution was developed using Visual Studio Code with the dotnet CLI and Angular (ng) CLI.

To run the web API application:-
`dotnet run --project .\AmCalendar.WebApi\AmCalendar.WebApi.csproj`

To run the unit tests for the web API application:-
`dotnet test /p:CollectCoverage=true`

Swagger can be used to view documentation for the available HTTP endpoints:-
`https://localhost:5001/swagger/index.html`

To run and open the Angular application:-
`ng serve --open`

To run the unit tests for the Angular application:-
`ng test`

## Stack

ASP.NET Core 3.1
EF Core 3.1
SQL Server