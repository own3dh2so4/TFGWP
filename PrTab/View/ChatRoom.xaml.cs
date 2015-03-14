using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.Model.Base_de_Datos;
using PrTab.Model.Modelo.ChatServer;
using System.Windows.Media;
using System.Windows.Shapes;
using PrTab.Utiles;
using PrTab.Model.ComunicacionChat;
using System.ComponentModel;
using System.Threading;

namespace PrTab.View
{
    public partial class ChatRoom : PhoneApplicationPage
    {
        private string sala;
        private HiloChat hilo = HiloChat.getInstance();
        private BackgroundWorker bw = new BackgroundWorker();
        CDB_MensajesServerMessage BDMensajes = new CDB_MensajesServerMessage();
        List<MensajeServerMensaje> mensajesHilo = new List<MensajeServerMensaje>();

        public ChatRoom()
        {
            InitializeComponent();
            //inicializaRecepcion();
            
        }

        private void inicializaRecepcion()
        {
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            bool finish = false;
            while (worker.CancellationPending == false && !finish)
            {
                if (sala!=null)
                    mensajesHilo = BDMensajes.getMessagesFromRoom(sala);
                if (mensajesHilo.Count > 0)
                    finish = true;
                else
                    Thread.Sleep(2000);
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var m in mensajesHilo)
                ponerMensaje(m, m.userID == Convert.ToInt32(AplicationSettings.getIdUsuario()));
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }


        private void cargarMensajes()
        {
            
            List<MensajeServerMensaje> mensajes = BDMensajes.getMessagesFromRoom(sala);
            if (mensajes == null)
            {
                //MessageBox.Show("El mensaje que estas intentando acceder ha sido borrado");
            }
            else
            {
                foreach (var m in mensajes)
                    ponerMensaje(m, m.userID == Convert.ToInt32(AplicationSettings.getIdUsuario()));
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            nombreSala.Text = "Sala: " + NavigationContext.QueryString["room"];
            sala = NavigationContext.QueryString["room"];
            cargarMensajes();
            inicializaRecepcion();
            
        }

        private void ponerMensaje(MensajeServerMensaje mensaje, bool me)
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

        }

        private void Mandar_Click(object sender, RoutedEventArgs e)
        {
            if (Mensaje.Text != "")
            {
                //bw.CancelAsync();
                MensajeServerMensaje mandar = new MensajeServerMensaje(Mensaje.Text, sala, AplicationSettings.getUsuario(), Convert.ToInt32(AplicationSettings.getIdUsuario()));
                hilo.sendMessage(mandar);
                //ponerMensaje("<Tú>" + Mensaje.Text, true);
                cargarMensajes();
                ponerMensaje(mandar, true);
                Mensaje.Text = "";
                //if (!bw.IsBusy)
                //bw.RunWorkerAsync();
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            bw.CancelAsync();
            NavigationService.GoBack();
        }

        

    }
}