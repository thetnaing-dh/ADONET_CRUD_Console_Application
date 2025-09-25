# ADONET CRUD Console Application
A simple console-based CRUD (Create, Read, Update, Delete) application built with C# and ADO.NET for managing blog entries in a SQL Server database.

## Features
* Read Blogs: Display all active blog entries from the database
* Create Blog: Add new blog entries with title, author, and content
* Update Blog: Modify existing blog entries with optional field preservation
* Delete Blog: Soft delete blog entries (sets DeleteFlag to 1)
* User-Friendly Interface: Simple console menu for easy navigation

## Prerequisites
* .NET Framework (compatible with .NET Core/.NET 5+)
* SQL Server (LocalDB or full SQL Server instance)
* Database: TrainingBatch5 with table Tbl_Blog

## Database Setup
Create Database Table

        sql
        
        CREATE DATABASE TrainingBatch5;
        GO
        
        USE TrainingBatch5;
        GO
        
        CREATE TABLE Tbl_Blog (
            BlogId INT IDENTITY(1,1) PRIMARY KEY,
            BlogTitle NVARCHAR(255) NOT NULL,
            BlogAuthor NVARCHAR(100) NOT NULL,
            BlogContent NVARCHAR(MAX) NOT NULL,
            DeleteFlag BIT DEFAULT 0 NOT NULL
        );
Connection String Configuration
The application uses the following connection string (modify in code as needed):

        csharp
        string _connectionString = "Server=.;Database=TrainingBatch5;User Id=sa;Password=23032106;TrustServerCertificate=True;";
Note: Update the connection string with your actual SQL Server credentials and instance details.

## Installation & Usage
1. Clone or Download the source code
2. Compile the application using Visual Studio or .NET CLI:

        bash
        dotnet build

3. Run the application:

        bash
        dotnet run

4. Follow the menu prompts:

* Press R to Read all blogs
* Press C to Create a new blog
* Press U to Update an existing blog
* Press D to Delete a blog
* Press any other key to Exit

## Application Structure
### Main Components
* ADOHelper Class: Contains all database operations
  * ReadData(): Fetches and displays all active blogs
  * InsertData(): Adds a new blog entry
  * UpdateData(): Modifies an existing blog entry
  * DeleteData(): Soft deletes a blog entry

## Key Features
* Parameterized Queries: Prevents SQL injection attacks
* Soft Delete: Uses DeleteFlag instead of physical deletion
* Error Handling: Validates user input and handles exceptions
* Connection Management: Properly opens and closes database connections
* Data Validation: Checks for existing records before update/delete operations

## Code Overview
### Database Connection
        csharp
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        // Database operations...
        connection.Close();
### Parameterized Queries Example
        csharp
        string sqlQuery = @"INSERT INTO [dbo].[Tbl_Blog] 
                           ([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag])
                           VALUES (@Title, @Author, @Content, 0)";
        
        SqlCommand command = new SqlCommand(sqlQuery, connection);
        command.Parameters.AddWithValue("@Title", title);
        command.Parameters.AddWithValue("@Author", author);
        command.Parameters.AddWithValue("@Content", content);

## Security Notes
* Uses parameterized queries to prevent SQL injection
* Connection string contains sensitive credentials (consider using secure configuration in production)
* Soft delete approach maintains data integrity

## Limitations & Improvements
* Current Limitations:
  * Connection string is hard-coded
  * No input validation for special characters
  * Basic error handling
  * Console-based UI only
* Potential Improvements:
  * Use app.config for connection strings
  * Add input sanitization
  * Implement logging
  * Add pagination for large datasets
  * Create a web or desktop interface

## Contributing
Feel free to fork this repository and submit pull requests for any improvements.

## License
This project is open source and available under the MIT License.
