[![LinkedIn][linkedin-shield-zajaczkowski]][linkedin-url-zajaczkowski] [![LinkedIn][linkedin-shield-kuniszewski]][linkedin-url-kuniszewski] 

# kiedy-kolos-backend

## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Deployment](#deployment)
	* [Docker](#docker)
	* [Docker-compose](#docker-compose)
* [Usage](#usage)

## About the project

 ASP .NET Core REST API with API key based authorization. Entity Framework Core was used for infrastructure persistence layer, with InMemory or MySQL database. Application logic is implemented using CQRS and MediatR, with authorization as pipeline behavior. Project structure adheres to Clean Architecture design. 
 Swagger is used for API documentation.
 Application can be deployed with provided Dockerfile or docker-compose configuration.

### Built with
* ASP .NET Core 5
* Entity Framework Core
* AutoMapper
* MediatR
* Docker

## Getting Started
These instructions will get you a copy of the project up and running on your local machine, for development and testing purposes. See deployment for notes on how to deploy the project using Docker containerization.

### Prerequisites
- .NET 5.0
	For installation see: [https://dotnet.microsoft.com/download/dotnet/5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### Installation

#### Clone repository
```
git clone https://github.com/krzysztofzajaczkowski/kiedy-kolos-backend
```
#### Visual Studio or Rider
Open .sln file and run project
#### Visual Studio Code
See: [https://code.visualstudio.com/docs/languages/csharp](https://code.visualstudio.com/docs/languages/csharp)
#### Other
```
cd KiedyKolos/Api/KiedyKolos.Api/
dotnet run
```

## Deployment
Docker is used for application deployment.

### Docker
In general solution directory, where Dockerfile is placed, build image with
```
docker build -f Dockerfile -t kiedy-kolos-backend .
```
And run container using
```
docker run -d -p 8080:80 8081:443 -e USE_SWAGGER=1 -e USE_IN_MEMORY=1 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_Kestrel__Certificates__Default__Password=s49z2a49n9541e -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -- kiedy-kolos-backend kiedy-kolos-backend_container
```

### Docker-compose
In general solution directory, where docker-compose.yml is placed, run composition using
```
docker-compose up -d
```

## Usage
Interactive API documentation is built with Swagger, available at
```
https://localhost:8081/swagger/index.html
```

[linkedin-shield-zajaczkowski]: https://img.shields.io/badge/LinkedIn-Zajaczkowski-blue?logo=linkedin
[linkedin-shield-kuniszewski]: https://img.shields.io/badge/LinkedIn-Kuniszewski-blue?logo=linkedin
[linkedin-url-zajaczkowski]: https://www.linkedin.com/in/krzysztof-m-zajaczkowski/
[linkedin-url-kuniszewski]: https://www.linkedin.com/in/jakub-kuniszewski/