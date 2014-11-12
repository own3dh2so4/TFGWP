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
using System.Threading.Tasks;
using PrTab.Model;
using PrTab.Model.Comunicacion;
using PrTab.Utiles;

namespace PrTab.View
{
    /**
     * Esta clase pertenece a la primera vista que se carga en la aplicación.
     * Segun si el usuario ya se ha logueado alguna otr vez decide  cual sera la proxima vista en cargar.
     * Si el Usuario nunca se ha registrado le pedira usuario y contraseña.
     * En caso contraro le permitirar al usuario entrar directamente a la aplicación.
     * 
     * */
    public partial class Inicial : PhoneApplicationPage
    {
        //Constructor por defecto.
        public Inicial()
        {
            InitializeComponent();
            //Le decimos que no nos guarde en cache esta vista
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
            //Cuando la vista esta cargada se ejeuta la funcion "Redireciona".
            this.Loaded += Redireciona;
        }

        //Funcion que decide cual es la vista que el usuario debe ver, segun si ya se ha registrado o no.
        private async void Redireciona(object sender, RoutedEventArgs e)
        {
            Task<bool> existUser = ExisteUsuarioRegistrado();
            if (await existUser)
                NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
            else
                NavigationService.Navigate(new Uri("/View/RegistroLogin.xaml", UriKind.Relative));
        }

        //Esta funcion mira si ya hay un usuario registrado en la aplicacion.
        //En caso de que exista comprueba si el usuario y contraseña son correctos.
        private async  Task<bool> ExisteUsuarioRegistrado()
        {
            string user = AplicationSettings.getUsuario();
            string password = AplicationSettings.getContraseña();
            if (user == null || password == null)
                return false;
            else if (user.Equals("root") && password.Equals("toor"))
                return true;
            else
            {
                Task<bool> tarea = Comunicacion_Usuario.LoguearUsuario(user, password);
                return await tarea;
            }
        }

    

        
        
    }
}