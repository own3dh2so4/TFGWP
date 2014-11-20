using PrTab.Model.Base_de_Datos;
using PrTab.Model.Comunicacion;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PrTab.ViewModel
{
    class ExamenViewModel :NotificationenabledObject
    {
        private List<Pregunta> preguntasExamen;
        private int[] respuestas;
        private SolidColorBrush[,] colorBotones;
        public Pregunta preguntaMostrada;
        private int posicion;
        private string textoPreguntas="";
        private string idAsignatura = "";
        private CDB_PreguntasExamenRealizado bd_preguntasrespuestas = new CDB_PreguntasExamenRealizado();


        public string NumeroPregunta
        {
            get
            {
                return textoPreguntas;
            }
            private set
            {
                textoPreguntas = value;
                this.OnPropertyChanged("NumeroPregunta");
            }
        }

        public SolidColorBrush ColorBoton1
        {
            get {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion,0]; }
            private set
            {
                colorBotones[posicion, 0] = value;
                this.OnPropertyChanged("ColorBoton1");    
            }
        }

        public SolidColorBrush ColorBoton2
        {
            get {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 1]; }
            private set
            {
                colorBotones[posicion, 1] = value;
                this.OnPropertyChanged("ColorBoton2");
            }
        }

        public SolidColorBrush ColorBoton3
        {
            get {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 2]; }
            private set
            {
                colorBotones[posicion, 2] = value;
                this.OnPropertyChanged("ColorBoton3");
            }
        }

        public SolidColorBrush ColorBoton4
        {
            get {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 3]; }
            private set
            {
                colorBotones[posicion, 3] = value;
                this.OnPropertyChanged("ColorBoton4");
            }
        }

        

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
                          //Todos el mismo color al principio, que apunten al mismo objeto asi solo hay un color en memoria
                          /*ColorBoton1 = new SolidColorBrush(Colors.Transparent);
                          ColorBoton2 = ColorBoton1;
                          ColorBoton3 = ColorBoton1;
                          ColorBoton4 = ColorBoton1;*/
                      }
                      NumeroPregunta=1+"/"+preguntasExamen.Count;
                      respuestas = new int[preguntasExamen.Count];
                      colorBotones = new SolidColorBrush[preguntasExamen.Count,4];
                      for (int i=0; i<respuestas.Length; i++)
                      {
                          respuestas[i] = 0;
                          //Pongo el fondo de los botones transparente.
                          for (int j = 0; j<=3; j++)
                          {
                              colorBotones[i, j] = new SolidColorBrush(Colors.Transparent);
                          }
                      }
                  };
        }

        public async void setAsignatura(string asignatura)
        {
            mostarMensaje("Cargando Examen");
            //Aun que el visual se queja de esto, asi esta bien.
            await servicioExamen.getExamen(asignatura, AplicationSettings.getNumeroDePreguntasExamen());
            idAsignatura = asignatura;
            ocultarMensaje();

        }

        public async void setTema(string asignatura,string idTema)
        {
            mostarMensaje("Cargando Examen");
            await servicioExamen.getExamen(asignatura, idTema, AplicationSettings.getNumeroDePreguntasExamen());
            idAsignatura = asignatura;
            ocultarMensaje();
        }

        public void contestarPregunta(int resp)
        {
            if ((resp == 1 || resp == 2 || resp == 3 || resp == 4) && (resp != respuestas[posicion]))
            {
                //Pongo transparente la que estaba antes
                switch(respuestas[posicion])
                {
                    case 1: ColorBoton1 = new SolidColorBrush(Colors.Transparent); break;
                    case 2: ColorBoton2 = new SolidColorBrush(Colors.Transparent); break;
                    case 3: ColorBoton3 = new SolidColorBrush(Colors.Transparent); break;
                    case 4: ColorBoton4 = new SolidColorBrush(Colors.Transparent); break;
                }
                respuestas[posicion] = resp;
                //Le doy color a la nueva
                switch(resp)
                {
                    case 1: ColorBoton1 = new SolidColorBrush(Colors.Orange); break;
                    case 2: ColorBoton2 = new SolidColorBrush(Colors.Orange); break;
                    case 3: ColorBoton3 = new SolidColorBrush(Colors.Orange); break;
                    case 4: ColorBoton4 = new SolidColorBrush(Colors.Orange); break;
                }
            }
            else
            {
                respuestas[posicion] = 0;
                //colorBotones[posicion, resp] = new SolidColorBrush(Colors.Transparent);
                switch (resp)
                {
                    case 1: ColorBoton1 = new SolidColorBrush(Colors.Transparent); break;
                    case 2: ColorBoton2 = new SolidColorBrush(Colors.Transparent); break;
                    case 3: ColorBoton3 = new SolidColorBrush(Colors.Transparent); break;
                    case 4: ColorBoton4 = new SolidColorBrush(Colors.Transparent); break;
                }
            }
        }

        public void siguientePregunta()
        {
            if (posicion<preguntasExamen.Count-1 && posicion!=-1)
            {
                posicion++;
                PreguntaMostrada = preguntasExamen[posicion];
                ColorBoton1 = colorBotones[posicion, 0];
                ColorBoton2 = colorBotones[posicion, 1];
                ColorBoton3 = colorBotones[posicion, 2];
                ColorBoton4 = colorBotones[posicion, 3];
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;

            }
        }

        public void anteriorPregunta()
        {
            if (posicion>0)
            {
                posicion--;
                PreguntaMostrada = preguntasExamen[posicion];
                ColorBoton1 = colorBotones[posicion, 0];
                ColorBoton2 = colorBotones[posicion, 1];
                ColorBoton3 = colorBotones[posicion, 2];
                ColorBoton4 = colorBotones[posicion, 3];
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
            }
        }

        public int evaluarExamen()
        {
            int nota = 0;
            List<RespuestaFallidaPregunta> failresponse = new List<RespuestaFallidaPregunta>();
            List<PreguntaRespondida> preguntasRespondidas = new List<PreguntaRespondida>();
            bd_preguntasrespuestas.deleteAll();
            for (int i=0; i<preguntasExamen.Count; i++)
            {
                if (preguntasExamen[i].respuestaCorrecta == respuestas[i])
                    nota++;
                else
                {
                    failresponse.Add(new RespuestaFallidaPregunta(preguntasExamen[i].identificador, respuestas[i]));
                }
                preguntasRespondidas.Add(new PreguntaRespondida(preguntasExamen[i].identificador, preguntasExamen[i].enunciado,
                    preguntasExamen[i].respuesta1, preguntasExamen[i].respuesta2, preguntasExamen[i].respuesta3, preguntasExamen[i].respuesta4,
                    preguntasExamen[i].respuestaCorrecta, preguntasExamen[i].idTema, respuestas[i]));
            }
            //servicioExamen.sendResultadoExamen(idAsignatura, nota, preguntasExamen.Count, failresponse);

            bd_preguntasrespuestas.insertAll(preguntasRespondidas);

            return nota;
        }

        public int getNumberOfQuestion()
        {
            return preguntasExamen.Count;
        }



        System.Windows.Visibility visibilidadMensaje;
        public System.Windows.Visibility VisibilidadMensaje
        {
            get
            {
                return visibilidadMensaje;
            }
            private set
            {
                visibilidadMensaje = value;

            }
        }

        string mensajeMostrar = "";

        public string MensajeMostrar
        {
            get
            {
                return mensajeMostrar;
            }
            private set
            {
                mensajeMostrar = value;

            }
        }

        private void mostarMensaje(string menssage)
        {
            VisibilidadMensaje = Visibility.Visible;
            mensajeMostrar = menssage;
            this.OnPropertyChanged("VisibilidadMensaje");
            this.OnPropertyChanged("MensajeMostrar");
        }

        private void ocultarMensaje()
        {
            visibilidadMensaje = Visibility.Collapsed;
            mensajeMostrar = "";
            this.OnPropertyChanged("VisibilidadMensaje");
            this.OnPropertyChanged("MensajeMostrar");
        }


    }
}
