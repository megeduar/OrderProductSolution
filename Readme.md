# Order&Products Example

Aplicación de Órdenes y Productos utilizando  ASP.NET Core 5 Web API,  Entity Framework Core 5 y Mysql.

Estructura del proyecto:

* Dominio: Representa las entidades del Negocio.
* Persistencia: Configura las tablas (Índices y Tipos de datos de las columnas),  relaciones entre tablas. Configuración de la fuente de datos (MySQL) con Pomelo.EntityFrameworkCore.Mysql y el DBContext.
* Infraestructura: Gestión de la seguridad, define la clase de User que hereda de IdentityUser e incorpora dos propiedades personalizadas. Definición de Roles, Autenticacion OAuth y JWT.
* Aplicación: Implementación de las lógica del negocio. Usando MediatR en para solicitud desde la Api, FluentValidation para evaluar los datos entrados por el cliente y Mapper.
* Web Api: Expone los End-Points y la configuración la Api.


### Prerequisitos
Herramientas necesarias.

* [Visual Studio Code o Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.9)
* [.NET Core SDK 5]
* [MySQL] (vesion >= 5.0)

Paquetes (Importantes en el proyecto) Nugget
* Pomelo.EntityFrameworkCore.MySql (5.0.0-alpha.2)
* Microsoft.EntityFrameworkCore (5.0.0)
* Newtonsoft.Json (12.0.3)
* FluentValidation.AspNetCore (9.5.2)
* MediatR.Extensions.Microsoft.DependencyInjection (9.0.0)
* AutoMapper.Extensions.Microsoft.DependencyInjection (8.1.1)

### Setup
Follow these steps to get your development environment set up:

  1. Clonar el repositorio
  2. En el directorio principal, restaure los paquetes requeridos:
      ```
     dotnet restore
     ```
  3. Contruir la solucion con el comando:
     ```
     dotnet build

     ```

     * NOTA: Para general el esquema de base de datos desde el proyecto de Persistencia y Infrastructura ejecute el siguiente comando:
     ```
     Desde Persistencia (Project)
     Add-Migration OrderDB 
     update-database

     Desde Infrastructura (Project)
     Add-Migration -context ApplicationDbContext
     update-database

  4. El proyeto de inicio esta en el diretorio `\Src\Api` directorio:
     ```
	 dotnet run
	 ```
  5. Launch [https://localhost:44328/swagger/index.html](http://localhost:39588/swagger/index.html) url de la Web UI en el navegador.
  

## Tecnologías 
* .NET Core 5
* ASP.NET Core 5
* Entity Framework Core 5
* MySQL
