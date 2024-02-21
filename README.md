# NumberSorter
ASP.NET Core 8 Project. This project allows the user to record the time it takes to order a list of numbers inside of a SQL database.

## Set up locally

### Prequisites
 - Have ASP.NET Core [8.0.2 SDK and Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed
 - Have a SQL Server running with access to create databases from your user account
 - Have EF Core Tools install `dotnet tool install --global dotnet-ef`

### Run locally
 - Update the NumberSorterDatabase property in the NumberSorter\NumberSorter.WebUI\appsettings.Development.json file to have the correct connection string to your SQL Server
  > Make sure you keep the database name as it is
 - Run this command to create and setup the database on your SQL Server: `dotnet ef database update --project NumberSorter.Data --startup-project NumberSorter.WebUI`
 - Set the NumberSorter.WebUI project as startup and run it (either as debug or live) and you should now be able to use the application

## Project Requirements
 - Be able to enter multiple numbers, sort them, and store them within a SQL datbase.
 - Provide a message that the the numbers were succesfully stored/organised or if there are any validation issues.
 - Display all the results.
 - Allow the user to export the results in a JSON file.

## Structure
The solution consists of 3 projects.
 - Common - contains shared resources across the projects such as view models to prevent circular dependancies
 - Data - responsible for interacting with and modifying the SQL database
 - Services -  acts as a middle man between the data and WebUI, to seperate the logic from the webui project
 - WebUI - responsible for giving the user access to the data, which would allow them to view/modify the data

## Assumptions
 - The SQL Server already exists and is setup (can change which database the )
 - If the user wants to edit an existing record they can delete it and recreate it
 - The styling of the web application is not considered 
 - Its ok for the web application to be running locally either through IIS Express or through IIS
 - Max character length for the list to enter is 4000 due to SQL limitations.

## Decisions
 - Seperate the project into 3 seperate projects for modularisation
 - Use EF Core to create and manage the database

## EF Core updating
Run this command from the NumberSorter directory to update the SQL database with any changes made to the schema:
 1. `dotnet ef migrations add "[MESSAGE]" --project NumberSorter.Data --startup-project NumberSorter.WebUI`
    > Change [MESSAGE] with a quick summary of what is changing on the database
 2. `dotnet ef database update --project NumberSorter.Data --startup-project NumberSorter.WebUI`