# CleanArch-Docker

## Clonar proyecto

  - Se debe ejecutar los siguientes comandos en cmd o powershell
    
```bash
git clone https://github.com/jvaldezn/CleanArch.git
cd CleanArch
```

## Uso

- Las migraciones estan configuradas de forma automatica, no se necesita hacer un Update-Database manualmente
- La app esta dockerizada se requiere docker desktop para usar el proyecto o sino iniciar como https
  

## Acceder a API

 - El default user para generar un token, se debera consultar el metodo /login, swagger se encuentra configurado en api para consultas a los endpoins asi como para setear bearer tokens

```bash
username: admin
password: Admin123!
```
## Unit Test

- Solo se agrego algunos unit test como ejemplo mostrando como usar Mock y EntityFramework InMemoryDatabase, se deberia probar TODOS los metodos, debido a esto el score en el code coverage report sera bajo

## Code Coverage

 - Ejecutar los siguientes commandos para crear el code coverage report reemplazar el guid y ruta

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet tool install --global coverlet.console

dotnet test --collect:"XPlat Code Coverage"

reportgenerator -reports:"C:\GIT\CleanArch-Docker\Application.Tests\TestResults\3b86fa8b-96e6-4cee-8dd0-2fc5ecdb4ab3\coverage.cobertura.xml" -targetdir:"C:\GIT\CleanArch-Docker\coveragereport_Application" -reporttypes:Html

reportgenerator -reports:"C:\GIT\CleanArch-Docker\Infrastructure.Tests\TestResults\51373deb-27ef-4316-ba92-ba97ce747358\coverage.cobertura.xml" -targetdir:"C:\GIT\CleanArch-Docker\coveragereport_Infrastructure" -reporttypes:Html
```

## RabbitMQ

 - Instalar DockerDesktop y ejecutar el siguiente commando en la consola
 
 ```bash
 docker run -d --hostname rmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
 ```
 
 - Accesos RabbitMQ Panel; Usuario: guest / Password: guest