using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class RespuestaPregunta
    {
        public int i { get; set; }
        public int r { get; set; }

        public RespuestaPregunta(int idP, int r)
        {
            i = idP;
            this.r = r;
        }

        public static List<RespuestaPregunta> parseRespuestaPregunta (List<PreguntaRespondida> list)
        {
            List<RespuestaPregunta> ret = new List<RespuestaPregunta>();
            foreach (var p in list)
            {
                ret.Add(new RespuestaPregunta(p.identificador, p.Respuesta));
            }
            return ret;
        }
    }
}
