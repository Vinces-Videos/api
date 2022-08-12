## Development Environment
Some useful tips for setting up your development environment

The IDE we've landed on is **Visual Studio Code**, feel free to use any you like but some of the steps below may not work for you.

#### Setup Instructions
- Install Visual Studio Code
- (optional) Install C# extension (by Microsoft) for Visual Studio Code : Extension Id (ms-dotnettools.csharp)

---
### Web Application
TODO

#### Technology
- Javascript
- GOV.UK Framework

#### Setup Instructions

---
### Back-end API
Some notes and information about getting the back-end set up to develop the back-end.

#### Technology
- C# .NET Core 6.0
- MSTest (Part of .NET Core) : This is what we're using for our unit testing

#### Setup Instructions
1. Install .NET Core, I installed this via homebrew. The command is `brew install dotnet`
2. Clone the vincesvideos repo to a desired path
3. Go to Visual Studio Code, ensure you can use the terminal (view > terminal)
4. Navigate to the directory
5. From the root vincesvideos folder, attempt to build the api `dotnet build api` (or `dotnet build`) if you're already in the api folder.
6. From the Api.Tests folder, attempt to run the unit tests `dotnet test Api.Tests` (or `dotnet test`) if you're already in the Api.Tests folder.
7. Run the api project via `dotnet run --project api` or `dotnet run`
8. Ensure swagger responds on the URL https://localhost:port/swagger (the port can be found in your visual studio code terminal). If you have localhost certificate issues, refer to [this document](https://docs.google.com/document/d/1Tmq-iHIfXu_bkeJ0O5CPDPkoUMKZeDTVcT0o48Vme1s/edit)
9. I have created some demo endpoints to test which will allow you to ensure everything is set up correctly. Try one of the demo urls https://localhost/Demo or https://localhost/Demo/5 (the number is arbitrary at the moment)

#### Development Environment Configuration
* I've used Visual Studio Code to prevent from the bloat of Visual Studio
* A plugin named C# by Microsoft was added to Visual Studio Code to aid with intellisense and debugging
* You will very likely need a localhost dev certificate which is trusted to debug the webapi
