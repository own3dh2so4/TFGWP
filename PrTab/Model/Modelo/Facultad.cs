using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class Facultad
    {
        [PrimaryKey]
        public int identificador { get; set; }
        public string nombre { get; set; }
        public int idUniversidad { get; set; }
        

        public Facultad(string name, int id, int idUni)
        {
            nombre = name;
            idUniversidad = idUni;
            identificador = id;
        }

        public Facultad()
        {

        }
    }
}
