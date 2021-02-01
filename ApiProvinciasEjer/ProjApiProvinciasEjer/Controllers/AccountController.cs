using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using  ProjApiProvinciasEjer.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ProjApiProvinciasEjer.Controllers
{

    //CMJ dentro de este controller vamos a teener las acciones que le permitiran al usuario registrarse y autenticarse
    //y tb estara el codigo que generara el token

    //este es un codigo muy usado para la creacion de usuarios copie y pegue as is...

    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;//**para traer el string de la llave secreta

        public AccountController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IConfiguration configuration)//**aca uso inyeccion de dependencias para usarlo luego para levantar la var de ambiente para la llave secreta, el campo se crea abajo tb
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;//**campo creado para traer el string de la llave secreta
        }


        //cmj esta accion va a usar el ApplicationUser que creamos anrtes y  le va a pasar un email y pass de un modelo que estamos enviando (UserInfo model)
        //luego vamos a intentar crear el usuario usando el _userManager y si el resultado esta ok se construye el token via BuildToken y si hay un problema
        //con el usuario damos mensaje de invalid o si hay problema con el modelo lanzamos un badrequest
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Username or password invalid");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        //cdo el usuario ya esta creado y quiere logearse vamos a intentar loguear al usuaruo con _signInManager y si el resultado es exitoso
        //vamos a contruir el token y si no es exitoso lanzamos error y badrequest 
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //el token es un string , que se generara al usuario que provea correttamente su usuario y password, contiene cabecera, data (claims?) y firma o  area de data de seguridad que usa la llave secreta combinada con las otras partes
        // usuario debera usar el token que generamos cada vez que quiera acceder a la app para indicar que esta autorizado a ingresar.

        //cmj este metodo va a construir el token a partir de la info del uaurio  pq en el token va uno de los claims en el cuerpo del token
        //como una de las informaciones en las cuales podemos confiar . y entonces se usa la llave secreta para firmar el token para saber en el futuro
        //si este token sera confiable cuando se use.
        //los claims son el conjunto de informaciones en las cuales confiamos, puede ser un nombre un id y un valor un arreglo de claims usaremos.
        //ademas esa info se forma de JwtRegisteredClaimNames (que son conjunto de nombres que los claims pueden tener que estan regulados por el standar de json JWT
        //se puede enviar el nombre de usaurio el mail el id o lo que deseemos....ej JwtRegisteredClaimNames.FamilyName
        private IActionResult BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
               //aca van "ID"1,"VALUE"
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),//nombre de usuario en este caso email, podria ser el id tambien , ojo id y valor 
        new Claim("Valor", "whatever you want to put here"),// Id y valor mas informacion cualquiera
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())//generara un string largo y random , con dificil posibilidad de colision , osea bien random
      //NOTA: el valor que pongamos en JwtRegisteredClaimNames.Jti de arriba sirve para revocar un token ya que es un identificador de token, o para desloguear de varios dispositivos
      //el Guid genera un string aleatorio y largo asiq sirve para eso mismo , id de token
            };

            //ahora usaremos el string secreto que tenemos en env vars y lo usaremos para crear nuestra llave secreta
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave_secreta"]));//como var de ambiente, mas seguro , de string a array de bytes
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//le pasamos la llave y el algoritmo a usar

            var expiration =  DateTime.UtcNow.AddHours(99999);//que no expire pronto para este ejemplo basta


            //aca instanciamos una var de tipo JwtSecurityToken y configurando sus valores como el emisor o issuer, etc
            JwtSecurityToken token = new JwtSecurityToken(
             issuer: "yourdomain.com",//emisor del token pero aca soy yo marcelo jury ja
             audience: "yourdomain.com",
             claims: claims, //los claims que acabamos de crear
             expires: expiration,//arriba puse 9999
             signingCredentials: creds); // las credenciales que creamos arriba

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token), //finalmente esto sirve para retornar el TOKEN, retornamos un obj anonimo con el token y su expiracion
                expiration = expiration
            });
                //cmj EJEMPLO:

                //{
                //"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNtanVyeTNAZ21haWwuY29tIiwiVmFsb3IiOiJ3aGF0ZXZlciB5b3Ugd2FudCB0byBwdXQgaGVyZSIsImp0aSI6ImI5YzhjZmEzLTA4YjEtNDJkMi05YWVmLWRlNDJmMTRlMWI2NyIsImV4cCI6MTk3MTk0MjIzNCwiaXNzIjoieW91cmRvbWFpbi5jb20iLCJhdWQiOiJ5b3VyZG9tYWluLmNvbSJ9.8_fjTtF9zIZikgKybE_UdkbLK2_G0seUvR0m-95xUmo",
                //"expiration": "2032-06-27T09:43:54.8076837Z"
                //}
        }


    }
}