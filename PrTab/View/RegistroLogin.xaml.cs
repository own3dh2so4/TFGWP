using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace PrTab.View
{
    public partial class RegistroLogin : PhoneApplicationPage
    {
        public RegistroLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Login.xaml", UriKind.Relative));
        }

        //Se sobre escribe la funcion que se llama cuando el usuario pulsa la tecla FISICA "Back", para que cierre la aplicación.
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //Do your work here
            base.OnBackKeyPress(e);
            IsolatedStorageSettings.ApplicationSettings.Save();
            Application.Current.Terminate();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Registrar.xaml", UriKind.Relative));
        }
    }
}