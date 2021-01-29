using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjApiProvinciasEjer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ProjApiProvinciasEjer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //cmj usamos una base en memoria por ahora, y agrego el servicio para agregar EF para poder inyectar nuestro ApplicationDbContext    
            //cmj a traves de inyeccion de dependencias en nuestros controllers
            //services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("AccionaDB"));

            //en sql server local
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //ademas de esto necesitamos agregar identity para usar el sistema de usuarios por defecto de asp net
            //con esto instanciaremos el servicio de asp core identity , usando ApplicationUser que representara un usuario en nuestra base de datos
            //asiq debemos crear una clase ApplicationUser que herede de IdentityUser

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            app.UseMvc();



            //cmj para tener en memoria algunos datos de ejemplo de paises, provincias antes de llamar a la api del gobierno, despues comentar

            //comento antes de crear la base de datos, si no cdo corra add-migration al no estar la base de datos creada daria error

            /*
            if (!context.Paises.Any())
            {
                context.Paises.AddRange(new List<Pais>()
                {
                    new Pais(){Nombre_Pais = "Argentina", Continente="America",  Id = 1 ,Provincias = new List<Provincia>() {new Provincia(){Nombre_Pcia = "Buenos Aires", Id = 1, Municipios_Cant = 2, Municipios = new List<Municipio>() { new Municipio() { Nombre_Munic = "San Isidro" , Id = 1}, new Municipio() { Nombre_Munic = "Lujan" , Id = 2} } }, new Provincia() { Nombre_Pcia = "Mendoza", Id = 2}, new Provincia() { Nombre_Pcia = "Jujuy" , Id = 3} } },
                    new Pais(){Nombre_Pais = "Chile",  Continente="America", Id = 2  }



                });
                context.SaveChanges();

            }

            if (!context.Provincias.Any())
            {
                context.Provincias.AddRange(new List<Provincia>()
                {
                    //new Provincia(){Nombre_Pcia = "Buenos Aires", Lat_Pcia =-34.4323 , Lon_Pcia = -58.5556 , Id = 1,Municipios_Cant = 2 , Municipios = new List<Municipio>() {new Municipio(){Nombre_Munic = "San Isidro"}, new Municipio() { Nombre_Munic = "Lujan" } } },
                    //new Provincia(){Nombre_Pcia = "Mendoza", Lat_Pcia =-33.4323 , Lon_Pcia = -33.5556 , Id = 2  },



                });
                context.SaveChanges();
            
            }
            */
        }
    }
}
