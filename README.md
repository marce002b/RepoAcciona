# RepoAcciona

DIA 1

instalacion VS2019 instalacion Net CORE 2.1instalacion via nuget de EF Core version compatible
videos 


Instalacion POSTMAN

DIA 2

Tutorials:

DIA 3

Creacion de repositorio GIT enlazado a vs2019 y al proyecto, sincro entre los
mismos solucion y GIT. readme file problemas de sincronizacion. 

Login basico para probar el funcionmiento en localhost de la API simplemente
se usa un controller de prueba para el login, no es lo que piden pero sirve para
ver si estaria andando. el login real se hara luego via token.

NOTAS: usaria Core Net 2.1 o superior via un Visual Studio 2019 comunity. Respecto a login, usare la generacion  de tokens JWT, AccountController y esquemas de autenticacion de Core, ademas de  la simulacion de login pedida, que la tendre en otro controller...
Entiendo que la API a su vez consume otra API :  de datos.gob.ar con lo que estarian presentes en el codigo tanto el consumo como la generacion de datos que sirve la api misma no?
Me es necesario usar  persistencia (para usuario, login ,etc) , y en este caso lo estaria manejando con Sql local y EntityFramework ya que es lo mas practico para mi, esta ok?
Respecto a la inyeccion de dependencias tengo su uso en el codigo y mi idea de lo que es en general pero no tengo un desarrollo del tema en si. Solo esta usado a nivel codigo.
Las pruebas unitarias, Logs (BD y File) lo dejo para el final ya que prefiero avanzar mas en firme, aunque el log veo de poder usarlo de entrada.
