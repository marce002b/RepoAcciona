using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjApiProvinciasEjer.Models
{
    public class Pais
    {

        public Pais()
        {
            Provincias = new List<Provincia>(); //esto es para que no salga null , y salgan corchete [] cada vex que no hay data, hago un ctor con la lista vacia

        }
        public int Id { get; set; }
        ///cmj en caso de largo nombre "Nombre_Pcia": [ "The field Nombre_Pcia must be a string with a maximum length of 45."

        [StringLength(45)]
        public string Nombre_Pais { get; set; }
        public string Continente { get; set; }
        public string  Habitantes { get; set; }
        public string Superficie { get; set; }
        public List<Provincia> Provincias { get; set; }

    }
}
