using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab01.Models
{
    public class Jugador
    {
        public int JugadorID { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Club { get; set; }

        public string Posición { get; set; }

        public double Sueldo { get; set; }
    }
}