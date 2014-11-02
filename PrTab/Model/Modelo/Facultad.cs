using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class Facultad
    {
        public string nombre;
        public int idUniversidad;
        public int identificador;

        public Facultad(string name, int id, int idUni)
        {
            nombre = name;
            idUniversidad = idUni;
            identificador = id;
        }
    }
}
