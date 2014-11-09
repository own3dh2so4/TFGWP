using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class Pregunta
    {
        [PrimaryKey]
        public int identificador { get; set; }
        public string enunciado { get; set; }
        public string respuesta1 { get; set; }
        public string respuesta2 { get; set; }
        public string respuesta3 { get; set; }
        public string respuesta4 { get; set; }
        public int respuestaCorrecta { get; set; }
        public int idTema { get; set; }

        public Pregunta(int id, string enun, string r1,string r2, string r3, string r4, int rc, int idT)
        {
            identificador = id;
            enunciado = enun;
            respuesta1 = r1;
            respuesta2 = r2;
            respuesta3 = r3;
            respuesta4 = r4;
            respuestaCorrecta = rc;
            idTema = idT;
        }
    }
}
