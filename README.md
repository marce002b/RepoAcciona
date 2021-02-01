# RepoAcciona

DIA 1 lunes25 de enero 2021

Liberar un laptop. instalacion VS2019 comunity,  instalacion y testeo de Net CORE 2.1 , creacion de solucion basica , instalacion via nuget de EF Core version compatible
videos varios net core api rest. pruebas

Instalacion POSTMAN

DIA 2  martes 26

Tutorials: net core api rest, seguridad JWT, revision API de Datos.GOB.AR creacion repositorio GIT enlace con Vstudio, push y pull
Controller verbos GET POST PUT y DELETE ApiController, estructura de la applicacion REST API creacion SLN
 
DIA 3 miercoles 27

Mantencion y prueba de repositorio GIT enlazado a vs2019 y al proyecto, sincro entre los
mismos solucion y GIT. readme file problemas de sincronizacion, no actualiza desde Vs2019, to be solved. 
Creacion de API REST basico de values y test controller basico en localhost
Login basico (para probar el funcionmiento en localhost de la API simplemente
se usa un controller de prueba para el login, no es lo que piden pero sirve para
ver si estaria andando. el login real se hara luego via token.

NOTAS: usaria Core Net 2.1 o superior via un Visual Studio 2019 comunity. Respecto a login, usare la generacion  de tokens JWT, AccountController y esquemas de autenticacion de Core, ademas de  la simulacion de login pedida, que la tendre en otro controller...
Entiendo que la API a su vez consume otra API :  de datos.gob.ar con lo que estarian presentes en el codigo tanto el consumo como la generacion de datos que sirve la api misma no?
Me es necesario usar  persistencia (para usuario, login ,etc) , y en este caso lo estaria manejando con Sql local y EntityFramework ya que es lo mas practico para mi, esta ok?
Respecto a la inyeccion de dependencias tengo su uso en el codigo y mi idea de lo que es en general pero no tengo un desarrollo del tema en si. Solo esta usado a nivel codigo.
Las pruebas unitarias, Logs (BD y File) lo dejo para el final ya que prefiero avanzar mas en firme, aunque el log veo de poder usarlo de entrada.

creacion del ApplicationDbContext con 3 tablas paises municipios y provincias luego agregaremos usuarios y las tablas especiales para la seguridad jwt

DIA 4 Jueves 28

Analisis del codigo en startup y agrego el servicio para agregar EF para poder inyectar nuestro ApplicationDbContext a traves de inyeccion de dependencias en nuestros controllers
Creacion de clases provincia, pais, municipios a modo de ejemplo, creacion y test del context, creacion rutas parciales de api/provincia api/pais y sus respectivos {id}... base en memoria mediante UseInMemoryDatabase , creacion de data dummy en startup aun en memoria, anulacion de null en datos hijos, usando StringLength acotando campos, analizando ModelState valido, condicional para POST al recibir data de nuevas provincias, recibir un 201 content created en POSTMAN o 404 not found
Creacion de clases datos.gob.ar desde json para enriquecer la clase provincia existente, en realidad solo agregue centroide y derive a una nueva clases las coordenadas que estaban en provincia.

PARA HACER DESDE MANANA:
Inicio de estudio de Consumir API en Net Core ver si es o no igual a .NET Framework, tener en cuanta Core 2.1 y no mezclar referencias.... al mismo tiempo ver el tema del login JWT y pasar toda la data desde memoria a SQL local


DIA 5 Viernes 29


Avances con base de datos persistente en local sql, ya se persisten los datos y las tablas han sidro creadas , y tambien la tabla de usuarios para el login. se creo ApplicationDbContext y UserInfo para tener los datos de usuario login. hubo problemas con el string de conexion se seteo a Server=(localdb)\\mssqllocaldb
Se corrigieron los problemas al generar comandos de consola de PM, add-migration y update-database y se crean los datos ok con las identity en off


DIA 6 Lunes 1


Persistencia,token , login de usuarios registrados en estructura de tablas ej AspNetUsers etc , controllers con esquemas autenticacion  via net core   [Authorize(AuthenticationSchemes soporado via  JwtBearerDefaults
