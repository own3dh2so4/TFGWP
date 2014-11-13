using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class Tema
    {
        [PrimaryKey]
        public int identificador { get; set; }
        public string nombre { get; set; }
        public int idAsignatura { get; set; }

        public Tema(int id, string name, int idAsig)
        {
            identificador=id;
            nombre = name;
            idAsignatura = idAsig;
        }

        public Tema()
        { }
    }
}
