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

        /**
            Conexion al chat
        **/
        //const string dir = "192.168.0.2";
        //const string dir = "178.62.194.33";
        //const int port = 8000;
        //SocketClient client = new SocketClient();
        //ChatMensaje newMsg = new ChatMensaje();
        
        //private BackgroundWorker bw = new BackgroundWorker();

        MensajesTablonViewModel _viewModelTablonMensaje;

        private Button botonAmor;

        private bool botonFavPulsado = false;

        private HiloChat hilo = HiloChat.getInstance();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //_viewModelTablonMensaje = (MensajesTablonViewModel)Resources["ViewModelTablon"];
            this.Loaded += InicializarViewModel;
            

            // Código de ejemplo para traducir ApplicationBar
            //BuildLocalizedApplicationBar();
            //inicializaElHilo();
            //conectWithServer();
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

        /*public void conectWithServer()
        {
            string result = client.Connect(dir, port);
            if (result == "Success")
            {

                ponerMensaje(new ChatMensaje("El servidor responde","lalala", "System", 0), false);
                string reg = client.Register();
                if(reg == "Success")
                {
                    ponerMensaje(new ChatMensaje("Te has conectado al chat!", "lalala", "System", 0), false);
                    bw.RunWorkerAsync();
                }
                
            }
            else
                ponerMensaje(new ChatMensaje("Hay algun problema", "lalala", "System", 0), false);
        }*/

        /*public void inicializaElHilo()
        {
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

        }*/

        /*private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            bool getMsg = false;
            while (worker.CancellationPending == false && !getMsg)
            {
                /*string msg = client.Receive();
                ponerMensaje(msg); AQUI COMENTAR
                newMsg = client.Receive();
                //if (newMsg != "Operation Timeout")
                getMsg = true;
            }
        }*/

        /*private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ponerMensaje(newMsg, false);
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }*/

        /*public void ponerMensaje(ChatMensaje mensaje, bool me)
        {
            Grid panelTexto = new Grid();
            //panelTexto.Orientation = System.Windows.Controls.Orientation.Horizontal;
            Thickness marginpanel = panelTexto.Margin;
            marginpanel.Bottom = 10;
            panelTexto.Margin = marginpanel;

            //Creo los colores
            SolidColorBrush yellowBrush = new SolidColorBrush();
            yellowBrush.Color = Colors.Yellow;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            //Creo el triangulo
            Polygon yellowTriangle = new Polygon();
            yellowTriangle.Fill = yellowBrush;
            //Creo sus puntos
            System.Windows.Point Point1 = new System.Windows.Point(0, 0);
            System.Windows.Point Point2 = new System.Windows.Point(10, 0);
            System.Windows.Point Point3;
            if (!me)
                Point3 = new System.Windows.Point(10, 10);
            else
                Point3 = new System.Windows.Point(0, 10);
            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(Point1);
            polygonPoints.Add(Point2);
            polygonPoints.Add(Point3);

            //Añado los puntos al triangulo
            yellowTriangle.Points = polygonPoints;


            //Creo el mensaje con el texto
            Grid gridParaTexto = new Grid();// En WP los textBlock notienecolor de fondo, usamos un gridpara ponerle ese color de fondo
            gridParaTexto.Background = yellowBrush;
            

            //Mejoramos la interfaz del mensaje
            gridParaTexto.RowDefinitions.Add(new RowDefinition()
            {
                Height = GridLength.Auto,
            });
            gridParaTexto.RowDefinitions.Add(new RowDefinition()
            {
                Height = GridLength.Auto,
            });
            TextBlock name = new TextBlock();
            name.Text = mensaje.name + ":";
            name.Foreground = blackBrush;
            gridParaTexto.Children.Add(name);

            TextBlock texto = new TextBlock();
            texto.TextWrapping = TextWrapping.Wrap;
            texto.Text = mensaje.message;
            texto.Foreground = blackBrush;
            gridParaTexto.Children.Add(texto);

            Grid.SetRow(name, 0);
            Grid.SetRow(texto, 1);

            panelTexto.Children.Add(yellowTriangle);
            panelTexto.Children.Add(gridParaTexto);

            //Añado el texto segun quien pusiera el mensaje
            if (!me)
            {

                //panelTexto.Children.Add(yellowTriangle);
                //panelTexto.Children.Add(gridParaTexto);
                panelTexto.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = GridLength.Auto,
                });
                panelTexto.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star),
                });
                Grid.SetColumn(gridParaTexto, 1);
                Grid.SetColumn(yellowTriangle, 0);
                chat.Children.Add(panelTexto);
            }
            else
            {
                //panelTexto.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                //panelTexto.Children.Add(gridParaTexto);
                //panelTexto.Children.Add(yellowTriangle);
                panelTexto.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star),
                });
                panelTexto.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = GridLength.Auto,
                });

                gridParaTexto.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                Grid.SetColumn(gridParaTexto, 0);
                Grid.SetColumn(yellowTriangle, 1);
                chat.Children.Add(panelTexto);

            }



            //Estas 2 lineas de abajo me bajan el ScrollView cuando esta lleno de mensajes.
            ScrollViewer.UpdateLayout();
            ScrollViewer.ScrollToVerticalOffset(double.MaxValue);

        }*/

        private void InicializarViewModel(object sender, RoutedEventArgs e)
        {
            _viewModelTablonMensaje = new MensajesTablonViewModel();
            GridMensajes.DataContext = _viewModelTablonMensaje;
            _viewModelTablonMensaje.CargarMensajesTablon();
        }

        /*private void Mandar_Click(object sender, RoutedEventArgs e)
        {
            if (Mensaje.Text != "")
            {
                //bw.CancelAsync();
                ChatMensaje mandar = new ChatMensaje(Mensaje.Text, AplicationSettings.getIdUniversidadUsuario(), "Tu", Convert.ToInt32(AplicationSettings.getIdUsuario()));
                client.SendMessage(mandar);
                //ponerMensaje("<Tú>" + Mensaje.Text, true);
                ponerMensaje(mandar, true);
                Mensaje.Text = "";
                //if (!bw.IsBusy)
                //bw.RunWorkerAsync();
            }
        }*/

        

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