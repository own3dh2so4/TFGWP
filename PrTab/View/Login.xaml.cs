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
using Windows.Phone.UI.Input;
using System.IO.IsolatedStorage;

namespace PrTab.View
{
    
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {        
            InitializeComponent();
        }

       

       

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            //Comprobar si el usuario y contraseña es el correcto.
           if (usuarioCorrecto(Usuario.Text, Contraseña.Password))
            {
                AplicationSettings.RegistrarUsuario(Usuario.Text, Contraseña.Password); 
                //NavigationService.Navigate(new Uri("/View/Inicial.xaml", UriKind.Relative));
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }


        private bool usuarioCorrecto(string user, string pass)
        {
            return user.Equals("qwe") && pass.Equals("qwe");
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Terminate();
        }

        

        //protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        //{
        //    int i = 0;
        //}

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //Do your work here
            base.OnBackKeyPress(e);
            IsolatedStorageSettings.ApplicationSettings.Save();
            Application.Current.Terminate();
        }

    }
}