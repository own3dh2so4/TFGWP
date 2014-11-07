using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class Universidad
    {
        [PrimaryKey]
        public int identificador{get;set;}
        public string nombre { get; set; }
        public int idProvincia { get; set; }
        

        public Universidad(string name, int id, int idProv)
        {
            nombre = name;
            idProvincia = idProv;
            identificador = id;
        }
    }
}
