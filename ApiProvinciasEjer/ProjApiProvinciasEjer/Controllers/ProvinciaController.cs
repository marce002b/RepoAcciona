using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjApiProvinciasEjer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Controllers
{


    [Produces("application/json")]
    [Route("api/Provincia")]
    [ApiController]
    public class ProvinciaController : Controller
    {
        private string BASE_URL = "https://apis.datos.gob.ar/georef/api/provincias?nombre=chaco";
        public Task<HttpResponseMessage> Find()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client.GetAsync("find");
            }
            catch
            {
                return null;
            }
        }
        private readonly ApplicationDbContext context;
        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> Get()
        {
            return context.Provincias.ToList();
            //retornar tb los municipios
            //return context.Provincias.Include(x => x.Municipios).ToList();
        }


        [HttpGet("{Id}", Name = "pciadevuelto")]
        public IActionResult GetPciaByID(int Id)
        {

           
            var pcia = context.Provincias.Include(x => x.Municipios).FirstOrDefault(x => x.Id == Id);

            if (pcia == null) //si este recurso no existe

            {
                return NotFound();//404 recurso no encontrado gracias a  IActionResult }
            }

           

            
            return Ok(pcia); // si existe pasamos el pais encontrado como parametro de retorno

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
        //cmj updatear la data
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais provincia, int id)
        {
            if (provincia.Id != id)
            {
                return BadRequest();
            }

            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //cmj borrar la data de prov
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok(provincia);
        }
    }

}

