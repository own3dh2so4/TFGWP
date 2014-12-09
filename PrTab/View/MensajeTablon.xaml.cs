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
using PrTab.Utiles;

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
            fecha.Text = calculoFecha();

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

            if(mensaje.identificadorUsuario+"" == AplicationSettings.getIdUsuario())
            {
                BotonBorrar.Visibility = System.Windows.Visibility.Visible;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CDB_MensajeTablon BDMensajes = new CDB_MensajeTablon();
            mensaje = BDMensajes.getForId(NavigationContext.QueryString["idMessage"]);
            if (mensaje==null)
            {
                MessageBox.Show("El mensaje que estas intentando acceder ha sido borrado");
            }
        }


        private string calculoFecha()
        {
            string ret = "";
            int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int messageTime = mensaje.fecha;
            //int.TryParse(value.ToString(),out messageTime);
            int valor = unixTimestamp - messageTime;
            valor = valor / 60;
            if (valor < 60)
                ret = "Enviado hace " + valor + " min";
            else
            {
                valor = valor / 60;
                if (valor < 24)
                    if (valor == 1)
                        ret = "Enviado hace " + valor + " hora";
                    else
                        ret = "Enviado hace " + valor + " horas";
                else
                {
                    valor = valor / 24;
                    if (valor < 31)
                        if (valor == 1)
                            ret = "Enviado hace " + valor + " dia";
                        else
                            ret = "Enviado hace " + valor + " dias";
                    else
                    {
                        valor = valor / 30;
                        if (valor < 12)
                            if (valor == 1)
                                ret = "Enviado hace " + valor + "mes";
                            else
                                ret = "Enviado hace " + valor + "meses";
                        else
                        {
                            valor = valor / 12;
                            ret = "Enviado hace " + valor + " años";
                        }
                    }
                }
            }

            return ret;
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

        private async void BotonBorrar_Click(object sender, RoutedEventArgs e)
        {
            Comunicacion_MensajesTablon cmt = new Comunicacion_MensajesTablon();
            var borrado = await cmt.borrarMensaje(mensaje.identificador + "");
            if(borrado)
            {
                TextoMensajeBorrado.Visibility = System.Windows.Visibility.Visible;
            }
        }
        



    }
}