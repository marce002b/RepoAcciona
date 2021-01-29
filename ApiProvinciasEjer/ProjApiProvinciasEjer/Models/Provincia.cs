using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Models
{
    public class Provincia
    {

        public Provincia()
        {
            Municipios = new List<Municipio>(); //esto es para que no salga null  y salgan corchete []  cada vex que no hay data, hago un ctor con la lista vacia
        }
        public int Id { get; set; }
        [StringLength(45)]
        public string Nombre_Pcia { get; set; }//cmj en caso de largo nombre "Nombre_Pcia": [ "The field Nombre_Pcia must be a string with a maximum length of 45."

        //public double Lat_Pcia   { get; set; }
        //public double Lon_Pcia { get; set; }

        public Centroide centroide { get; set; }
        //cmj 
        //https://apis.datos.gob.ar/georef/api/provincias?nombre=chaco
        //{"cantidad":1,"inicio":0,"parametros":{"nombre":"chaco"},"provincias":[{"centroide":{"lat":-26.3864309061226,"lon":-60.7658307438603},"id":"22","nombre":"Chaco"}],"total":1}


        public int Municipios_Cant { get; set; }

        public List<Municipio> Municipios { get; set; }


        [ForeignKey("Pais")] //esta va a ser una llave foranea para la propiedad navigacional Pais de abajo
        public int PaisId { get; set; } //cmj esta es una llave foranea , propiedades navigacionales de relaciones
        [JsonIgnore]
        public Pais Pais { get; set; } //cmj pq a cada pcia le corresponde un pais, propiedades navigacionales de relaciones
    }


    //cmj necesarios para https://apis.datos.gob.ar/georef/api/provincias?nombre=chaco
    public class Centroide
    {
        public int Id { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Root
    {
        public int cantidad { get; set; }
        public int inicio { get; set; }
        public Parametros parametros { get; set; }
        public List<Provincia> provincias { get; set; }
        public int total { get; set; }
    }

    public class Parametros
    {
        public string nombre { get; set; }
    }
}
