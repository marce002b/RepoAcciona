using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        [StringLength(45)]
        public string Nombre_Pcia { get; set; }//cmj en caso de largo nombre "Nombre_Pcia": [ "The field Nombre_Pcia must be a string with a maximum length of 45."

        public double Lat_Pcia   { get; set; }
        public double Lon_Pcia { get; set; }

        public int Municipios_Cant { get; set; }

        public List<Municipio> Municipios { get; set; }

        public int PaisId { get; set; } //cmj propiedades navigacionales de relaciones

        [JsonIgnore]
        public Pais Pais { get; set; } //cmj propiedades navigacionales de relaciones
    }
}
