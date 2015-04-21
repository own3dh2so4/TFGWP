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
    class ExamenResueltoParejasViewModel: NotificationenabledObject
    {
        private List<PreguntaParejasRespondida> preguntasExamen;
        private PreguntaParejasRespondida preguntaMostrada;
        private int poscion;
        private string textoPreguntas = "";

        private bool mostrarCorrecion = false;



        private SolidColorBrush colorVerde1 = new SolidColorBrush(Color.FromArgb(0xFF, 0, 0xFF, 0));
        private SolidColorBrush colorVerde2 = new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0x80, 0));
        private SolidColorBrush colorVerde3 = new SolidColorBrush(Color.FromArgb(0xFF, 0, 0x64, 0));
        private SolidColorBrush colorRojo1 = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
        private SolidColorBrush colorRojo2 = new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0, 0));
        private SolidColorBrush colorRojo3 = new SolidColorBrush(Color.FromArgb(0xFF, 0xFA, 0x80, 0x72));


        private CDB_PreguntasExamenRealizadoParejas bd_preguntasRespuestas = new CDB_PreguntasExamenRealizadoParejas();

        public void cambioMostrar()
        {
            mostrarCorrecion = !mostrarCorrecion;
            avisarCambios();
        }

        public ExamenResueltoParejasViewModel()
        {
            preguntasExamen = bd_preguntasRespuestas.getExamenCorregido();
            if( preguntasExamen.Count!=0)
            {
                poscion = 0;
                preguntaMostrada = preguntasExamen[0];
                NumeroPregunta = 1 + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }

        public PreguntaParejasRespondida PreguntaMostrada
        {
            get { return preguntaMostrada; }
            set
            {
                preguntaMostrada = value;
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

        public void avisarCambios()
        {
            this.OnPropertyChanged("ColorBoton1");
            this.OnPropertyChanged("ColorBoton2");
            this.OnPropertyChanged("ColorBoton3");
            this.OnPropertyChanged("ColorBoton4");
            this.OnPropertyChanged("ColorBoton5");
            this.OnPropertyChanged("ColorBoton6");


            this.OnPropertyChanged("PropietarioRespuestas");

            this.OnPropertyChanged("PreguntaMostrada");
        }

        public SolidColorBrush ColorBoton1
        {
            get
            {
                if(mostrarCorrecion)
                {
                    return colorVerde1;
                }
                else
                {
                    if (preguntaMostrada.r1 == 4)
                        return colorVerde1;
                    else
                        return colorRojo1;
                }
                
            }
        }

        public SolidColorBrush ColorBoton2
        {
            get
            {

                if (mostrarCorrecion)
                {
                    return colorVerde2;
                }
                else
                {
                    if (preguntaMostrada.r2 == 5)
                        return colorVerde2;
                    else
                        return colorRojo2;
                }
                
            }
        }

        public SolidColorBrush ColorBoton3
        {
            get
            {
                if (mostrarCorrecion)
                {
                    return colorVerde3;
                }
                else
                {
                    if (preguntaMostrada.r3 == 6)
                        return colorVerde3;
                    else
                        return colorRojo3;
                }
            }
        }

        public SolidColorBrush ColorBoton4
        {
            get
            {
                if (mostrarCorrecion)
                {
                    return colorVerde1;
                }
                else
                {

                    if (preguntaMostrada.r1 == 4)
                        return colorVerde1;
                    else
                    {
                        if (preguntaMostrada.r2 == 4)
                            return colorRojo2;
                        else
                            return colorRojo3;
                    }
                }
                    
            }
        }

        public SolidColorBrush ColorBoton5
        {
            get
            {
                if (mostrarCorrecion)
                {
                    return colorVerde2;
                }
                else
                {

                    if (preguntaMostrada.r2 == 5)
                        return colorVerde2;
                    else
                    {
                        if (preguntaMostrada.r1 == 5)
                            return colorRojo1;
                        else
                            return colorRojo3;
                    }
                }

            }
        }


        public SolidColorBrush ColorBoton6
        {
            get
            {
                if (mostrarCorrecion)
                {
                    return colorVerde3;
                }
                else
                {

                    if (preguntaMostrada.r3 == 6)
                        return colorVerde3;
                    else
                    {
                        if (preguntaMostrada.r2 == 6)
                            return colorRojo2;
                        else
                            return colorRojo1;
                    }
                }

            }
        }

        public void siguientePregunta()
        {
            if (poscion < preguntasExamen.Count - 1 && poscion != -1)
            {
                poscion++;
                preguntaMostrada = preguntasExamen[poscion];
                NumeroPregunta = (poscion + 1) + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }

        public void anterioPregunta()
        {
            if (poscion > 0)
            {
                poscion--;
                preguntaMostrada = preguntasExamen[poscion];
                NumeroPregunta = (poscion + 1) + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }
    }
}
