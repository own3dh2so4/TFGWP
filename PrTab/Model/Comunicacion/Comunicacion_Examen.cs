using Newtonsoft.Json.Linq;
using PrTab.Model.Base_de_Datos;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace PrTab.Model.Comunicacion
{
    class Comunicacion_Examen
    {
        public event EventHandler<ExamenEventArgs> getExanenCompletado;

        public event EventHandler<TemaEventArgs> getTemasCompletado;

        public event EventHandler<TemaEventServerArgs> getTemasServerCompletado;

        private CDB_TemaExamen DB_Tema = new CDB_TemaExamen();

        public void getThemeFromDataBase(string asignatura)
        {
            List<Tema> temasAsignaturas = DB_Tema.getTemasDeAsignaturas(asignatura);
            if (getTemasCompletado != null)
            {
                getTemasCompletado(this, new TemaEventArgs(temasAsignaturas));
            }
        }

        public async Task<bool> getThemeFromServer (string asignatura)
        {
            List<Tema> temasAsignatura = new List<Tema>();
            string response = await Comunicacion.getTemaAsignatura(AplicationSettings.getToken(), asignatura);
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                JArray temas = (JArray)o.SelectToken("data");
                foreach(var t in temas)
                {
                    temasAsignatura.Add(new Tema(Convert.ToInt32((string)t.SelectToken("pk")),
                        (string)t.SelectToken("nombre"),
                        Convert.ToInt32(asignatura)));
                }

                if (getTemasServerCompletado != null)
                {
                    getTemasServerCompletado(this, new TemaEventServerArgs(temasAsignatura));
                }

                DB_Tema.insertAll(temasAsignatura);

                return true;
            }
            return false;
        }


        public async Task<bool> getExamenParejas(string asignatura,  string numPreguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura,  "pa", numPreguntas);
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));
                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    preguntasExamen.Add(new PreguntaParejaRespuesta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("pareja11"),
                        (string)p.SelectToken("pareja12"),
                        (string)p.SelectToken("pareja13"),
                        (string)p.SelectToken("pareja21"),
                        (string)p.SelectToken("pareja22"),
                        (string)p.SelectToken("pareja23"),
                        Convert.ToInt32((string)p.SelectToken("tema"))));
                }
                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }
                return true;
            }
            return false;
        }

        public async Task<bool> getExamenCortas(string asignatura, string numPreguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura, "sa", numPreguntas);
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));
                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    preguntasExamen.Add(new PreguntaCortaRespuesta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("respuestaCorta"),
                        Convert.ToInt32((string)p.SelectToken("enunciado"))));
                }
                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }
                return true;
            }
            
            
            return false;
        }

        public async Task<bool> getExamenMulti(string asignatura, string numPreguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura, "ma", numPreguntas);
            JObject o = JObject.Parse(response);
            if((string)o.SelectToken("error")=="200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));
                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    List<int> correctas = new List<int>();
                    foreach (var i in (JArray)p.SelectToken("respuestasCorrectas"))
                        correctas.Add(Convert.ToInt32((string)i));
                    preguntasExamen.Add(new PreguntaMultirespuesta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("respuesta1"),
                        (string)p.SelectToken("respuesta2"),
                        (string)p.SelectToken("respuesta3"),
                        (string)p.SelectToken("respuesta4"),
                        (string)p.SelectToken("respuesta5"),
                        correctas,
                        0));
                }
                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }
                return true;
            }

            return false;
        }

        public async Task<bool> getExamenNormal (string asignatura, string numPreguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura, "na", numPreguntas );
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));

                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    preguntasExamen.Add(new Pregunta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("respuesta1"),
                        (string)p.SelectToken("respuesta2"),
                        (string)p.SelectToken("respuesta3"),
                        (string)p.SelectToken("respuesta4"),
                        (string)p.SelectToken("respuesta5"),
                        Convert.ToInt32((string)p.SelectToken("respuestaCorrecta")),
                        0));//El id del tema no se pasa a si que lo pongo a 0.
                }

                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }

                return true;
            }

            return false;
        }
        public async Task<bool> getExamenParejas(string asignatura,string tema, string numPreguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura, tema, "pa", numPreguntas);
            JObject o = JObject.Parse(response);
            if((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));
                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    preguntasExamen.Add(new PreguntaParejaRespuesta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("pareja11"),
                        (string)p.SelectToken("pareja12"),
                        (string)p.SelectToken("pareja13"),
                        (string)p.SelectToken("pareja21"),
                        (string)p.SelectToken("pareja22"),
                        (string)p.SelectToken("pareja23"),
                        Convert.ToInt32((string)p.SelectToken("tema"))));
                }
                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }
                return true;
            }
            return false;
        }
        public async Task<bool> getExamenCortas(string asignatura,string tema, string numPreguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura,tema, "sa", numPreguntas);
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));
                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    preguntasExamen.Add(new PreguntaCortaRespuesta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("respuestaCorta"),
                        Convert.ToInt32((string)p.SelectToken("enunciado"))));
                }
                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }
                return true;
            }


            return false;
        }

        public async Task<bool> getExamenMulti(string asignatura, string tema, string numPreguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura,tema, "ma", numPreguntas);
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));
                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    List<int> correctas = new List<int>();
                    foreach (var i in (JArray)p.SelectToken("respuestasCorrectas"))
                        correctas.Add(Convert.ToInt32((string)i));
                    preguntasExamen.Add(new PreguntaMultirespuesta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("respuesta1"),
                        (string)p.SelectToken("respuesta2"),
                        (string)p.SelectToken("respuesta3"),
                        (string)p.SelectToken("respuesta4"),
                        (string)p.SelectToken("respuesta5"),
                        correctas,
                        0));
                }
                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }
                return true;
            }

            return false;
        }

        public async Task<bool> getExamenNormal(string asignatura, string tema, string numPreguguntas)
        {
            List<PreguntaInterface> preguntasExamen = new List<PreguntaInterface>();
            string response = await Comunicacion.getExamen(AplicationSettings.getToken(), asignatura, tema,"na", numPreguguntas);
            JObject o = JObject.Parse(response);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setIdTest((string)o.SelectToken("test"));
                JArray preguntas = (JArray)o.SelectToken("data");
                foreach (var p in preguntas)
                {
                    preguntasExamen.Add(new Pregunta(Convert.ToInt32((string)p.SelectToken("pk")),
                        (string)p.SelectToken("enunciado"),
                        (string)p.SelectToken("respuesta1"),
                        (string)p.SelectToken("respuesta2"),
                        (string)p.SelectToken("respuesta3"),
                        (string)p.SelectToken("respuesta4"),
                        (string)p.SelectToken("respuesta5"),
                        Convert.ToInt32((string)p.SelectToken("respuestaCorrecta")),
                        Convert.ToInt32(tema)));
                }

                if (getExanenCompletado != null)
                {
                    getExanenCompletado(this, new ExamenEventArgs(preguntasExamen));
                }

                return true;
            }

            return false;
        }

        public async Task<bool> sendResultadoExamen(string asignatura, int numRespuestasCorrectas, int numeroPreguntas, List<PreguntaRespondida> lista, long tiempo )
        {
            /*MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer jsonList = new DataContractJsonSerializer(typeof(List<RespuestaFallidaPregunta>));
            jsonList.WriteObject(stream, listaFallos);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            var json = sr.ReadToEnd();*/
            var listaFallos = RespuestaPregunta.parseRespuestaPregunta(lista);
            var json = JsonConvert.SerializeObject(listaFallos);
            string response = await Comunicacion.sendResults(AplicationSettings.getToken(),AplicationSettings.getIdTest(),json,tiempo+"");

            //Continuar por aqui...

            return false;
        }

    }
}
