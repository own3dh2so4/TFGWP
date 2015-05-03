using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class TestStat
    {
        public long fecha { get; set; }
        public double noRespondidas { get; set; }
        public double preguntas { get; set; }
        public double nota { get; set; }
        public long tiempo { get; set; }
        public double correctas { get; set; }
        public double falladas { get; set; }

        public TestStat()
        {

        }

        public TestStat(long fe, int nR, int p, int n,  long t, int c, int f)
        {
            fecha = fe;
            noRespondidas = nR;
            preguntas = p;
            nota = n;
            tiempo = t;
            correctas = c;
            falladas = f;
        }

        public double getNotaSobreDiez()
        {
            return 10*(correctas / preguntas);
        }

        public double correctasSobreCien()
        {
            return 100 * (correctas / preguntas);
        }

        public double sinResponderSobreCien()
        {
            return 100 * (noRespondidas / preguntas);
        }

        public double falladasSobreCien()
        {
            return 100 * (falladas / preguntas);
        }
    }
}
