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
    public partial class ExamenResuelto : PhoneApplicationPage
    {
        ExamenResueltoViewModel _viewModel;

        public ExamenResuelto()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
        }

        private void Inicializa(object sender, RoutedEventArgs e)
        {
            _viewModel = new ExamenResueltoViewModel();
            GridPregunta.DataContext = _viewModel;
        }

        private void AppBarAnterior_Click(object sender, EventArgs e)
        {
            _viewModel.anteriorPregunta();
        }

        private void AppBarSiguiente_Click(object sender, EventArgs e)
        {
            _viewModel.siguientePregunta();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
        }

        private void AppBarSalir_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
        }
    }
}