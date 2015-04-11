using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class PreguntaCortaRespuesta: PreguntaInterface
    {
        [PrimaryKey]
        public int identificador { get; set; }
        public string enunciado { get; set; }
        public string respuestaCorrecta { get; set; }
        public int idTema { get; set; }

        public PreguntaCortaRespuesta(int i, string enu, string resp, int idT)
        {
            identificador = i;
            enunciado = enu;
            respuestaCorrecta = resp;
            idTema = idT;
        }

        public PreguntaCortaRespuesta()
        {

        }

    }
}
