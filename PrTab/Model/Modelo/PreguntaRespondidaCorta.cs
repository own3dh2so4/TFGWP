using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class PreguntaRespondidaCorta : PreguntaCortaRespuesta, PreguntaRespondidaInterface
    {
        public string respuesta { get; set; }
        public PreguntaRespondidaCorta(int i, string enu, string resp, int idT, string r)
            :base(i, enu,  resp,  idT)
        {
            respuesta = r;
        }

        public PreguntaRespondidaCorta() { }
    }
}
