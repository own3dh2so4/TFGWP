using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PrTab.View
{
   
    public partial class TipoDeExamen : PhoneApplicationPage
    {

         private string idAsignatura;
         private string idTema;
        public TipoDeExamen()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            idAsignatura = NavigationContext.QueryString["idAsignatura"];
            idTema = NavigationContext.QueryString["idTema"];
        }

        private void normal_Click(object sender, RoutedEventArgs e)
        {
            string targetUri = string.Format("/View/Examen.xaml?idAsignatura={0}&idTema={1}", idAsignatura, idTema + "");
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }

        private void multi_Click(object sender, RoutedEventArgs e)
        {
            string targetUri = string.Format("/View/ExamenMultiRespuesta.xaml?idAsignatura={0}&idTema={1}", idAsignatura, idTema + "");
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }

        private void cortas_Click(object sender, RoutedEventArgs e)
        {
            string targetUri = string.Format("/View/ExamenCortaRespuesta.xaml?idAsignatura={0}&idTema={1}", idAsignatura, idTema + "");
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }
    }

    
}