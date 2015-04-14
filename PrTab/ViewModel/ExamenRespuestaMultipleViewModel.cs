using PrTab.Model.Base_de_Datos;
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
    class ExamenRespuestaMultipleViewModel : NotificationenabledObject
    {
        private List<PreguntaMultirespuesta> preguntasExamen;
        private List<bool[]> respuestas;
        private SolidColorBrush[,] colorBotones;
        public PreguntaMultirespuesta preguntaMostrada;
        private int posicion;
        private string textoPreguntas = "";
        private string idAsignatura = "";
        private CDB_PreguntasExamenRealizadoMulti bd_preguntasrespuestas = new CDB_PreguntasExamenRealizadoMulti();
        private Stopwatch tiempoTranscurrido;
        private Visibility[] visibilidadBotones = new Visibility[3];


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

        public System.Windows.Visibility VisibilidadBoton3
        {
            get
            {
                if (visibilidadBotones[0] == null)
                    return Visibility.Collapsed;
                return visibilidadBotones[0];

            }
            private set
            {
                if (preguntaMostrada.respuesta3 == "")
                    visibilidadBotones[0] = Visibility.Collapsed;
                else
                    visibilidadBotones[0] = Visibility.Visible;
                this.OnPropertyChanged("VisibilidadBoton3");
            }
        }

        public System.Windows.Visibility VisibilidadBoton4
        {
            get
            {
                if (visibilidadBotones[1] == null)
                    return Visibility.Collapsed;
                return visibilidadBotones[1];

            }
            private set
            {
                if (preguntaMostrada.respuesta4 == "")
                    visibilidadBotones[1] = Visibility.Collapsed;
                else
                    visibilidadBotones[1] = Visibility.Visible;
                this.OnPropertyChanged("VisibilidadBoton4");
            }
        }
        public System.Windows.Visibility VisibilidadBoton5
        {
            get
            {
                if (visibilidadBotones[2] == null)
                    return Visibility.Collapsed;
                return visibilidadBotones[2];

            }
            private set
            {
                if (preguntaMostrada.respuesta5 == "")
                    visibilidadBotones[2] = Visibility.Collapsed;
                else
                    visibilidadBotones[2] = Visibility.Visible;
                this.OnPropertyChanged("VisibilidadBoton5");
            }
        }

        public void avisarCambioPreguntaVisivilidad()
        {

            VisibilidadBoton3 = Visibility.Collapsed;
            VisibilidadBoton4 = Visibility.Collapsed;
            VisibilidadBoton5 = Visibility.Collapsed;
        }

        public PreguntaMultirespuesta PreguntaMostrada
        {
            get { return preguntaMostrada; }
            set
            {
                preguntaMostrada = value;
                this.OnPropertyChanged("PreguntaMostrada");
            }
        }

        Comunicacion_Examen servicioExamen = new Comunicacion_Examen();


        public ExamenRespuestaMultipleViewModel()
        {
            preguntasExamen = new List<PreguntaMultirespuesta>();
            posicion = -1;
            tiempoTranscurrido = new Stopwatch();
            servicioExamen.getExanenCompletado += (s, a) =>
                {
                    preguntasExamen = a.preguntas.Cast<PreguntaMultirespuesta>().ToList();
                    if(preguntasExamen.Count!=0)
                    {
                        PreguntaMostrada = preguntasExamen[0];
                        posicion = 0;
                    }
                    NumeroPregunta = 1 + "/" + preguntasExamen.Count;
                    respuestas = new List<bool[]>();
                    colorBotones = new SolidColorBrush[preguntasExamen.Count, 5];
                    for(int i=0; i<preguntasExamen.Count; i++)
                    {
                        respuestas.Add(new bool[5]);
                        for (int j = 0; j <= 4; j++)
                        {
                            colorBotones[i, j] = new SolidColorBrush(Colors.Transparent);
                            respuestas.ElementAt(i)[j] = false;
                        }
                    }
                    avisarCambioPreguntaVisivilidad();
                };
        }

        public async void setAsignatura(string asignatura)
        {
            mostarMensaje("Cargando Examen");
            //Aun que el visual se queja de esto, asi esta bien.
            await servicioExamen.getExamenMulti(asignatura, AplicationSettings.getNumeroDePreguntasExamen());
            idAsignatura = asignatura;
            ocultarMensaje();
            tiempoTranscurrido.Start();

        }

        public async void setTema(string asignatura, string idTema)
        {
            mostarMensaje("Cargando Examen");
            await servicioExamen.getExamenMulti(asignatura, idTema, AplicationSettings.getNumeroDePreguntasExamen());
            idAsignatura = asignatura;
            ocultarMensaje();
            tiempoTranscurrido.Start();
        }

        public void contestarPregunta(int resp)
        {
            if ((resp == 1 || resp == 2 || resp == 3 || resp == 4 || resp == 5))
            {
                respuestas.ElementAt(posicion)[resp-1] = !respuestas.ElementAt(posicion)[resp-1];
                if(respuestas.ElementAt(posicion)[resp-1])
                {
                    switch (resp)
                    {
                        case 1: ColorBoton1 = new SolidColorBrush(Colors.Orange); break;
                        case 2: ColorBoton2 = new SolidColorBrush(Colors.Orange); break;
                        case 3: ColorBoton3 = new SolidColorBrush(Colors.Orange); break;
                        case 4: ColorBoton4 = new SolidColorBrush(Colors.Orange); break;
                        case 5: ColorBoton5 = new SolidColorBrush(Colors.Orange); break;
                    }
                }
                else
                {
                    switch (resp)
                    {
                        case 1: ColorBoton1 = new SolidColorBrush(Colors.Transparent); break;
                        case 2: ColorBoton2 = new SolidColorBrush(Colors.Transparent); break;
                        case 3: ColorBoton3 = new SolidColorBrush(Colors.Transparent); break;
                        case 4: ColorBoton4 = new SolidColorBrush(Colors.Transparent); break;
                        case 5: ColorBoton5 = new SolidColorBrush(Colors.Transparent); break;
                    }
                }
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
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
                avisarCambioPreguntaVisivilidad();
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
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
                avisarCambioPreguntaVisivilidad();
            }
        }

        public int evaluarExamen()
        {
            tiempoTranscurrido.Stop();
            int nota = 0;
            List<PreguntaRespondidaInterface> preguntasRespondidas = new List<PreguntaRespondidaInterface>();
            bd_preguntasrespuestas.deleteAll();
            for (int i = 0; i < preguntasExamen.Count; i++ )
            {
                if (respuestas[i][0] == preguntasExamen[i].correcta1 && respuestas[i][1] == preguntasExamen[i].correcta2 && respuestas[i][2] == preguntasExamen[i].correcta3 &&
                    respuestas[i][3] == preguntasExamen[i].correcta4 && respuestas[i][4] == preguntasExamen[i].correcta5)
                {
                    nota++;
                }
                List<int> r = new List<int>();
                if(respuestas[i][0]) r.Add(1);
                if(respuestas[i][1]) r.Add(2);
                if(respuestas[i][2]) r.Add(3);
                if(respuestas[i][3]) r.Add(4);
                if(respuestas[i][4]) r.Add(5);
                preguntasRespondidas.Add(new PreguntaMultiRespondida(preguntasExamen[i].identificador, preguntasExamen[i].enunciado, preguntasExamen[i].respuesta1, preguntasExamen[i].respuesta2,
                    preguntasExamen[i].respuesta3, preguntasExamen[i].respuesta4, preguntasExamen[i].respuesta5, preguntasExamen[i].getRepuestasCorrectas(), preguntasExamen[i].idTema, r));
            }
            servicioExamen.sendResultadoExamen(idAsignatura, nota, preguntasExamen.Count, preguntasRespondidas, tiempoTranscurrido.ElapsedMilliseconds, "ma");
            bd_preguntasrespuestas.insertAll(preguntasRespondidas.Cast<PreguntaMultiRespondida>().ToList());

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
