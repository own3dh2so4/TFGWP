using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class Provincia
    {
        public string nombre;
        public int identificador; 

        public Provincia(string name, int id)
        {
            nombre = name;
            identificador = id;
        }
    }
}
