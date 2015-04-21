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
using System.ComponentModel;
using System.Windows.Shapes;
using PrTab.ComunicacionChat;
using PrTab.Model.ComunicacionChat;

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

        private HiloChat hilo = HiloChat.getInstance();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += InicializarViewModel;
            

            InicializaChats();
        }

        private void InicializaChats()
        {
            var rooms = hilo.getRooms();
            foreach (var r in rooms)
            {
                
                    anadirSalas(r);
            }
        }

        private void anadirSalas(string text)
        {
            Grid fondo = new Grid();
            fondo.Height = 100;

            SolidColorBrush yellowBrush = new SolidColorBrush();
            yellowBrush.Color = Colors.Yellow;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            fondo.Background = yellowBrush;

            Button boton = new Button();
            boton.Content = text;

            boton.Foreground = blackBrush;

            fondo.Children.Add(boton);
            Chats.Children.Add(fondo);

            boton.Click += (s, a) =>
            {
                NavigationService.Navigate(new Uri("/View/ChatRoom.xaml?room=" + text, UriKind.Relative));
            };
        }

        

        private void InicializarViewModel(object sender, RoutedEventArgs e)
        {
            _viewModelTablonMensaje = new MensajesTablonViewModel();
            GridMensajes.DataContext = _viewModelTablonMensaje;
            _viewModelTablonMensaje.CargarMensajesTablon();
        }

       
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
            NavigationService.Navigate(new Uri("/View/UsuarioVista.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            botonFavPulsado = true;
            botonAmor = ((Button)sender);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GridRoom.Visibility = System.Windows.Visibility.Visible;
        }

        private void BotonCancelar_Click(object sender, RoutedEventArgs e)
        {
            GridRoom.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BotonNuevaSala_Click(object sender, RoutedEventArgs e)
        {
            GridRoom.Visibility = System.Windows.Visibility.Collapsed;
            hilo.createRoom(nombreNuevaSala.Text);
            anadirSalas(nombreNuevaSala.Text);
        }

        private void SubirExamen_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SubirExamen.xaml", UriKind.Relative));
        }

        private void SubirApuntes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SubirApuntes.xaml", UriKind.Relative));
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ExamenesApuntesBuscar.xaml", UriKind.Relative));
        }




       
    }
}