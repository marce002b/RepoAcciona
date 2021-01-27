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
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("AccionaDB"));
            
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


            //cmj para tener en memoria algunos datos de ejemplo de provincias antes de llamar a la api del gobierno, despues comentar
            if (!context.Provincias.Any())
            {
                context.Provincias.AddRange(new List<Provincia>()
                {
                    new Provincia(){Nombre_Pcia = "Mendoza", Lat_Pcia =0 }


                });
                context.SaveChanges();

            }
        }
    }
}
