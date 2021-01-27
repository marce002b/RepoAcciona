using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
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
            if (login == "Marcelo") return Ok(); //new CreatedAtRouteResult("Login Aceptado",null); 
            else return BadRequest();
        }

       


    }
}
