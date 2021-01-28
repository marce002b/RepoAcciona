using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre_Munic { get; set; }

        public int ProvinciaId { get; set; } //cmj propiedades navigacionales de relaciones

        [JsonIgnore]
        public Provincia Provincia { get; set; } //cmj propiedades navigacionales de relaciones
    }
}
