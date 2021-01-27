using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProjApiProvinciasEjer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //cmj para leer configuracion 'marcelo' en var de entorno propiedades del proyecto y debug -- env variables, asi no esta hardcoded
        public LoginController(IConfiguration configuration)//este constructor lo hago para ller en el controller la configuracion de una variable de entorno, invento mio
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; } //cmj para usar la propiedad Configuration
        
        // GET api/Login
        [HttpGet]
        public ActionResult<string> Get()
        {
            return  "Debe hacer un POST a Api/Login para continuar";
        }

        // POST api/Login
        [HttpPost]
        public IActionResult Post([FromBody] string login)
        {
            //if (login == "Marcelo") return Ok(); //postman devolveria 200 ok si mando ok y si no un 400 badrequest error ante otro login 
            if (login == Configuration["loginvarentorno"]) return Ok(); //postman devolveria 200 ok si mando ok y si no un 400 badrequest error ante otro login 
            else return BadRequest();
        }

       


    }
}
