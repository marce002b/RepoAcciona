using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Nombre_Pcia { get; set; }

        public double Lat_Pcia   { get; set; }
        public double Lon_Pcia { get; set; }

        public int Municipios_Cant { get; set; }
    }
}
