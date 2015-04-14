using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class PreguntaParejasRespondida: PreguntaParejaRespuesta, PreguntaRespondidaInterface
    {
        public string respuesta { get; set; }

        public PreguntaParejasRespondida(int id, string enun, string p11, string p12, string p13, string p21, string p22, string p23, int idT, int r1, int r2, int r3)
            :base( id, enun,  p11,  p12,  p13,  p21,  p22,  p23,idT)
        {
            respuesta = "[[1," + r1 + "],[2," + r2 +"],[3," + r3 +"]]";
        }

        public PreguntaParejasRespondida() { }
    }
}
