using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//cmj suppose that you want to build a client application that uses .NET Core's HttpClient class to invoke the Get() and Put() actions. So, first step would be to use these namespaces:
using System.Text.Json;
using System.Text.Json.Serialization;
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
    //cmj con esto nuestro esquema de autenticacion sera el de los json webtokens
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProvinciaController : Controller
    {
        private Provincia.Root gobpcia = new Provincia.Root();
        private string BASE_URL = "https://apis.datos.gob.ar/georef/api/provincias";
        private HttpClient client = new HttpClient();

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
        public async Task<IActionResult> GetPciaByID(string Id)
        {


            //si la busco a la provincia de la api de gob .ar uso esto
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await GetDataGobAr(Id); ////cmj mando parametro del nombre de pcia solicitado usando async await pq es metodo asincrono  quiero que espere a terminar
            


                //cmj si la busco de mi base usaria esto
                //var pcia = context.Provincias.Include(x => x.Municipios).FirstOrDefault(x => x.Id == Id);

                if (gobpcia == null) //si este recurso no existe

            {
                return NotFound();//404 recurso no encontrado gracias a  IActionResult }
            }

            return Ok(gobpcia); // si existe pasamos el pais encontrado como parametro de retorno

        }



        //CMJ aca enviaremos un nuevo pcia y recibiremos un 201 content created y devolveremos la pcia creada, todo  via POSTMAN
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
        public IActionResult Delete(string id)
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


        public async Task<string> GetDataGobAr(string pcianombre)//cmj recibo parametro del nombre de pcia solicitado en el ejercicio y retorno data q deserealizo en el objeto gobpcia de tipo Provincia.Root
        {

            HttpResponseMessage response = client.GetAsync(BASE_URL + "?nombre=" + pcianombre).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true // this is the point
            };
            gobpcia = JsonSerializer.Deserialize<Provincia.Root>(stringData, options);

            return "";
                
        }
    }



}

