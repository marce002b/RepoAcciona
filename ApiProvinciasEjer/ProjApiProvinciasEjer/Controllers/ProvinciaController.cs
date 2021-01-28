using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjApiProvinciasEjer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Controllers
{
    [Produces("application/json")]
    [Route("api/Provincia")]
    [ApiController]
    public class ProvinciaController : Controller
    {
        private readonly ApplicationDbContext context;
        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> Get()
        {
            return context.Provincias.ToList();
        }



        //CMJ aca enviaremos un nuevo pcia y recibiremos un 201 conted created y devolveremos la pcia creada, todo  via POSTMAN
        //en mi  ejemplo en la URL https://localhost:44372/api/provincia
        //entramos {"nombre_Pcia":"Salta" , "Id":22}
        //mandaremos eso de arriba  en postman con POST y en Body-RAW-Json  
        //entonces recibiremos STATUS 201 created 
        //y se listan los datos creador osea el nuevo pcia entero gracias a CreatedAtRouteResult

        //        {
        //    "id": 22,
        //    "nombre_Pcia": "Salta",
        //    "lat_Pcia": 0,
        //    "lon_Pcia": 0,
        //    "municipios_Cant": 0,
        //    "municipios": null,
        //    "paisId": 0
        //}

        [HttpPost]
        public IActionResult Post([FromBody] Provincia mipcia)
        {
            if (ModelState.IsValid)
            {

                context.Provincias.Add(mipcia);
                context.SaveChanges();
                return new CreatedAtRouteResult("paisdevuelto", new { Id = mipcia.Id }, mipcia);
            }

            return BadRequest(ModelState);
        }
    }

}

