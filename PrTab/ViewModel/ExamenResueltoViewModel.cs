using PrTab.Model.Base_de_Datos;
using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PrTab.ViewModel
{
    class ExamenResueltoViewModel :NotificationenabledObject
    {
        private List<PreguntaRespondida> preguntasExamen;
        private PreguntaRespondida preguntaMostrada;
        private int posicion;
        private string textoPreguntas = "";


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
                if (preguntasExamen == null)
                    return new SolidColorBrush(Colors.Transparent);
                else
                {
                    if (preguntaMostrada.respuestaCorrecta == 1)
                            return new SolidColorBrush(Colors.Green);
                    else if (preguntaMostrada.Respuesta == 1)
                            return new SolidColorBrush(Colors.Red);
                    else
                        return new SolidColorBrush(Colors.Transparent);
                }
                
            }
            
        }

        public SolidColorBrush ColorBoton2
        {
            get
            {
                if (preguntasExamen == null)
                    return new SolidColorBrush(Colors.Transparent);
                else
                {
                    if (preguntaMostrada.respuestaCorrecta == 2)
                            return new SolidColorBrush(Colors.Green);
                    else if (preguntaMostrada.Respuesta == 2)
                        return new SolidColorBrush(Colors.Red);
                    else
                        return new SolidColorBrush(Colors.Transparent);
                }

            }

        }

        public SolidColorBrush ColorBoton3
        {
            get
            {
                if (preguntasExamen == null)
                    return new SolidColorBrush(Colors.Transparent);
                else
                {
                    if (preguntaMostrada.respuestaCorrecta == 3)
                            return new SolidColorBrush(Colors.Green);
                    else if (preguntaMostrada.Respuesta == 3)
                            return new SolidColorBrush(Colors.Red);
                    else
                        return new SolidColorBrush(Colors.Transparent);
                }

            }

        }

        public SolidColorBrush ColorBoton4
        {
            get
            {
                if (preguntasExamen == null)
                    return new SolidColorBrush(Colors.Transparent);
                else
                {
                    if (preguntaMostrada.respuestaCorrecta == 4)
                        return new SolidColorBrush(Colors.Green);
                    else if (preguntaMostrada.Respuesta == 4)                   
                        return new SolidColorBrush(Colors.Red);
                    else
                        return new SolidColorBrush(Colors.Transparent);
                }

            }

        }

        public SolidColorBrush ColorBoton5
        {
            get
            {
                if (preguntasExamen == null)
                    return new SolidColorBrush(Colors.Transparent);
                else
                {
                    if (preguntaMostrada.respuestaCorrecta == 5)
                        return new SolidColorBrush(Colors.Green);
                    else if (preguntaMostrada.Respuesta == 5)
                        return new SolidColorBrush(Colors.Red);
                    else
                        return new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        public System.Windows.Visibility VisibilidadBoton3
        {
            get
            {
                if (preguntaMostrada.respuesta3 == "")
                    return System.Windows.Visibility.Collapsed;
                else
                    return System.Windows.Visibility.Visible;
            }
        }

        public System.Windows.Visibility VisibilidadBoton4
        {
            get
            {
                if (preguntaMostrada.respuesta4 == "")
                    return System.Windows.Visibility.Collapsed;
                else
                    return System.Windows.Visibility.Visible;
            }
        }

        public System.Windows.Visibility VisibilidadBoton5
        {
            get
            {
                if (preguntaMostrada.respuesta5 == "")
                    return System.Windows.Visibility.Collapsed;
                else
                    return System.Windows.Visibility.Visible;
            }
        }

        public PreguntaRespondida PreguntaMostrada
        {
            get { return preguntaMostrada; }
            set
            {
                preguntaMostrada = value;
                this.OnPropertyChanged("PreguntaMostrada");
            }
        }

        private CDB_PreguntasExamenRealizado bd_preguntasRespuestas = new CDB_PreguntasExamenRealizado();

        public ExamenResueltoViewModel()
        {
            preguntasExamen = bd_preguntasRespuestas.getExamenCorregido();
            if (preguntasExamen.Count!=0)
            {
                posicion = 0;
                PreguntaMostrada = preguntasExamen[0];
                NumeroPregunta = 1 + "/" + preguntasExamen.Count;
            }
        }

        public void siguientePregunta()
        {
            if (posicion < preguntasExamen.Count - 1 && posicion != -1)
            {
                posicion++;
                PreguntaMostrada = preguntasExamen[posicion];
                this.OnPropertyChanged("ColorBoton1");
                this.OnPropertyChanged("ColorBoton2");
                this.OnPropertyChanged("ColorBoton3");
                this.OnPropertyChanged("ColorBoton4");
                this.OnPropertyChanged("ColorBoton5");

                this.OnPropertyChanged("VisibilidadBoton3");
                this.OnPropertyChanged("VisibilidadBoton4");
                this.OnPropertyChanged("VisibilidadBoton5");

                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;

            }
        }

        public void anteriorPregunta()
        {
            if (posicion > 0)
            {
                posicion--;
                PreguntaMostrada = preguntasExamen[posicion];
                this.OnPropertyChanged("ColorBoton1");
                this.OnPropertyChanged("ColorBoton2");
                this.OnPropertyChanged("ColorBoton3");
                this.OnPropertyChanged("ColorBoton4");
                this.OnPropertyChanged("ColorBoton5");

                this.OnPropertyChanged("VisibilidadBoton3");
                this.OnPropertyChanged("VisibilidadBoton4");
                this.OnPropertyChanged("VisibilidadBoton5");

                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
            }
        }

    }
}
