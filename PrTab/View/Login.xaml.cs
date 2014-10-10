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
    /**
     * Clase que contiene los componentes visuales que el usuario necesita para hacer login.
     * 
     * */
    public partial class Login : PhoneApplicationPage
    {
        //Constructor por defecto.
        public Login()
        {        
            InitializeComponent();
        }

       

       
        //Funcion que se llama cuando el usuario pulsa el boton "Aceptar".
        //Esta funcuion te redirecciona a la ventana principal de la aplicación si el usuario existe,
        //en caso contrario te muestra un mensaje de error.
        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            //Comprobar si el usuario y contraseña es el correcto.
           if (usuarioCorrecto(Usuario.Text, Contraseña.Password))
            {
                AplicationSettings.RegistrarUsuario(Usuario.Text, Contraseña.Password); 
                //NavigationService.Navigate(new Uri("/View/Inicial.xaml", UriKind.Relative));
                //NavigationService.GoBack();
                NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        //FUNCION PROVISIONAL.
        private bool usuarioCorrecto(string user, string pass)
        {
            return user.Equals("qwe") && pass.Equals("qwe");
        }

        //Funcion que se ejecuta cuando el usuario pulsa el boton cancelar.
        //Guarda los datos pendientes de guardar y cierra la aplicación.
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            //IsolatedStorageSettings.ApplicationSettings.Save();
            //Application.Current.Terminate();
            NavigationService.GoBack();
        }

        

   
        //Se sobre escribe la funcion que se llama cuando el usuario pulsa la tecla FISICA "Back", para que cierre la aplicación.
        /*protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //Do your work here
            base.OnBackKeyPress(e);
            IsolatedStorageSettings.ApplicationSettings.Save();
            Application.Current.Terminate();
        }*/

    }
}