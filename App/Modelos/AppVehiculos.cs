using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modelos
{
    public class AppVehiculos
    {
        public string _id { get; set; }
        public string nombre { get; set; }
        public string modelo { get; set; }
        public int velocidad_max { get; set; }
        public int precio { get; set; }
        public string imagen_url { get; set; }
        public string marca { get; set; }


        public override string ToString()
        {
            return nombre + " - " + modelo;
        }
    }
}
