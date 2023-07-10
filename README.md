# Prerequisites

Before running the solution, make sure you have the following:

1. Visual Studio 2022 (Any version)
2. SQL Server 2012 or later.

# Steps to Run Locally with Visual Studio

1. Navigate to the "scripts" folder located in the root directory and execute the database creation script.
2. The solution is running with local certificates. If you intend to test it with Postman, please note that you need to disable the "Enable SSL certificate verification" option.

   ![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/6380b601-ca22-42a5-9b2e-e4efc779cc1b)
   
4. Open the solution in Visual Studio, and the first thing you need to do is set multiple startup projects. Select Exercise.API, Exercise.UI, and Exercise.Identity projects.

    ![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/39f84a7c-25a9-4546-a120-021337e36a4b)
   
6. Proceed to run the project.
7. Once it is executed, three browser tabs will open on your machine. If you have reached this point without any issues, you can start exploring the solution.

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

![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/49c39f28-eb0b-484f-ba8c-428d1ba16929)

![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/e75c8bce-abd6-4eb8-bd7f-1a9ab17e31c2)

![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/8d4bb34c-9e6b-4774-86ea-4940fbcba8d8)

![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/67c94f98-e391-4819-aeb4-e962b3a76e47)

6. Finally, a UI with Blazor was added, where a service is consumed. It is important to note that, for demonstration purposes, a public endpoint was enabled using the [AllowAnonymous] attribute

![image](https://github.com/eflorespalma/dotnet-exercise-solution/assets/2238801/aa168e68-3b5c-4e1d-a757-6d42cdf7ab0f)

# Solution Components

1. Identity.API: This solution covers the authorization requirement for resources. It generates an access token if the provided user credentials are correct. Please note that this solution is for demo purposes. For production environments, more robust identity providers such as Identity Server, OKTA, Azure AD, etc., should be used.
2. Exercise.API: This project hosts the endpoints created for the exercise.
3. Exercise.BizLogic: This project contains the business rules and validations for the exercise.
4. Exercise.Repository: This project manages database operations using the Dapper library. It also implements the Unit of Work pattern to handle transactions more effectively and avoid direct database operations repetition.
5. Exercise.Domain: This project contains all the domain classes, in this case, User and Product.
6. Exercise.Test: This project covers all the neccesary unit test for the different layers.
7. Exercise.Database: This roject allows us to maintain change control of the different objects of the database
