using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class UpdateMensajeTablon
    {
        public int id;
        public int numFav;
        public bool userFav;
        public bool deleted;

        public UpdateMensajeTablon(int i, int n, bool u, bool d)
        {
            id = i;
            numFav = n;
            userFav = u;
            deleted = d;
        }
    }
}
