# Proyecto API Universidades Cochabamba

Este proyecto está dividido en tres partes principales:

1. **Backend**
2. **Frontend**
3. **Infrastructure**

El objetivo del proyecto es desarrollar una API para gestionar información sobre universidades en Cochabamba. 
Las tecnologías utilizadas son: **C#**, **JavaScript**, **React**, **TailwindCSS**, **Docker**, y **MySQL**.

---

## Backend

El backend está implementado en **C#** y organiza sus funcionalidades en las siguientes carpetas principales:

### Base de Datos

La base de datos utiliza MySQL y está diseñada con las siguientes tablas:

```sql
CREATE TABLE IF NOT EXISTS Universidades (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Descripcion TEXT NOT NULL,
    Tipo VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Facultades (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Descricpion TEXT NOT NULL,
    UniversidadID INT NOT NULL,
    FOREIGN KEY (UniversidadID) REFERENCES Universidades(Id)
);

CREATE TABLE IF NOT EXISTS Carreras (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Duracion INT NOT NULL,
    FacultadID INT NOT NULL,
    FOREIGN KEY (FacultadID) REFERENCES Facultades(Id)
);

CREATE TABLE IF NOT EXISTS Becas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(255) NOT NULL,
    Descripcion TEXT NOT NULL,
    Criterios TEXT NOT NULL,
    UniversidadesID INT NOT NULL,
    FOREIGN KEY (UniversidadesID) REFERENCES Universidades(Id)
);
```

Se incluye un procedimiento para limpiar las tablas:

```sql
DELIMITER //
CREATE PROCEDURE TruncateAllTables()
BEGIN
    SET FOREIGN_KEY_CHECKS = 0;
    TRUNCATE TABLE Universidades;
    TRUNCATE TABLE Facultades;
    TRUNCATE TABLE Carreras;
    TRUNCATE TABLE Becas;
    SET FOREIGN_KEY_CHECKS = 1;
END //
DELIMITER ;
```

Los datos iniciales se almacenan en archivos `.csv` con los siguientes volúmenes:
- Universidades: 18
- Facultades: 103
- Carreras: 50
- Becas: 91

### Estructura del Proyecto

- **Entities**: Define las entidades según las tablas de la base de datos.
- **DTOs**: Utilizadas para transferir datos sin exponer las entidades:
  - `WithID`: Incluyen el atributo `ID`.
  - `WithoutID`: Excluyen el atributo `ID`, usadas para creación.
- **Mappers**: Configurados con AutoMapper para convertir entre DTOs y entidades.
- **DB**: Maneja la conexión y consultas a la base de datos:
  - **DAOs**: Definen los métodos de consulta.
    - `Abstract`: Clases e interfaces base.
    - `Concrete`: Implementaciones específicas por entidad.
  - **Injectors**: Inyectan datos iniciales desde archivos `.csv`.
  - **Utils**: Configuran la conexión (DBConnector) y manejo de datos (DBInjector).
- **Services**: Contienen la lógica del negocio y llamadas a DAOs.
- **Controllers**: Implementan los endpoints de la API utilizando los servicios.

### Swagger

Se utiliza **Swagger** para documentar y probar los endpoints.

---

## Infrastructure

### Configuración

Se utiliza Docker para contenerizar el backend y la base de datos. El archivo `.env` contiene las variables de entorno:

```env
CONTEXT=/path/to/project
BD_PASSWORD=yourpassword
DATABASE=universidades_db
```

### Dockerfile

El `Dockerfile` para el backend:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /webapp

COPY ./Backend/*.csproj ./
RUN dotnet restore

COPY ./Backend .
COPY ./infrastructure/Universidades/dbsettings.json .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /webapp
COPY --from=build /webapp/out .
ENTRYPOINT [ "dotnet","Backend.dll" ]
```

### Docker Compose

El archivo `docker-compose.yml` define los servicios:

```yaml
services:
  backend-services:
    container_name: backend
    build:
      context: ${CONTEXT}
      dockerfile: ./infrastructure/Universidades/Dockerfile
    environment:
      ASPNETCORE_HTTP_PORTS: "80"
    depends_on:
      mysql:
        condition: service_healthy
    ports:
      - "8080:80"
    volumes:
      - ${CONTEXT}/infrastructure/Universidades/dbsettings.json:/webapp/dbsettings.json

  mysql:
    image: mysql:9
    container_name: mysql_container
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: ${BD_PASSWORD}
      MYSQL_DATABASE: ${DATABASE}
    volumes:
      - ${CONTEXT}/infrastructure/Universidades/BD:/docker-entrypoint-initdb.d
      - ${CONTEXT}/infrastructure/Universidades/BD/Data:/var/lib/mysql-files/
    healthcheck:
      test: ["CMD-SHELL", "mysql -u root -p${BD_PASSWORD} --execute 'SELECT @@GLOBAL.version;'"]
      interval: 2s
      retries: 60
```

---

## Frontend (En Proceso)

El diseño inicial del frontend está en desarrollo en [Figma](https://www.figma.com/proto/78kX36iHqfrJIOtxOAM0N6/Universidades-Bolivia%2FCochabamba?node-id=8-21&p=f&t=YagyBWjbI9mVylVa-1&scaling=min-zoom&content-scaling=fixed&page-id=8%3A19&starting-point-node-id=8%3A21).

Próximamente se implementará utilizando **React** y **TailwindCSS**.


