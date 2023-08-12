# MovieStoreAPI

MovieStoreAPI is a RESTful Web API built using ASP.NET Core that allows users to manage movie data, genres, and authentication.

## Features

- User authentication using Identity framework
- CRUD operations for movies and genres
- Retrieve genres associated with a movie
- Error handling and response formatting for API

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- Microsoft Identity
- SQL Server (for database)
- Swagger for API documentation

## Getting Started

1. Clone this repository to your local machine.
2. Open the solution in your preferred IDE (Visual Studio, Visual Studio Code, etc.).

### Prerequisites

- .NET 5 SDK
- SQL Server (or change the database provider in `appsettings.json`)

### Database Setup

1. Update the connection string in `appsettings.json` to point to your SQL Server instance.

```json
"ConnectionStrings": {
    "DbType": "MsSql",
    "MsSqlConnection": "Server=localhost\\SQLEXPRESS;Database=movieStoreDb;Trusted_Connection=True;Encrypt=False;"
}
```

2. Open a terminal and navigate to the project directory.

```bash
cd MovieStoreAPI
```

3. Run the following command to apply the database migrations.

```bash
dotnet ef database update --project "./MovieStoreAPI.Data" --startup-project "./MovieStoreAPI.Web"
```

### Running the Application

1. Build and run the application.

```bash
dotnet run --project MovieStoreAPI.Web
```

2. The API will be accessible at `https://localhost:5001/api`.

## API Documentation

Swagger is used to generate API documentation. You can access the Swagger UI by navigating to `/swagger` in your browser after running the application.

## Authentication

User registration and login endpoints are provided for authentication. Use the `/register` and `/login` endpoints to create an account and obtain a JWT token for authentication.

## Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue on the GitHub repository.

## License

This project is licensed under the [MIT License](LICENSE).

---

Feel free to add more information specific to your project, such as additional features, endpoints, deployment instructions, and any other relevant details.
