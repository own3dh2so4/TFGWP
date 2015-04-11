using PrTab.Model.Comunicacion;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PrTab.ViewModel
{
    class ExamenRespuestaParejasViewModel : NotificationenabledObject
    {
        private List<PreguntaParejaRespuesta> preguntasExamen;
        private List<int[]> respuestas;
        private SolidColorBrush[,] colorBotones;
        private PreguntaParejaRespuesta preguntaMostrada;
        int posicion;
        private string textoPreguntas = "";
        private string idAsignatura = "";
        //Aqui la base de datos
        private Stopwatch tiempoTranscurrido;

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
            get
            {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 0];
            }
            private set
            {
                colorBotones[posicion, 0] = value;
                this.OnPropertyChanged("ColorBoton1");
            }
        }

        public SolidColorBrush ColorBoton2
        {
            get
            {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 1];
            }
            private set
            {
                colorBotones[posicion, 1] = value;
                this.OnPropertyChanged("ColorBoton2");
            }
        }

        public SolidColorBrush ColorBoton3
        {
            get
            {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 2];
            }
            private set
            {
                colorBotones[posicion, 2] = value;
                this.OnPropertyChanged("ColorBoton3");
            }
        }

        public SolidColorBrush ColorBoton4
        {
            get
            {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 3];
            }
            private set
            {
                colorBotones[posicion, 3] = value;
                this.OnPropertyChanged("ColorBoton4");
            }
        }

        public SolidColorBrush ColorBoton5
        {
            get
            {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 4];
            }
            private set
            {
                colorBotones[posicion, 4] = value;
                this.OnPropertyChanged("ColorBoton5");
            }
        }

        public SolidColorBrush ColorBoton6
        {
            get
            {
                if (colorBotones == null)
                    return new SolidColorBrush(Colors.Transparent);
                return colorBotones[posicion, 5];
            }
            private set
            {
                colorBotones[posicion, 5] = value;
                this.OnPropertyChanged("ColorBoton6");
            }
        }

        public PreguntaParejaRespuesta PreguntaMostrada
        {
            get { return preguntaMostrada; }
            set
            {
                preguntaMostrada = value;
                this.OnPropertyChanged("PreguntaMostrada");
            }
        }

        Comunicacion_Examen servicioExamen = new Comunicacion_Examen();

        public ExamenRespuestaParejasViewModel()
        {
            preguntasExamen = new List<PreguntaParejaRespuesta>();
            posicion = -1;
            tiempoTranscurrido = new Stopwatch();
            servicioExamen.getExanenCompletado += (s, a) =>
                  {
                      preguntasExamen = a.preguntas.Cast<PreguntaParejaRespuesta>().ToList();
                      if(preguntasExamen.Count!=0)
                      {
                          PreguntaMostrada = preguntasExamen[0];
                          posicion = 0;
                      }
                      NumeroPregunta=1+"/"+preguntasExamen.Count;
                      respuestas = new List<int[]>();
                      colorBotones = new SolidColorBrush[preguntasExamen.Count,6];
                      for (int i = 0; i < preguntasExamen.Count; i++)
                      {
                          respuestas.Add(new int[6]);
                          //Pongo el fondo de los botones transparente.
                          for (int j = 0; j<=5; j++)
                          {
                              colorBotones[i, j] = new SolidColorBrush(Colors.Transparent);
                              respuestas[i][j] = 0;
                          }
                      }
                  };
        }

        public async void setAsignatura(string asignatura)
        {
            mostarMensaje("Cargando Examen");
            //Aun que el visual se queja de esto, asi esta bien.
            await servicioExamen.getExamenParejas(asignatura, AplicationSettings.getNumeroDePreguntasExamen());
            idAsignatura = asignatura;
            ocultarMensaje();
            tiempoTranscurrido.Start();

        }

        public async void setTema(string asignatura, string idTema)
        {
            mostarMensaje("Cargando Examen");
            await servicioExamen.getExamenParejas(asignatura, idTema, AplicationSettings.getNumeroDePreguntasExamen());
            idAsignatura = asignatura;
            ocultarMensaje();
            tiempoTranscurrido.Start();
        }

        private bool derechaPulsado = false;
        private bool izquierdaPulsado = false;
        private int botonIzqPulsado = 0;
        private int botonDerPulsado = 0;

        public void contestarPregunta(int resp)
        {
            if(!derechaPulsado && !izquierdaPulsado)
            {
                switchColorBotones(resp, Colors.Blue);
                if(respuestas[posicion][resp-1] != 0)
                {
                    switchColorBotones(respuestas[posicion][resp - 1], Colors.Transparent);
                    respuestas[posicion][respuestas[posicion][resp - 1]-1] = 0;
                    respuestas[posicion][resp - 1] = 0;
                }
                if(resp>3)
                {
                    botonDerPulsado = resp;
                    derechaPulsado = true;
                }
                else
                {
                    botonIzqPulsado = resp;
                    izquierdaPulsado = true;
                }
            }
            else
            {
                if (izquierdaPulsado)
                {
                    if (resp>3)
                    {
                        switch(botonIzqPulsado)
                        {
                            case 1: switchColorBotones(1, Colors.Purple);
                                switchColorBotones(resp, Colors.Purple);
                                break;
                            case 2: switchColorBotones(2, Colors.Orange);
                                switchColorBotones(resp, Colors.Orange);
                                break;
                            case 3: switchColorBotones(3, Colors.Magenta);
                                switchColorBotones(resp, Colors.Magenta);
                                break;
                        }
                        if (respuestas[posicion][resp - 1] != 0)
                        {
                            switchColorBotones(respuestas[posicion][resp - 1], Colors.Transparent);
                            respuestas[posicion][respuestas[posicion][resp - 1] - 1] = 0;
                            respuestas[posicion][resp - 1] = 0;
                        }
                        respuestas[posicion][resp-1] = botonIzqPulsado;
                        respuestas[posicion][botonIzqPulsado-1] = resp;
                        izquierdaPulsado = false;
                        botonIzqPulsado = 0;

                    }
                    else
                    {
                        switchColorBotones(botonIzqPulsado, Colors.Transparent);
                        switchColorBotones(resp, Colors.Blue);
                        botonIzqPulsado = resp;
                        if (respuestas[posicion][resp - 1] != 0)
                        {
                            switchColorBotones(respuestas[posicion][resp - 1], Colors.Transparent);
                            respuestas[posicion][respuestas[posicion][resp - 1] - 1] = 0;
                            respuestas[posicion][resp - 1] = 0;
                        }
                    }
                }
                else
                {
                    if(resp<=3)
                    {
                        switch (botonDerPulsado)
                        {
                            case 4: switchColorBotones(4, Colors.Green);
                                switchColorBotones(resp, Colors.Green);
                                break;
                            case 5: switchColorBotones(5, Colors.Yellow);
                                switchColorBotones(resp, Colors.Yellow);
                                break;
                            case 6: switchColorBotones(6, Colors.Brown);
                                switchColorBotones(resp, Colors.Brown);
                                break;
                        }
                        if (respuestas[posicion][resp - 1] != 0)
                        {
                            switchColorBotones(respuestas[posicion][resp - 1], Colors.Transparent);
                            respuestas[posicion][respuestas[posicion][resp - 1] - 1] = 0;
                            respuestas[posicion][resp - 1] = 0;
                        }
                        respuestas[posicion][resp-1] = botonDerPulsado;
                        respuestas[posicion][botonDerPulsado-1] = resp;
                        derechaPulsado = false;
                        botonDerPulsado = 0;
                    }
                    else
                    {
                        switchColorBotones(botonDerPulsado, Colors.Transparent);
                        switchColorBotones(resp, Colors.Blue);
                        botonDerPulsado = resp;
                        if (respuestas[posicion][resp - 1] != 0)
                        {
                            switchColorBotones(respuestas[posicion][resp - 1], Colors.Transparent);
                            respuestas[posicion][respuestas[posicion][resp - 1] - 1] = 0;
                            respuestas[posicion][resp - 1] = 0;
                        }
                    }
                }
            }
        }


        private void switchColorBotones(int i, Color c)
        {
            switch (i)
            {
                case 1: ColorBoton1 = new SolidColorBrush(c); break;
                case 2: ColorBoton2 = new SolidColorBrush(c); break;
                case 3: ColorBoton3 = new SolidColorBrush(c); break;
                case 4: ColorBoton4 = new SolidColorBrush(c); break;
                case 5: ColorBoton5 = new SolidColorBrush(c); break;
                case 6: ColorBoton6 = new SolidColorBrush(c); break;
            }
        }

        public void siguientePregunta()
        {
            if (posicion < preguntasExamen.Count - 1 && posicion != -1)
            {
                posicion++;
                PreguntaMostrada = preguntasExamen[posicion];
                ColorBoton1 = colorBotones[posicion, 0];
                ColorBoton2 = colorBotones[posicion, 1];
                ColorBoton3 = colorBotones[posicion, 2];
                ColorBoton4 = colorBotones[posicion, 3];
                ColorBoton5 = colorBotones[posicion, 4];
                ColorBoton6 = colorBotones[posicion, 5];
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
            }
        }

        public void anteriorPregunta()
        {
            if (posicion > 0)
            {
                posicion--;
                PreguntaMostrada = preguntasExamen[posicion];
                ColorBoton1 = colorBotones[posicion, 0];
                ColorBoton2 = colorBotones[posicion, 1];
                ColorBoton3 = colorBotones[posicion, 2];
                ColorBoton4 = colorBotones[posicion, 3];
                ColorBoton5 = colorBotones[posicion, 4];
                ColorBoton6 = colorBotones[posicion, 5];
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
            }
        }

        public int evaluarExamen()
        {
            return 0;
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
