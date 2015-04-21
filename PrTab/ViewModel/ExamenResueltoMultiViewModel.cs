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
    class ExamenResueltoMultiViewModel : NotificationenabledObject
    {
        private List<PreguntaMultiRespondida> preguntasExamen;
        private PreguntaMultiRespondida preguntaMostrada;
        private int poscion;
        private string textoPreguntas = "";

        private bool mostrarCorrecion = false;

        private SolidColorBrush colorVerde = new SolidColorBrush(Colors.Green);
        private SolidColorBrush colorRojo = new SolidColorBrush(Colors.Red);
        private SolidColorBrush colorNaranja = new SolidColorBrush(Colors.Orange);
        private SolidColorBrush colorTransparente = new SolidColorBrush(Colors.Transparent);

        private CDB_PreguntasExamenRealizadoMulti bd_preguntasRespuestas = new CDB_PreguntasExamenRealizadoMulti();

        public ExamenResueltoMultiViewModel()
        {
            preguntasExamen = bd_preguntasRespuestas.getExamenCorregido();
            if(preguntasExamen.Count!=0)
            {
                poscion = 0;
                preguntaMostrada = preguntasExamen[0];
                NumeroPregunta = 1 + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }

        public void cambioMostrar()
        {
            mostrarCorrecion = !mostrarCorrecion;
            avisarCambios();
        }

        public string PropietarioRespuestas
        {
            get
            {
                if (mostrarCorrecion)
                    return "Correcto";
                else
                    return "Tus respuestas";
            }
        }



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
                if (preguntaMostrada == null)
                    return colorTransparente;
                else
                {
                    if(mostrarCorrecion)
                    {
                        if(preguntaMostrada.correcta1)
                            return colorVerde;
                        else
                            return colorTransparente;
                    }
                    else
                    {
                        if(preguntaMostrada.correcta1)
                        {
                            if (preguntaMostrada.respondio1)
                                return colorVerde;
                            else
                                return colorNaranja;
                        }
                        else
                        {
                            if (preguntaMostrada.respondio1)
                                return colorRojo;
                            else
                                return colorTransparente;
                        }
                    }
                    
                }
            }
        }


        public SolidColorBrush ColorBoton2
        {
            get
            {
                if (preguntaMostrada == null)
                    return colorTransparente;
                else
                {
                    if (mostrarCorrecion)
                    {
                        if (preguntaMostrada.correcta2)
                            return colorVerde;
                        else
                            return colorTransparente;
                    }
                    else
                    {
                        if (preguntaMostrada.correcta2)
                        {
                            if (preguntaMostrada.respondio2)
                                return colorVerde;
                            else
                                return colorNaranja;
                        }
                        else
                        {
                            if (preguntaMostrada.respondio2)
                                return colorRojo;
                            else
                                return colorTransparente;
                        }
                    }

                }
            }
        }


        public SolidColorBrush ColorBoton3
        {
            get
            {
                if (preguntaMostrada == null)
                    return colorTransparente;
                else
                {
                    if (mostrarCorrecion)
                    {
                        if (preguntaMostrada.correcta3)
                            return colorVerde;
                        else
                            return colorTransparente;
                    }
                    else
                    {
                        if (preguntaMostrada.correcta3)
                        {
                            if (preguntaMostrada.respondio3)
                                return colorVerde;
                            else
                                return colorNaranja;
                        }
                        else
                        {
                            if (preguntaMostrada.respondio3)
                                return colorRojo;
                            else
                                return colorTransparente;
                        }
                    }

                }
            }
        }

        public SolidColorBrush ColorBoton4
        {
            get
            {
                if (preguntaMostrada == null)
                    return colorTransparente;
                else
                {
                    if (mostrarCorrecion)
                    {
                        if (preguntaMostrada.correcta4)
                            return colorVerde;
                        else
                            return colorTransparente;
                    }
                    else
                    {
                        if (preguntaMostrada.correcta4)
                        {
                            if (preguntaMostrada.respondio4)
                                return colorVerde;
                            else
                                return colorNaranja;
                        }
                        else
                        {
                            if (preguntaMostrada.respondio4)
                                return colorRojo;
                            else
                                return colorTransparente;
                        }
                    }

                }
            }
        }


        public SolidColorBrush ColorBoton5
        {
            get
            {
                if (preguntaMostrada == null)
                    return colorTransparente;
                else
                {
                    if (mostrarCorrecion)
                    {
                        if (preguntaMostrada.correcta5)
                            return colorVerde;
                        else
                            return colorTransparente;
                    }
                    else
                    {
                        if (preguntaMostrada.correcta5)
                        {
                            if (preguntaMostrada.respondio5)
                                return colorVerde;
                            else
                                return colorNaranja;
                        }
                        else
                        {
                            if (preguntaMostrada.respondio5)
                                return colorRojo;
                            else
                                return colorTransparente;
                        }
                    }

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

        public void avisarCambios()
        {
            this.OnPropertyChanged("ColorBoton1");
            this.OnPropertyChanged("ColorBoton2");
            this.OnPropertyChanged("ColorBoton3");
            this.OnPropertyChanged("ColorBoton4");
            this.OnPropertyChanged("ColorBoton5");

            this.OnPropertyChanged("VisibilidadBoton3");
            this.OnPropertyChanged("VisibilidadBoton4");
            this.OnPropertyChanged("VisibilidadBoton5");

            this.OnPropertyChanged("PropietarioRespuestas");

            this.OnPropertyChanged("PreguntaMostrada");
        }

        public PreguntaMultiRespondida PreguntaMostrada
        {
            get { return preguntaMostrada; }
            set
            {
                preguntaMostrada = value;
                this.OnPropertyChanged("PreguntaMostrada");
            }
        }

        public void siguientePregunta()
        {
            if(poscion < preguntasExamen.Count - 1 && poscion !=-1)
            {
                poscion++;
                preguntaMostrada = preguntasExamen[poscion];
                NumeroPregunta = (poscion + 1) + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }

        public void anterioPregunta()
        {
            if(poscion > 0)
            {
                poscion--;
                preguntaMostrada = preguntasExamen[poscion];
                NumeroPregunta = (poscion + 1) + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }


    }
}
