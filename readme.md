# DigitalMenu

## Requirements to run the application
* MongoDB installed locally or on a server

## How to build and run
1. Clone the repository
2. Navigate to the ```{REPO-PATH}\src\DigitalMenu``` and execute ```dotnet build```
3. Check the ```MongoDbSettings``` in the ```appsettings.json```-file and change it if necessary
4. Run the project with ```dotnet run```

## Triggering endpoints
You can trigger the api endpoints on swagger ui. Swagger ui can be reached via http://localhost:5000/swagger

## HealthCheck
The HealthCheck can be reached via http://localhost:5000/hc