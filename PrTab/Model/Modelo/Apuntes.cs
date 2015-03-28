using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class Apuntes : Documento
    {
        public string descipcion;

        public Apuntes() { }

        public Apuntes(string idAsi, string idT, string a, string des, string nu, string fu, List<string> i)
            :base( idAsi,  idT,  a, nu, fu, i)
        {
            descipcion = des;
        }
    }
}
