# Prerequisites

Before running the solution, make sure you have the following:

1. Visual Studio 2022 (Any version)
2. SQL Server 2012 or later.

# Steps to Run Locally with Visual Studio

1. Navigate to the "scripts" folder located in the root directory and execute the database creation script. It will create all the neccesary objects to execute the solution properly.

   ![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/280dcc83-59ea-4fae-befa-337c4e8963a8)
   
2. Modify ConnectionString for Identity.API and Exercise.API project, you can do this in the appsettings.json file.
   
   ![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/6346f4ae-325c-4748-ae0c-6ff436d4103d)

3. The solution is running with local certificates. If you intend to test it with Postman, please note that you need to disable the "Enable SSL certificate verification" option.

   ![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/6380b601-ca22-42a5-9b2e-e4efc779cc1b)
   
4. Open the solution in Visual Studio, and the first thing you need to do is set multiple startup projects. Select Exercise.API, Exercise.UI, and Exercise.Identity projects.

    ![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/39f84a7c-25a9-4546-a120-021337e36a4b)
   
5. Proceed to run the project.
6. Once it is executed, three browser tabs will open on your machine. If you have reached this point without any issues, you can start exploring the solution.

# Steps to Invoke Services

To invoke the Exercise.API enpoints, it is important to note that is protected by Identity.API. This means that you cannot access the product endpoints without providing a valid token. Therefore, there are two ways to generate a token.

**Generating a Token with Initialization User:**

1. Start the solution by selecting the multiple projects.
2. Open Postman or Swagger for the Identity.API project.
3. Set the URL to `https://localhost:7209/identity/token` with the POST method.
4. In the payload, provide the credentials of the initialization user. These credentials will only exist if the database initialization script was executed.

    ```json
    {
        "Email": "eflorespalma@gmail.com",
        "Password" : "*1nd3strvct1bl3*"
    }
    ```
![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/afc5b420-e655-4f91-b96c-aa6837bc9206)

**Registering a New User:**

1. Start the solution by selecting the multiple projects.
2. Open Postman or Swagger for the Exercise.API project.
3. Set the URL to `https://localhost:7210/user` with the POST method.
4. The required payload for registration is the Email and Password of the user. For example:

    ```json
    {
        "Email": "iamdeveloper@gmail.com",
        "Password" : "*IZI321*"
    }
    ```  
![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/6b07b090-5bee-4c06-a034-85d46cbdfd66)

5. Once the new user is registered, you can retrieve a new token using the newly created user. To do this, repeat steps 1 to 4 from the first method, replacing the values with the credentials of the new user.

With the generated token, you can now use the product endpoints of the Exercise.API project. You can use either Swagger or Postman. Please remember to add the following header: Authorization: Bearer [TOKEN_GENERATED]. In Swagger, the "Authorize" button is enabled for you to enter the token.

## Not Access
![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/ba1a60de-2dae-41d1-b07f-b2c236433307)

## Authorize Button
![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/078539b5-45f3-489f-a8f8-debce8987cef)

## Insert Token and Authorize
![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/839a94a4-d37c-49d8-8289-ee82aec4e8e1)

## Retrieve Products
![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/3cc3712f-8054-44ba-8ddf-b8e0b99a532d)

6. Finally, a UI with Blazor was added, where a service is consumed. It is important to note that, for demonstration purposes, a public endpoint was enabled using the [AllowAnonymous] attribute

![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/49058c41-8daa-4ad3-8fb1-d775c3eaae6a)

 ---
**NOTE**

The project includes the launchsetting.json files for both the Identity.API and Exercise.API projects. If you encounter any issues with the defined port and need to use a different one, you will need to modify certain properties in the appsettings.json file.

## Modify properties in both Identity.API and Exercise.API projects

![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/2bd93a5d-5ef0-43b5-8d59-1916c572330f)

--- 

# Project Overview

## API Layer
- Responsible for exposing the project's functionalities as RESTful API endpoints.
- Handles incoming requests, authorization, and request validation.
- Utilizes Serilog for implementing structured logs in physical files.
- Implements a global filter to handle exceptions in a centralized location.
- Implements the problem details specification to provide a standardized structure for error responses.
- Uses the Swagger library for documenting the endpoints.

## Identity Layer
- Manages user authentication and authorization using a simple approach to generate a JWT token based on user credentials.
- Provides services for token generation and login validation.

## Business Logic
- Contains the core business logic and rules of the application.
- Implements the unit of work pattern in this layer for handling multiple database operations.
- Uses FluentValidations to facilitate data validation and separate the validation logic from the core business code.

## Domain Layer
- Represents the domain model of the application.
- Only the entity is responsible for modifying its properties, either through constructors for initialization or methods within the same object.

## Repository Layer
- Implements data access and persistence using Dapper.
- Provides repositories for interacting with the database.
- Utilizes the Unit of Work pattern for managing transactions.
- Implements a simple generic implementation above Dapper, which is necessary for facilitating unit tests for the repositories.

## Unit Tests
- Contains test cases to verify the functionality and behavior of the project.
- Uses XUNIT as the testing framework and FluentAssertions for expressive assertions.
- Follows the AAA Pattern (Arrange, Act, Assert) for organizing test code.

