using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.Model.Modelo;
using PrTab.Model.Comunicacion;
using PrTab.Utiles;

namespace PrTab.View
{
    public partial class ExamenCortaRespuesta : PhoneApplicationPage
    {

        private string idAsignatura;
        private string idTema;
        private List<PreguntaCortaRespuesta> preguntas;
        private string[] respuestas;
        private int posicion = -1;

        Comunicacion_Examen servicioExamen = new Comunicacion_Examen();
        public ExamenCortaRespuesta()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
        }

        private async void Inicializa(object sender, RoutedEventArgs e)
        {
            servicioExamen.getExanenCompletado += (s, a) =>
                  {
                      preguntas = a.preguntas.Cast<PreguntaCortaRespuesta>().ToList();
                      if(preguntas.Count>0)
                      {
                          respuestas = new string[preguntas.Count];
                          for (int i = 0; i < respuestas.Length; i++)
                            respuestas[i] = "";
                          posicion=0;
                          actualizaVista();
                          GridCargando.Visibility = System.Windows.Visibility.Collapsed;
                      }

                  };
            if(idTema=="")
            {
                await servicioExamen.getExamenCortas(idAsignatura, AplicationSettings.getNumeroDePreguntasExamen());
            }
            else
            {
                await servicioExamen.getExamenCortas(idAsignatura, idTema, AplicationSettings.getNumeroDePreguntasExamen());
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            idAsignatura = NavigationContext.QueryString["idAsignatura"];
            idTema = NavigationContext.QueryString["idTema"];
        }

        private void actualizaVista()
        {
            if(posicion>=0 && posicion<preguntas.Count)
            {
                numeroPregunta.Text = (posicion + 1) + "/" + preguntas.Count;
                enunciado.Text = preguntas[posicion].enunciado;
                Respuesta.Text = respuestas[posicion];
                GridPregunta.UpdateLayout();
            }
        }

        private void AppBarAnterior_Click(object sender, EventArgs e)
        {
            if (posicion-1 >= 0)
            {
                posicion--;
                actualizaVista();
            }
        }

        private void AppBarSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion+1 < preguntas.Count)
            {
                posicion++;
                actualizaVista();
            }
        }

        private void AppBarCorregir_Click(object sender, EventArgs e)
        {
            //TODO esto queda pendiente
        }
    }
}