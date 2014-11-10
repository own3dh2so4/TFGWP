using PrTab.Model.Comunicacion;
using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.ViewModel
{
    class ExamenViewModel :NotificationenabledObject
    {
        private List<Pregunta> preguntasExamen;
        private int[] respuestas;
        public Pregunta preguntaMostrada;
        private int posicion;

        public Pregunta PreguntaMostrada
        {
            get { return preguntaMostrada; }
            set { preguntaMostrada = value;
            this.OnPropertyChanged("PreguntaMostrada");
            }
        }

        Comunicacion_Examen servicioExamen = new Comunicacion_Examen();

        public ExamenViewModel()
        {
            preguntasExamen = new List<Pregunta>();
            posicion = -1;
            servicioExamen.getExanenCompletado += (s, a) =>
                  {
                      preguntasExamen = a.preguntas;
                      if(preguntasExamen.Count!=0)
                      {
                          PreguntaMostrada = preguntasExamen[0];
                          posicion = 0;
                      }
                      respuestas = new int[preguntasExamen.Count];
                      for (int i=0; i<respuestas.Length; i++)
                      {
                          respuestas[i] = 0;
                      }
                  };
        }

        public void setAsignatura(string asignatura)
        {
            //Aqui llamo al servicio para coger un examen.

        }

        public void setTema(string asignatura,string idTema)
        {
            //Aqui llamo para coger las cositas del tema.
        }

        public void contestarPregunta(int resp)
        {
            if (resp==0 || resp==1 || resp == 2 || resp == 3 || resp ==4)
                respuestas[posicion] = resp;
        }

        public void siguientePregunta()
        {
            if (posicion<preguntasExamen.Count && posicion!=-1)
            {
                posicion++;
                PreguntaMostrada = preguntasExamen[posicion];

            }
        }

        public void anteriorPregunta()
        {
            if (posicion>0)
            {
                posicion--;
                PreguntaMostrada = preguntasExamen[posicion];
            }
        }

        public void evaluarExamen()
        {
            int nota = 0;
            for (int i=0; i<preguntasExamen.Count; i++)
            {
                if (preguntasExamen[i].respuestaCorrecta == respuestas[i])
                    nota++;
            }
        }


    }
}
