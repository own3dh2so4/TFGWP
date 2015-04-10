using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class PreguntaMultirespuesta : PreguntaInterface
    {
        [PrimaryKey]
        public int identificador { get; set; }
        public string enunciado { get; set; }
        public string respuesta1 { get; set; }
        public string respuesta2 { get; set; }
        public string respuesta3 { get; set; }
        public string respuesta4 { get; set; }
        public string respuesta5 { get; set; }
        //En la base de datos no se puede guradar una List con las respuestas correctas, asi que ponemos un bool por cada respuesta para saber si es correcta o no.
        public bool correcta1 {get; set;}
        public bool correcta2 {get; set;}
        public bool correcta3 { get; set; }
        public bool correcta4 { get; set; }
        public bool correcta5 { get; set; }
        public int idTema { get; set; }

        public PreguntaMultirespuesta(int id, string enun, string r1,string r2, string r3, string r4,string r5,List<int> correctas, int idT)
        {
            identificador = id;
            enunciado = enun;
            respuesta1 = r1;
            respuesta2 = r2;
            respuesta3 = r3;
            respuesta4 = r4;
            respuesta5 = r5;
            idTema = idT;
            correcta1 = false;
            correcta2 = false;
            correcta3 = false;
            correcta4 = false;
            correcta5 = false;
            foreach(var c in correctas)
            {
                switch(c)
                {
                    case 1: correcta1 = true;
                        break;
                    case 2: correcta2 = true;
                        break;
                    case 3: correcta3 = true;
                        break;
                    case 4: correcta4 = true;
                        break;
                    case 5: correcta5 = true;
                        break;
                }
            }
        }

        public PreguntaMultirespuesta()
        {

        }
    }
}
