using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class RespuestaPregunta
    {
        public int i{ get; set; }
        public string r { get; set; }

        public RespuestaPregunta(int idP, string r)
        {
            i = idP;
            this.r = r;
        }

        public static List<RespuestaPregunta> parseRespuestaPregunta (List<PreguntaRespondida> list)
        {
            List<RespuestaPregunta> ret = new List<RespuestaPregunta>();
            foreach (var p in list)
            {
                ret.Add(new RespuestaPregunta(p.identificador, p.Respuesta+""));
            }
            return ret;
        }

        public static List<RespuestaPregunta> parseRespuestaPregunta(List<PreguntaMultiRespondida> list)
        {
            List<RespuestaPregunta> ret = new List<RespuestaPregunta>();
            foreach(var p in list)
            {
                ret.Add(new RespuestaPregunta(p.identificador, JsonConvert.SerializeObject(p.respuestas())));
            }
            return ret;
        }

        public static List<RespuestaPregunta> parseRespuestaPregunta(List<PreguntaParejasRespondida> list)
        {
            List<RespuestaPregunta> ret = new List<RespuestaPregunta>();
            foreach (var p in list)
            {
                //ret.Add(new RespuestaPregunta(p.identificador, JsonConvert.SerializeObject(p.respuesta)));
                ret.Add(new RespuestaPregunta(p.identificador, p.respuesta));
            }
            return ret;
        }

        public static List<RespuestaPregunta> parseRespuestaPregunta(List<PreguntaRespondidaCorta> list)
        {
            List<RespuestaPregunta> ret = new List<RespuestaPregunta>();
            foreach (var p in list)
            {
                ret.Add(new RespuestaPregunta(p.identificador, p.respuesta));
            }
            return ret;
        }
    }
}
