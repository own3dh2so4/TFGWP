using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using PrTab.Model.Base_de_Datos;
using PrTab.Model.Comunicacion;

namespace PrTab.View
{
    public partial class MensajeTablon : PhoneApplicationPage
    {
        //private string nombre;
        //private string foto;
        //private string comentario;

        PrTab.Model.Modelo.MensajeTablon mensaje;

        public MensajeTablon()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
        }

        private void Inicializa(object sender, RoutedEventArgs e)
        {
            //Inicializacion de la imagen
            //BitmapImage bi3 = new BitmapImage();
            //bi3.UriSource = new Uri(mensaje.foto, UriKind.Absolute);
            //FotoUsuario.Stretch = Stretch.Fill;
            //FotoUsuario.Source = bi3;
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(mensaje.foto));
            ElipseUsuario.Fill = myBrush;

            NombreUsuario.Text = mensaje.nombre;
            MensajeUsuario.Text = mensaje.mensaje;
            numFav.Text = mensaje.numFav + "";

            if (mensaje.userFav)
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.UriSource = new Uri("icons/heart.red.png", UriKind.Relative);
                imagenFav.Stretch = Stretch.Fill;
                imagenFav.Source = bi3;
            }
            else
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.UriSource = new Uri("icons/heart.white.png", UriKind.Relative);
                imagenFav.Stretch = Stretch.Fill;
                imagenFav.Source = bi3;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CDB_MensajeTablon BDMensajes = new CDB_MensajeTablon();
            mensaje = BDMensajes.getForId(NavigationContext.QueryString["idMessage"]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mensaje.userFav = !mensaje.userFav;
            Comunicacion_MensajesTablon servicioMensajes = new Comunicacion_MensajesTablon();
            //servicioMensajes.favMesajeTablon(mensaje);

            if (mensaje.userFav)
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.UriSource = new Uri("icons/heart.red.png", UriKind.Relative);
                imagenFav.Stretch = Stretch.Fill;
                imagenFav.Source = bi3;
                mensaje.numFav++;
                numFav.Text = mensaje.numFav+"";
            }
            else
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.UriSource = new Uri("icons/heart.white.png", UriKind.Relative);
                imagenFav.Stretch = Stretch.Fill;
                imagenFav.Source = bi3;
                mensaje.numFav--;
                numFav.Text = mensaje.numFav + "";
            }

           servicioMensajes.favMesajeTablon(mensaje);
        }
    }
}