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
    class ExamenResueltoCortaViewModel : NotificationenabledObject
    {

        private List<PreguntaRespondidaCorta> preguntasExamen;
        private PreguntaRespondidaCorta preguntaMostrada;
        private int posicion;
        private string textoPreguntas = "";

        private SolidColorBrush colorRed = new SolidColorBrush(Colors.Red);
        private SolidColorBrush colorGreen = new SolidColorBrush(Colors.Green);

        private CDB_PreguntasExamenRealizadoCortas bd_preguntasRespuestas = new CDB_PreguntasExamenRealizadoCortas();

        public string NumeroPregunta
        {
            get
            {
                return textoPreguntas;
            }
            private set
            {
                textoPreguntas = value;                
            }
        }

        public PreguntaRespondidaCorta PreguntaMostrada
        {
            get { return preguntaMostrada; }
            set
            {
                preguntaMostrada = value;
            }
        }
        public SolidColorBrush ColorGrid
        {
            get
            {
                if(preguntaMostrada.respuestaCorrecta.ToUpper() == preguntaMostrada.respuesta.ToUpper())
                {
                    return colorGreen;
                }
                else
                {
                    return colorRed;
                }                
            }            
        }

        public ExamenResueltoCortaViewModel()
        {
            preguntasExamen = bd_preguntasRespuestas.getExamenCorregido();
            if (preguntasExamen.Count!=0)
            {
                posicion = 0;
                PreguntaMostrada = preguntasExamen[0];
                NumeroPregunta = 1 + "/" + preguntasExamen.Count;
            }
        }

        public void avisarCambios()
        {
            this.OnPropertyChanged("ColorGrid");

            this.OnPropertyChanged("PreguntaMostrada");

            this.OnPropertyChanged("NumeroPregunta");
        }

        public void siguientePregunta()
        {
            if (posicion < preguntasExamen.Count - 1 && posicion != -1)
            {
                posicion++;
                preguntaMostrada = preguntasExamen[posicion];
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }

        public void anterioPregunta()
        {
            if (posicion > 0)
            {
                posicion--;
                preguntaMostrada = preguntasExamen[posicion];
                NumeroPregunta = (posicion + 1) + "/" + preguntasExamen.Count;
                avisarCambios();
            }
        }

    }
}
