using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class PreguntaMultiRespondida : PreguntaMultirespuesta, PreguntaRespondidaInterface
    {
        public bool respondio1 { get; set; }
        public bool respondio2 { get; set; }
        public bool respondio3 { get; set; }
        public bool respondio4 { get; set; }
        public bool respondio5 { get; set; }

        public PreguntaMultiRespondida(int id, string enun, string r1, string r2, string r3, string r4, string r5, List<int> correctas, int idT, List<int> respuestas) :
            base( id, enun, r1, r2,  r3,  r4, r5, correctas,  idT)
        {
            respondio1=false;
            respondio2=false;
            respondio3=false;
            respondio4=false;
            respondio5=false;
            foreach(var r in respuestas)
            {
                switch(r)
                {
                    case 1: respondio1 = true;
                        break;
                    case 2: respondio2 = true;
                        break;
                    case 3: respondio3 = true;
                        break;
                    case 4: respondio4 = true;
                        break;
                    case 5: respondio5 = true;
                        break;
                }
            }
        }

        public PreguntaMultiRespondida() { }

        public List<int> respuestas()
        {
            List<int> ret = new List<int>();
            if (respondio1) ret.Add(1);
            if (respondio2) ret.Add(2);
            if (respondio3) ret.Add(3);
            if (respondio4) ret.Add(4);
            if (respondio5) ret.Add(5);
            return ret;
        }
    }
}
