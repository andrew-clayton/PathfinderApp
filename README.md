# Pathfinder
This is C.H. Robinson's Software Engineering Intern interview project, built using C# with ASP.NET on .NET 8.0 and xUnit.

## **Objective**
Create an application that receives a three-letter code for a North American Country and returns a list of all countries a
driver must travel through to go from the United State of America to the destination.

## **Deployment**

This is deployed at: [https://pathfinder20241013135746.azurewebsites.net](https://pathfinder20241013135746.azurewebsites.net)

To make a request, use this format: https://pathfinder20241013135746.azurewebsites.net/Countries/Path/{countryCode}
- The API expects this *Countries/Path/{countryCode}* format

For example, https://pathfinder20241013135746.azurewebsites.net/Countries/Path/usa will return: *["USA"]*

---
**To run this locally:** 
- Run the Dockerfile. This project will use a Swagger page but requests can be more manually as well.
  - The URi will look like this example: https://localhost:32779/swagger/index.html
- (or) run with  Visual Studio, where the Pathfinder project can be built and run.
  - By default, this will open a Swagger page (to show the API) when run.   
  - Note that in this solution, there are two projects: Pathfinder and Pathfinder_UnitTests. Pathfinder is an ASP.NET project, and Pathfinder_UnitTests is an xUnit project.


## **Code practices and structure**
This is a simple project, but I wanted to adhere to best practices for the bigger projects such as DI injection and unit testing. 

The crux of this project is the *PathfinderService.cs* file, with the PathfinderService class. This executes the logic of this assignment (finding a list of countries to go from the USA to Mexico, for example). 


**appsettings.json** (configuration file)
- This determines what country is the starting point (this is currently configured to "USA", as specified in the project assignment).
- This populates the adjacency list that is being used to relate countries to one another, to allow for flexibility.

**PathfinderService class** (more details)
- This class inherits from an IPathfinderService interface to enable more flexibility with DI.
- These are examples that I wanted to highlight to show how I am trying to adhere to best practices, even though this project could've been simpler without showing off this.

**Unit testing**
- There are two unit testing files for PathfinderController and PathfinderService. These are units tests done using the xUnit package.
- I considered but did not add a Postman test suite for the sake of keeping all of the work for this in a couple of hours.
- The Pathfinder.http file also always for some basic HTTP tests that have been set up.
---
These are the main things I wanted to highlight, and I am sure everything will be explored more. 

I really appreciate the time that anybody spends looking through this, and I look forward to discussing this project more.
