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
using System.Windows.Media;

namespace PrTab.View
{
    public partial class Examen : PhoneApplicationPage
    {
        string idAsignatura;
        string idTema;
        ExamenViewModel _viewModel;
        public Examen()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
           
        }

        private void Inicializa(object sender, RoutedEventArgs e)
        {
            _viewModel = new ExamenViewModel();
            GridPregunta.DataContext = _viewModel;
            if (idTema == "")
                _viewModel.setAsignatura(idAsignatura);
            else
                _viewModel.setTema(idAsignatura, idTema);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            idAsignatura = NavigationContext.QueryString["idAsignatura"];
            idTema = NavigationContext.QueryString["idTema"];
        }

        private void Respuesta1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.contestarPregunta(1);
            //_viewModel.siguientePregunta();
        }

        private void Respuesta2_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.contestarPregunta(2);
            //_viewModel.siguientePregunta();
        }

        private void Respuesta3_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.contestarPregunta(3);
            //_viewModel.siguientePregunta();
        }

        private void Respuesta4_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.contestarPregunta(4);
            //_viewModel.siguientePregunta();
        }

        private void AppBarAnterior_Click(object sender, EventArgs e)
        {
            _viewModel.anteriorPregunta();
        }

        private void AppBarSiguiente_Click(object sender, EventArgs e)
        {
            _viewModel.siguientePregunta();
        }


    }
}