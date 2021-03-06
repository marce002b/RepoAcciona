﻿using Microsoft.AspNetCore.Builder;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            //cmj es para que nos de un 401 al intentar navegar hacia el web api, hasta que se ponga la llave secreta y demas

            //esto es configurar bien nuestro AddJwtBearer con las options para  entonces  validar el token recibido y enviado por el usuario para saber si es valido o no
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience=true,
                ValidateLifetime=true,
                ValidateIssuerSigningKey=true,
                ValidIssuer= "yourdomain.com",
                ValidAudience= "yourdomain.com",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Llave_secreta"])),
                ClockSkew=TimeSpan.Zero
            }) ;

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

            //cmj agregado para la autenticacion de usuarios
            app.UseAuthentication();

            app.UseMvc();



            //cmj para tener en memoria algunos datos de ejemplo de paises, provincias antes de llamar a la api del gobierno, despues comentar

            //comento antes de crear la base de datos, si no cdo corra add-migration al no estar la base de datos creada daria error

            
            if (!context.Paises.Any())
            {

                
                context.Paises.AddRange(new List<Pais>()
                {
                    new Pais(){Nombre_Pais = "Argentina", Continente="America" ,Provincias = new List<Provincia>() {new Provincia(){Nombre = "Buenos Aires", Municipios_Cant = 2, Municipios = new List<Municipio>() { new Municipio() { Nombre_Munic = "San Isidro" }, new Municipio() { Nombre_Munic = "Lujan" } } }, new Provincia() { Nombre = "Mendoza"}, new Provincia() { Nombre = "Jujuy" } } },
                    new Pais(){Nombre_Pais = "Chile",  Continente="America"  },
                    new Pais(){Nombre_Pais = "Uruguay",  Continente="America"  }



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
            
        }
    }
}
