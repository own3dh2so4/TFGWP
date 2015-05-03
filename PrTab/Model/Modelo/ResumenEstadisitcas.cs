using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class ResumenEstadisitcas
    {
        public List<TestStat> test { get; set; }
        public long avg_tiempo { get; set; }
        public double avg_nota { get; set; }
        public double per_correcta { get; set; }
        public double per_incorrecta { get; set; }
        public double per_noRespondida {get; set;}

        public ResumenEstadisitcas(List<TestStat> t, long ti, double n, double c, double i, double nr)
        {
            test = t;
            avg_tiempo = ti;
            avg_nota = n;
            per_correcta = c;
            per_incorrecta = i;
            per_noRespondida = nr;
        }

        public ResumenEstadisitcas() { }
    }
}
