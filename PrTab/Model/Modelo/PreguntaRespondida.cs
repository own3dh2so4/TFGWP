using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class PreguntaRespondida : Pregunta, PreguntaRespondidaInterface
    {
        public int Respuesta { get; set; }


        public PreguntaRespondida(int id, string enun, string r1,string r2, string r3, string r4, string r5, int rc, int idT, int r)
        {
            identificador = id;
            enunciado = enun;
            respuesta1 = r1;
            respuesta2 = r2;
            respuesta3 = r3;
            respuesta4 = r4;
            respuesta5 = r5;
            respuestaCorrecta = rc;
            idTema = idT;
            Respuesta = r;
        }

        public PreguntaRespondida() { }

    }
}
