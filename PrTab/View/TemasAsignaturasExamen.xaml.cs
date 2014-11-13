using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.ViewModel;
using PrTab.Model.Modelo;

namespace PrTab.View
{
    public partial class TemasAsignaturasExamen : PhoneApplicationPage
    {
        TemasAsignaturaViewModel _viewModel;
        string idAsignatura;

        public TemasAsignaturasExamen()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
        }

        private void Inicializa(object sender, RoutedEventArgs e)
        {
            _viewModel = new TemasAsignaturaViewModel(idAsignatura);
            ContentPanel.DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            idAsignatura = NavigationContext.QueryString["idAsignatura"];
        }

        private void ReloadAplicationBar_Click(object sender, EventArgs e)
        {
            _viewModel.cargarTemasServidor();
        }

        private void ListaTemasAsignatura_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myTema = ((LongListSelector)sender).SelectedItem as Tema;
            string targetUri = string.Format("/View/Examen.xaml?idAsignatura={0}&idTema={1}", idAsignatura,myTema.identificador+"" );
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }

        private void AllAplicationBar_Click(object sender, EventArgs e)
        {
            string targetUri = string.Format("/View/Examen.xaml?idAsignatura={0}&idTema={1}", idAsignatura, "");
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }
    }
}