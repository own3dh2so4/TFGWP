﻿using System;
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
    public partial class EvaluacionExamen : PhoneApplicationPage
    {
        private string numCorectas;
        private string numPreguntas;

        public EvaluacionExamen()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
        }

        private void Inicializa(object sender, RoutedEventArgs e)
        {
            Nota.Text = numCorectas + "/" + numPreguntas;
            double notaSobreDiez = 10 * Convert.ToInt32(numCorectas) / Convert.ToInt32(numPreguntas);
            if (notaSobreDiez < 5)
                MensajeAnimo.Text = "Con mas esfuerzo aprobaras";
            else if (notaSobreDiez<7)
                MensajeAnimo.Text = "No esta mal, pero deberias mejorar";
            else if (notaSobreDiez<9)
                MensajeAnimo.Text = "Muy bien, pero no te conformes";
            else if (notaSobreDiez<10)
                MensajeAnimo.Text = "Casi perfecto, ya estas rozando la perfección";
            else if (notaSobreDiez==10)
                MensajeAnimo.Text = "Enhorabuena, buen trabajo";
                
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            numCorectas = NavigationContext.QueryString["numCorrectas"];
            numPreguntas = NavigationContext.QueryString["numPreguntas"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
        }
    }
}