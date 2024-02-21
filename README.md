# NumberSorter
ASP.NET Core 8 Project. This project allows the user to record the time it takes to order a list of numbers inside of a SQL database.

## Requirements
 - Be able to enter multiple numbers, sort them, and store them within a SQL datbase.
 - Provide a message that the the numbers were succesfully stored/organised or if there are any validation issues.
 - Display all the results and be able to sort the list.
 - Allow the user to export the results in a JSON file.

## Structure
The solution consists of 3 projects.
 - Common - contains shared resources across the projects such as view models to prevent circular dependancies
 - Data - responsible for interacting with and modifying the SQL database
 - Services -  acts as a middle man between the data and WebUI, to seperate the logic from the webui project
 - WebUI - responsible for giving the user access to the data, which would allow them to view/modify the data

## Assumptions
 - 

## EF Core updating
`dotnet ef database update --project NumberSorter.Data   --startup-project NumberSorter.WebUI`