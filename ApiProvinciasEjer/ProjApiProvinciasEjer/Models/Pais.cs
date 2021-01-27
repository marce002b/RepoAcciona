using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Models
{
    public class Pais
    {
        public int Id_Pais { get; set; }
        public string Nombre_Pais { get; set; }
        public string Continente { get; set; }
        public string  Habitantes { get; set; }
        public string Superficie { get; set; }
    }
}
