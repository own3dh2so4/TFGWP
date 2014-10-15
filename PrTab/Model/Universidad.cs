using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    public class Universidad
    {
        public string nombre;
        public int idProvincia;
        public int identificador;

        public Universidad(string name, int id, int idProv)
        {
            nombre = name;
            idProvincia = idProv;
            identificador = id;
        }
    }
}
