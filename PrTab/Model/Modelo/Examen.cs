using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class Examen:Documento
    {
        public string mes;
        
        public Examen():base()
        {

        }

        public Examen(string idAsi, string idT, string a, string mes, string nu, string fu, List<string> i)
            :base( idAsi, idT, a, nu, fu ,i)
        {
            this.mes = mes;
        }
    }
}
