using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class Provincia
    {
        [PrimaryKey]
        public int identificador { get; set; }
        public string nombre { get; set; }
        

        public Provincia(string name, int id)
        {
            nombre = name;
            identificador = id;
        }
    }
}
