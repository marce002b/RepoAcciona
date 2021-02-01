using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjApiProvinciasEjer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjApiProvinciasEjer.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais")]
    [ApiController]
    //cmj con esto nuestro esquema de autenticacion sera el de los json webtokens
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaisController : Controller
    {
        private readonly ApplicationDbContext context;
        public PaisController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            return context.Paises.ToList();
            //ademas quiero traer todas las pcias
           // return context.Paises.Include(x => x.Provincias).ToList();
        }
        //cmj aca permito buscar por id un pais con verbo httpget, le pasamos un parametro
        //
        [HttpGet("{Id}", Name = "paisdevuelto")]
        public IActionResult GetPaisByID(int Id)
        {

            //var pais = context.Paises.FirstOrDefault(x => x.Id == Id);
            //ademas quiero traer todas las pcias
            var pais = context.Paises.Include(x => x.Provincias).FirstOrDefault(x => x.Id == Id);

            if (pais == null) //si este recurso no existe

            {
                return NotFound();//404 recurso no encontrado gracias a  IActionResult }
            }
            return Ok(pais); // si existe pasamos el pais encontrado como parametro de retorno

        }


        //CMJ aca enviaremos un nuevo pais y recibiremos un 201 conted created y devolveremos el pais creado, todo  via POSTMAN
        //en mi  ejemplo en la URL https://localhost:44372/api/pais
        //entramos {"Nombre":"Japon" , "Id":22}
        //mandaremos eso de arriba  en postman con POST y en Body-RAW-Json  
        //entonces recibiremos STATUS 201 created 
        //y se listan los datos creador osea el nuevo pais entero gracias a CreatedAtRouteResult
//        {
//    "id": 22,
//    "nombre_Pais": null,
//    "continente": null,
//    "habitantes": null,
//    "superficie": null,
//    "provincias": null
//}


    [HttpPost]
        public IActionResult Post([FromBody] Pais mipais)
        {
            if (ModelState.IsValid)
            {

                context.Paises.Add(mipais);
                context.SaveChanges();
                return new CreatedAtRouteResult("paisdevuelto", new { Id = mipais.Id }, mipais);
            }

            return BadRequest(ModelState);
        }

        //cmj updatear la data
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }

            context.Entry(pais).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //cmj borrar la data
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pais = context.Paises.FirstOrDefault(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            context.Paises.Remove(pais);
            context.SaveChanges();
            return Ok(pais);
        }

    }
}
