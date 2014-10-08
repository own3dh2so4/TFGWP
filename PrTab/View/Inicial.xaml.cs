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
    public partial class Inicial : PhoneApplicationPage
    {
        public Inicial()
        {
            InitializeComponent();
            this.Loaded += Redireciona;
        }

        private void Redireciona(object sender, RoutedEventArgs e)
        {
            if (ExisteUsuarioRegistrado())
                NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
            else
                NavigationService.Navigate(new Uri("/View/Login.xaml", UriKind.Relative));
        }

        private bool ExisteUsuarioRegistrado()
        {
            string user = AplicationSettings.getUsuario();
            string password = AplicationSettings.getContraseña();
            if (user == null || password == null)
                return false;
            else if (usuarioCorrecto(user, password))
            {
                return true;
            }
            else
                return false;
        }

        private bool usuarioCorrecto(string user, string pass)
        {
            return user.Equals("qwe") && pass.Equals("qwe");
        }
    }
}