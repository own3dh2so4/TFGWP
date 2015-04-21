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

namespace PrTab.View
{
    public partial class ExamenResueltoPareja : PhoneApplicationPage
    {

        ExamenResueltoParejasViewModel _viewModel;
        public ExamenResueltoPareja()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
        }

        private void Inicializa(object sender, RoutedEventArgs e)
        {
            _viewModel = new ExamenResueltoParejasViewModel();
            GridPregunta.DataContext = _viewModel;
        }

        private void AppBarAnterior_Click(object sender, EventArgs e)
        {
            _viewModel.anterioPregunta();
        }

        private void AppBarSiguiente_Click(object sender, EventArgs e)
        {
            _viewModel.siguientePregunta();
        }

        private void AppBarCambiar_Click(object sender, EventArgs e)
        {
            _viewModel.cambioMostrar();
        }

        private void AppBarSalir_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
        }
    }
}