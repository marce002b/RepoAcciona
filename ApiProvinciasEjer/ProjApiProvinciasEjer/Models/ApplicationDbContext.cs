using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Models
{
    public class ApplicationDbContext: DbContext 
    {

        //este constructor le va a pasar a su padre las opciones q se envien a traves de el
        //envia para aca las opciones que estamos usando en el startup
        //osea en el startup estoy usando este ApplicationDbContext pero sin eeste constructor no andara
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        { }

        //cmj EF para guardar los datos, creamos una tabla para cada una
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Municipio> Municipios { get; set; }

    }
}
