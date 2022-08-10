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

Development Environment Configuration
I've used Visual Studio Code to prevent from the bloat of Visual Studio
A plugin named C# by Microsoft was added to Visual Studio Code to aid with intellisense and debugging
You will very likely need a localhost dev certificate which is trusted to debug the webapi
