You should check the run book for further information about the project, how to deploy as well as other information.
https://github.com/Vinces-Videos/resources.git

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

### Deployment Instructions
Note, the Terraform scripts are configured to use EC2 instances which should not accrue and costs.

1. Ensure that your git secrets are configured with an AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY for terraform to run
2. Install Terraform within your development environment
2. Run `terraform apply`. 
3. Go to AWS Console, ensure that the following have been created:
    * IAM Role called ecs-agent
    * Auto-scaling group for EC2
    * Launch configuration for EC2
    * ECS Cluster
    * ECS Task Definition
    * ECS Service
    * ECR Repository

Github is responsible for deploying the docker image to the elastic container repository. Have a look inside the created ECR, if you can't see an image there it typically means the deployment of the image has failed from Github Actions

### Debugging Tips
You need a few components to debug vinces videos locally. All will need to be running for it to work:
1. The Mongo DB. This is running within a Docker container, ensure that the volume has been mapped correctly to your docker container and the docker container is running. You can ensure the drive has been successfully mapped by using an SSH connection to the docker instance and running the following commands.
   * `mongo` starts mongo CLI
   * `show dbs` if you can't see vinces-videos in this list, the volume has not been correctly mapped.
   * `use vinces-videos` set's the active database
   * `show collections` lists the available 'tables' within the database
   * `db.Products.find()` lists all items within the Products collection

2. The C# dotnet API. You can run this locally on your machine.
    * Ensure that Api/appsettings.json has the correct connection string to the mongo docker container as well as the correct database name. These are in source control so it should always be correct unless you have a different configuration of the docker container
    * You can use `dotnet run` to start the API, once it's running you can use https://localhost:5001/swagger to view API documentation. This will of course require the database to be running to get data.

3. The front-end. You can use different front ends from the project. The current available front-ends are as follows:
    * React (https://github.com/Vinces-Videos/frontend-react). Can be debugged with `npm start`.
