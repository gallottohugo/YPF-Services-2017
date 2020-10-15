using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace Services
{
    
    public class Auto
    {
        public int id { get; set; }
        public string dominio { get; set; }
        public string modelo { get; set; }
        public string propietario { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int periodo { get; set; }
        public bool llamada { get; set; }
        public DateTime llamamda_fecha { get; set; }
    }
}
