# APIUniversidades
Una API donde se gestionara la informacion de las universidades en Bolivia, Cochabamba.


Te voy a ir diciendo todo loq eu hice en mi proyecto y quiero que me crees Documentacion en formato README para eso

Dividi mi proyecto entres partes 
Backend
frontend
infrastructure


Las tecnologias con las que voy a ir trabajando alrededor de todo el proyecto son 
C#, JavaScript, React, TailwindCSS, Docker, MySQL

Y para el manjeo de los endpoints usare Swagger

Entonces el primer commit que hice fue ya subiendo todo lo que es el backend y el infrastructure entonces ire definiendo uno por uno

Mysql:
Primero teniamos que definir nuestras tablas que ibamos a trabajar dentro del proyecto, como era un API de universidades en cochabamba mi init.sql tenia el siguiente formato

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


-- TRUNCATE ALL TABLES
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

El truncate es para ir limpiando los datos que tenian las tablas de la base de datos cada vez que se iniciaba el proeycto, pero tambien se puede eliminar para manejar persistencia

Tambien dentro de la parte cree los .CSV para lo que son cada tabla de nuestros poryectos destacando principalmente que se tendrian
Universidades: 18 
Facultades:103 
Carreras: 50
Becas: 91

Backend:
En este manej un proyecto de carpetas sencillos
- Entities: Las entidades que estaremos manejando para nuestro proyecto estas se basan mediante las tablas definidas en nuestra base de datos, manejando los atributos de cada una de estas (namepsace  Backend.Entities)

- DTOs: Luego en los DTOs para no exponer mis entidades en los controladores y dar una capa mas a mi manejo de datos los dividi en dos
	- WithID: Que son DTOs que manejaban la misma estructura que una entidad normal (namespace Backend.DTOs.WithID;)
	- WithoutID: Que son DTOs que no contenian el atributo del ID que cada uno de las entidades tenian, estos eran utilizados principalmente para la creacion de nuevas entidades en los servicios (namespace Backend.DTOs.WithoutID;)
	
- Mappers: Luego tenia los Mappers para lo cual use la libreria de AutoMapper para manejar mejor el proceso de transformacion y conversion entre los Datos que estaba manejando en el proyecto los DTOs y entidades, estos los dividi en 3 mappers para cada uno de las entidades que teniamos en el proyecto:
- CreateMap<Dto, Entidad>().ReverseMap(); 
- CreateMap<(PostDTO, int), Entidads>();
- CreateMap<PostDTO, Entidad>(); 
De esa manera curia todos los casos necesarios que usaba en los servicios y controladores del proecto

Ahora se viene la parte medio compleja del proyecto, la conexion del backend con la base de datos para eso utilice una carpeta llamada DB, en esta maneja otras tres carpetas que iremos explicando una por una: 
	- DAOs: Aqui iremos definiendo los comandos que vamos a utilizar en MySQL para poder obtener datos de nuestra base dedatos estos se dibidian en dos 
		- Abstract: En el abstract se definieron la clase abstracta SingleDAO y las interfaces IDAO y ISingleDAO para definir las definiciones de los metodos que vamos a utulizar que eran (IDAO: Create, ReadAll, Update) y (ISngleDAO(Read, Delete) que ISingleDAO heredaba de IDAO entonces todos esos metodos eran para manejarse para cada uno de nuestras entidades, luego teniamos la clase abstracta SingleDAO que era la que utilizaba estos metodos para ir definiendo las llamadas a la base de datos, entonces los metodos eran Execute tal metodo que seran utilziados en los Concretes, Tambien teniamos en el Abstract una carpeta extra SingleDAO que contenian una interfaz para cada una de las entidades IEntityDAO por si alguna de esas necesitaba algun metodo extra aparte de los ya creados en las otras interfaces y la clase abstracta
		- Concrete. Y aqui paso la magia aqui se definio los metodos que iban a ejecutar la clase SingleDAO por cada entidade que manejabamos, en los cuales mostrare un ekemplo con las clase de la entidad Becas:
		
		using System.Text;
using Backend.DTOs.WithID;
using Backend.Entities;
using DB.SingleDAO;
using Microsoft.Extensions.Primitives;

namespace DB;

public sealed class BecaDAO : SingleDAO<Becas>, IBecaDAO
{
    public BecaDAO()
    {
        _tableName = "Becas";
    }

    private protected override Becas MapReaderToEntity()
    {
        _entity = new Becas()
        {
            BecaID = _mySqlReader!.GetInt32(0),
            Nombre = _mySqlReader!.GetString(1),
            Descripcion = _mySqlReader!.GetString(2),
            Criterios = _mySqlReader!.GetString(3),
            UniversidadID = _mySqlReader!.GetInt32(4)
        };
        _mySqlReader.Close();
        return _entity;
    }

    private protected override List<Becas> MapReaderToEntitiesList()
    {
        _entitiesList = new List<Becas>();
        while (_mySqlReader!.Read())
        {
            _entity = new Becas()
            {
                BecaID = _mySqlReader!.GetInt32(0),
                Nombre = _mySqlReader!.GetString(1),
                Descripcion = _mySqlReader!.GetString(2),
                Criterios = _mySqlReader!.GetString(3),
                UniversidadID = _mySqlReader!.GetInt32(4)
            };
            _entitiesList.Add(_entity);
        }

        _mySqlReader.Close();
        return _entitiesList;
    }

    private protected override StringBuilder CreateCommandIntoStringBuilder(Becas becas)
    {
        string becasIdC = becas.BecaID.ToString();
        string becasNombreC = becas.Nombre;
        string becasDescripcionC = becas.Descripcion;
        string becasCriteriosC = becas.Criterios;
        string becasUniversidadC = becas.UniversidadID.ToString();

        _sb = new StringBuilder();
        _sb.AppendLine("INSERT INTO ").Append(_tableName).Append("(Id, Nombre, Descripcion, Criterios, UniversidadesID) ")
            .AppendLine("VALUES (").Append(becasIdC).Append(",'")
            .Append(becasNombreC).Append("','")
            .Append(becasDescripcionC).Append("','")
            .Append(becasCriteriosC).Append("', ")
            .Append(becasUniversidadC).Append(");");
        
        return _sb;
    }

    private protected override StringBuilder UpdateCommandIntoStringBuilder(Becas becas)
    {
        string becasIdC = becas.BecaID.ToString();
        string becasNombreC = becas.Nombre;
        string becasDescripcionC = becas.Descripcion;
        string becasCriteriosC = becas.Criterios;
        string becasUniversidadC = becas.UniversidadID.ToString();
    
        _sb = new StringBuilder();
        _sb.AppendLine("UPDATE ").Append(_tableName)
            .Append(" SET Nombre = '").Append(becasNombreC).Append("', ")
            .Append("Descripcion = '").Append(becasDescripcionC).Append("', ")
            .Append("Criterios = '").Append(becasCriteriosC).Append("', ")
            .Append("UniversidadesID = ").Append(becasUniversidadC).Append(" ") 
            .Append("WHERE Id = ").Append(becasIdC).Append(";");
    
        return _sb;
    }


}

//AQUI EXPLICA CADA UNO DE LOS METODOS QUE ESTAN SIENDO EJECUTADOS PARA QUE SE PUEDA ENTENDER BIEN
	- Injectors: Esta pase era para ejecutar e ingresar los comandos para poder inyectar los datos que teniamos en los CSVs definidos previamente para tener datos iniciales en la base de datos
		- Abstract: Aqui definimos unicamente dos cosas el DataInjector y su interfaz, esto principalemnte para tener el comando de ejecutar el comando SQL mediante ell metodo InjectData
		- Concrete: Es una clase por cada entidad que utilizan el metodo InjectDara para ingresar un dato aquij hay un ejmplo de la estructura de como se maneja por cada entidad
namespace DB;

public class BecaDataInjector : DataInjector
{
    public BecaDataInjector()
    {
        _injectionCommand = @"LOAD DATA INFILE '/var/lib/mysql-files/Becas.csv'" +
                            " INTO TABLE Becas" +
                            " FIELDS TERMINATED BY ','" +
                            " LINES TERMINATED BY '\n'" +
                            " IGNORE 1 LINES" +
                            " (Id, Nombre, Descripcion, Criterios, UniversidadesID)" +
                            " SET Criterios = TRIM(TRAILING '\n' FROM Criterios)";
        Console.WriteLine(_injectionCommand);
    }
}
//EXPLICA QUE HACE EL METOOD

	-Utils: Y por ultimo tenemos las partes mas importante que son el DBConnector y el DBInjector que manejan, el connector se encarga de leer el dbsettings.json con las credenciales que tiene para ingresar la base de datos y obtener el URL de conexion mismo para ejecutar todos los comandos que estamos mandando con nuestros DAOs e inyectores, y el DBInjector es el que instanca los EntidadDataInejctor y el define el metodo TrucateAllTables que se ejecutara en el program al inicio del programa 
	
- Services: AL fin entramos a la parte de definir los servicios y para esto se subdivide en una carpeta 
	- Interfaces: Que aqui unicamente son interfaces que definenn los emtodos que cada servicio utilizara 
Tenemos servicios por cada entidad que manejmos y los servicios que manejamos son
public List<DTOBecaDTO> GetAll();
public DTO? GetById(int Id);
public DTO? Create(PostDTO _);
public DTO? Update(DTO _);
Utilizan un llamado de los DAOs y el AutoMapper definiendolos cada uno en su constructor, ejemplo:
 private readonly IBecaDAO _becaDao;
    private readonly IMapper _mapper;

    public BecaService(IBecaDAO becaDao, IMapper mapper)
    {
        _becaDao = becaDao;
        _mapper = mapper;
    }
    

//Explica la propiedad

- Controllers: Por ultimo tenemos los controladores que manejan una estructura sencilla para ir definiendo todo
[ApiController]
[Route("[controller]")]
Y heredando de ControllerBase, luego utilizamos en el constructor la instancia de IService de la entidad especifica y creamos los controladores en base a estas 


    [HttpGet]
    public ActionResult<List<DTO>> GetAll()
    [HttpGet("{id}")]
    public ActionResult<dto> GetById(int id)
    [HttpPost]
    public ActionResult<dto> Create(PostDTO _)
    [HttpPut("{id}")]
    public ActionResult<DTO> Update(int id, [FromBody] DTO _)
    
 ESO SERIA TODO EL BACKEND AHORA TOCA LA DOCKERIZACION
 
 infrastructure: Esta es la crpeta donde ocurre toda la magia, primero nos definimos nuestras variables de entorno con nuestro .env
 CONTEXT=/home/fundacion/Pictures/Vacaciones/Login&Register/APIUniversidades/Intento1
BD_PASSWORD=...
DATABASE=universidades_db
Aqui definiendo nuestra base de datos que bamos a usar y la contraseña de la misma con la cual entraremos, y el contexto de nuestro proyecto con el comando pwd
Ante sde entrar al .yml dentro de infrastructure tenemos la carpeta del Backend definida como Universidades, esta tiene una subcarpeta llamada BD donde estan los CSVs y el init.sql, pero la carpeta Universidades tambien tiene el dbsettings.json en donde definimos nuestro ConnectionURL con el nombre del server que manejamos en el compose.yml, el port donde correra como es mysql corre en el puerto 3306, la contraseña de la base de datos, la base de datos donde se guardara todo y tambien Allow User Variables=True, eso es por parte del dbsettings.json tambien existe un Dockerfile que automatiza el proceso de ejecutar el backend cada vez que se levanta el compose.yml 
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

EXPLICA EL DOCKERFILE

Y por ultimo el docker-compose.yml
services:
  # Backend
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


  # Base de datos
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


.
├── Backend/
│   ├── Controllers/
│   ├── DTOs/
│   ├── DB/
│   ├── Entities/
│   ├── Mappers/
│   └── Services/
├── Frontend/
└── Infrastructure/
    ├── Universidades/
    │   ├── BD/
    │   ├── Dockerfile
    │   └── dbsettings.json
    ├── .env
    └── docker-compose.yml

Se definen los dos servicios que estamos manejando el backend y la base de datos, se les asigna a cada uno un contenedor especifico, y se hace que el servicio del backend sea dependiente del servicio del mysql si el mysql falla no se ejecuta el servicio del backend, para automatizar el backend y ejecutarlo con un docker compose p --build se hace el llamado al Dockerfile y lo eejcuta automaticamente en el puerto 8080, tambien se define el llamado al dbsettings.json al init.sql y a los CSVs cada uno guardandolos en ciertos archivos de cada contenedor, y haciendo un helathcheck a la base de datos cada 2 segundos para verificar que todo este yendo bien 

LO QUE FALTA
Se avanzara con el proceso de la parte del frontend que se avanzo un poco en este link:
https://www.figma.com/proto/78kX36iHqfrJIOtxOAM0N6/Universidades-Bolivia%2FCochabamba?node-id=8-21&p=f&t=YagyBWjbI9mVylVa-1&scaling=min-zoom&content-scaling=fixed&page-id=8%3A19&starting-point-node-id=8%3A21



