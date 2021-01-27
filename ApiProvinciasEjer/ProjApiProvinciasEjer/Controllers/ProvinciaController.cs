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
    }
}
