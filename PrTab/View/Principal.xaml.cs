using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.Resources;
using Windows.Phone.UI.Input;
using System.IO.IsolatedStorage;
using PrTab.Model;
using System.Windows.Media;
using PrTab.ViewModel;
using System.Windows.Data;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using System.Windows.Media.Imaging;

namespace PrTab
{
    /**
     * Clase con de la vista principal de la aplicación.
     * 
     * */
    public partial class MainPage : PhoneApplicationPage
    {
        MensajesTablonViewModel _viewModelTablonMensaje;

        private Button botonAmor;

        private bool botonFavPulsado = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //_viewModelTablonMensaje = (MensajesTablonViewModel)Resources["ViewModelTablon"];
            this.Loaded += InicializarViewModel;
            

            // Código de ejemplo para traducir ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void InicializarViewModel(object sender, RoutedEventArgs e)
        {
            _viewModelTablonMensaje = new MensajesTablonViewModel();
            GridMensajes.DataContext = _viewModelTablonMensaje;
        }


        

        // Código de ejemplo para compilar una ApplicationBar traducida
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Establecer ApplicationBar de la página en una nueva instancia de ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crear un nuevo botón y establecer el valor de texto en la cadena traducida de AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crear un nuevo elemento de menú con la cadena traducida de AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        //Se sobre escribe la funcion que se llama cuando el usuario pulsa la tecla FISICA "Back", para que cierre la aplicación.
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //Do your work here
            base.OnBackKeyPress(e);
            IsolatedStorageSettings.ApplicationSettings.Save();
            Application.Current.Terminate();
        }

        private void MensajeTablonSelected(object sender, SelectionChangedEventArgs e)
        {

            if (((LongListSelector)sender).SelectedItem != null)
            {
                if (botonFavPulsado)
                {

                    var myItem = ((LongListSelector)sender).SelectedItem as MensajeTablon;

                    _viewModelTablonMensaje.addFavMessage(myItem);

                    if (botonAmor != null)
                    {
                        if (myItem.userFav)
                        {
                            botonAmor.Content = new Image
                            {
                                Source = new BitmapImage(new Uri("icons/heart.red.png", UriKind.Relative)),
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center
                                ,
                                MaxHeight = 75,
                                MaxWidth = 75,
                                MinHeight = 75,
                                MinWidth = 75
                            };
                        }
                        else
                        {
                            botonAmor.Content = new Image
                            {
                                Source = new BitmapImage(new Uri("icons/heart.white.png", UriKind.Relative)),
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center
                                ,
                                MaxHeight = 75,
                                MaxWidth = 75,
                                MinHeight = 75,
                                MinWidth = 75
                            };
                        }
                    }
                    botonFavPulsado = false;
                    ((LongListSelector)sender).SelectedItem = null;
                }
                else
                {               
                    var myItem = ((LongListSelector)sender).SelectedItem as MensajeTablon;
                    NavigationService.Navigate(new Uri("/View/MensajeTablon.xaml?idMessage=" + myItem.identificador , UriKind.Relative));
                }
            }
            
            
        }

        private void destrucion_Click(object sender, RoutedEventArgs e)
        {
            AplicationSettings.logout();
            IsolatedStorageSettings.ApplicationSettings.Save();
            Application.Current.Terminate();
        }

        private void TextoNuevoMensaje_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TextoNuevoMensaje.Text == "Escribe tu mensaje")
            {
                TextoNuevoMensaje.Text = "";
            }
        }

        private void TextoNuevoMensaje_LostFocus(object sender, RoutedEventArgs e)
        {
            if(TextoNuevoMensaje.Text == "")
            {
               TextoNuevoMensaje.Text = "Escribe tu mensaje";
            }
        }


        private void escribirMensajeTablon_Click(object s, EventArgs e)
        {
            if (TextoNuevoMensaje.Text != "Escribe tu mensaje")
            {
                _viewModelTablonMensaje.EnviarMensajeTablon(TextoNuevoMensaje.Text);
                //Esto oculta el teclado.
                this.Focus();
                TextoNuevoMensaje.Text = "Escribe tu mensaje";
            }
            else
            {
                //TODO aqui mostrar un error o algo.
            }
                
        }

        private void recargarMensajesTablon_Click(object sender, EventArgs e)
        {
            _viewModelTablonMensaje.CargarMensajesTablon();
        }

        private void NavegaExamen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AsignaturasExamen.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/EditarPerfil.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            botonFavPulsado = true;
            botonAmor = ((Button)sender);
        }


       
    }
}