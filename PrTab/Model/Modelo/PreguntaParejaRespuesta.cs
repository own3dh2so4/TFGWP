using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class PreguntaParejaRespuesta :PreguntaInterface
    {
        public int identificador { get; set; }
        public string enunciado { get; set; }
        public string pareja11 { get; set; }
        public string pareja12 { get; set; }
        public string pareja13 { get; set; }
        public string pareja21 { get; set; }
        public string pareja22 { get; set; }
        public string pareja23 { get; set; }
        public int idTema { get; set; }

        public PreguntaParejaRespuesta(int id, string enun, string p11, string p12, string p13, string p21, string p22, string p23, int idT)
        {
            identificador = id;
            enunciado = enun;
            pareja11 = p11;
            pareja12 = p12;
            pareja13 = p13;
            pareja21 = p21;
            pareja22 = p22;
            pareja23 = p23;
            idTema = idT;
        }

        public PreguntaParejaRespuesta()
        { }
    }
}
