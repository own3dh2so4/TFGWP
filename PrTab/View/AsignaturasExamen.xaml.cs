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
    public partial class AsignaturasExamen : PhoneApplicationPage
    {
        private AsignaturasExamenViewModel _viewModel;
        public AsignaturasExamen()
        {
            InitializeComponent();
           
            this.Loaded += InicializarViewModel;
        }

        private void InicializarViewModel(object sender, RoutedEventArgs e)
        {
            _viewModel = new AsignaturasExamenViewModel();
            ContentPanel.DataContext = _viewModel;
        }

        private void AddAplicationBar_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AgregarAsignaturaExamen.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
        }

        private void ListaAsignaturasExamen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myAsignatura = ((LongListSelector)sender).SelectedItem as Asignatura;
            string targetUri = string.Format("/View/Examen.xaml?idAsignatura={0}", myAsignatura.identificador);
            NavigationService.Navigate(new Uri(targetUri, UriKind.Relative));
        }
    }
}