using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class RespuestaFallidaPregunta
    {
        public int IdPregunta { get; set; }
        public int Respuesta { get; set; }

        public RespuestaFallidaPregunta(int idP, int r)
        {
            IdPregunta = idP;
            Respuesta = r;
        }
    }
}
