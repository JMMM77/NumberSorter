# NumberSorter

This project allows the user to store how long it takes sort a list of numbers.

I am using this project as a way to explore frameworks/tools/patterns etc... so a lot of it will be overly engineered for what it is, but its all for fun.

## Set up locally

### Prequisites

- Have ASP.NET Core [10 SDK and Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) installed
- Have [Docker Desktop](https://www.docker.com/products/docker-desktop) installed
- Have [Aspire](https://aspire.dev/uk/get-started/install-cli) installed

### Run locally

Both methods should get Aspire to spin up the necessary resources and open a dashboard allowing you to test the application.

- Via Visual Studio - Set the NumberSorter.AppHost project as startup and run it (either as debug or live)
- Via Aspire CLI - Run `aspire run` in PowerShell within this repo

## Project Requirements

- Be able to enter multiple numbers, sort them, and store them within a SQL datbase
- Provide a message that the the numbers were succesfully stored/organised or if there are any validation issues
- Display all the results
- Allow the user to export the results in a JSON file

## Structure

The solution consists of multiple projects.

- WebUI - Responsible for giving the user access to the data, which would allow them to view/modify the data. This calls the Web API project to perform the CRUD operations
- Api - Responsible for exposing Web APIs to view/modify the data. This abstracts any of the business/data logic away from the web ui, also allowing for other non-dotnet projects to interact with the data
- Services - Acts as a middle man between the data and api, to seperate the business logic away from the front and data layer
- Data - Responsible for interacting with and modifying the database
