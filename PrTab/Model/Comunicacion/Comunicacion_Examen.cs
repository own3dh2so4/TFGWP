using Newtonsoft.Json.Linq;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Comunicacion
{
    class Comunicacion_Examen
    {
        public event EventHandler<ExamenEventArgs> getExanenCompletado;

        //Aqui creare el metodo para pedir examen y cuando este lanzare el evento.

        public async Task<bool> getExamen (string asignatura, string numPreguguntas)
        {
            List<Pregunta> preguntasExamen = new List<Pregunta>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura, numPreguguntas);
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                JArray preguntas = (JArray)o.SelectToken("questions");
                foreach (var p in preguntas)
                {
                    preguntasExamen.Add(new Pregunta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("respuesta1"),
                        (string)p.SelectToken("respuesta2"),
                        (string)p.SelectToken("respuesta3"),
                        (string)p.SelectToken("respuesta4"),
                        Convert.ToInt32((string)p.SelectToken("respuestaCorrecta")),
                        0));//El id del tema no se pasa a si que lo pongo a 0.
                }

                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }
            }

            return false;
        }
    }
}
