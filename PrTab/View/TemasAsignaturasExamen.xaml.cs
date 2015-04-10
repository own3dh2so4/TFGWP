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
using PrTab.Model.Base_de_Datos;

namespace PrTab.View
{
    public partial class TemasAsignaturasExamen : PhoneApplicationPage
    {
        TemasAsignaturaViewModel _viewModel;
        CDB_AsignaturaExamen BD_AE = new CDB_AsignaturaExamen();
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
            var asignatura = BD_AE.getAsignaturaExamen(idAsignatura);
            NombreAsignatura.Text = asignatura.nombre;

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
            string targetUri = string.Format("/View/TipoDeExamen.xaml?idAsignatura={0}&idTema={1}", idAsignatura, myTema.identificador + "");
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }

        private void AllAplicationBar_Click(object sender, EventArgs e)
        {
            string targetUri = string.Format("/View/TipoDeExamen.xaml?idAsignatura={0}&idTema={1}", idAsignatura, "");
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }
    }
}