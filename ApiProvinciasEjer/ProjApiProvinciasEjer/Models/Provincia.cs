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

        public double Lat_Pcia   { get; set; }
        public double Lon_Pcia { get; set; }

        public int Municipios_Cant { get; set; }

        public List<Municipio> Municipios { get; set; }


        [ForeignKey("Pais")] //esta va a ser una llave foranea para la propiedad navigacional Pais de abajo
        public int PaisId { get; set; } //cmj esta es una llave foranea , propiedades navigacionales de relaciones
        [JsonIgnore]
        public Pais Pais { get; set; } //cmj pq a cada pcia le corresponde un pais, propiedades navigacionales de relaciones
    }
}
