# GeoLoc

GeoLoc is simple api made for querying some short info from ip address.
Dns services, external provders collect data from ip adress such as country, region, city, lat, lon etc, this data can be utilized later on. This Api has no specific purpose.

## Running

### Prerequisites

Make sure the following tools are installed on your system:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

### Setting Up the Database

1. Start the Docker container with SQL Server by running the following command:
    ```bash
    docker-compose up -d
    ```
2. Wait for the container to start.

### Running the Application

1. Navigate to the project directory:
    ```bash
    cd YourProjectDirectory
    ```
2. Run the application using the dotnet CLI:
    ```bash
    dotnet run
    ```

The application will be accessible at `http://localhost:5046`.

## Configuration

### Environment Settings

To customize application settings, refer to the `appsettings.json` file.

### Database Configuration

To change the database connection settings, edit the `ConnectionStrings` section in the `appsettings.json` file.

### Ipstack Api

To query info about specified ip ipstack is used. Ipstack is external api, with access to thousands of ip addresses. To query you need secret key. Provide it in either `appsettings.json` or `appsettings.Development.json`.
  "ExternalApiIpstackOptions": {
    "ApiKey": "XXXXX",  `<<--- replace it here`
    "ApiPath": "XXXXX"   `<<--- replace it here`
  }
