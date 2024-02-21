# NumberSorter
ASP.NET Core 8 Project. This project allows the user to record the time it takes to order a list of numbers inside of a SQL database.

## Requirements
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

## Decisions
 - Use EF Core to create and manage the database

## EF Core updating
Run this command from the NumberSorter directory to update the SQL database with any changes made to the schema:
 1. `dotnet ef migrations add "[MESSAGE]" --project NumberSorter.Data   --startup-project NumberSorter.WebUI`
    > Change [MESSAGE] with a quick summary of what is changing on the database
 2. `dotnet ef database update --project NumberSorter.Data --startup-project NumberSorter.WebUI`