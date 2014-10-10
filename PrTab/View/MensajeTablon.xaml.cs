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

namespace PrTab.View
{
    public partial class MensajeTablon : PhoneApplicationPage
    {
        private string nombre;
        private string foto;
        private string comentario;
        public MensajeTablon()
        {
            InitializeComponent();
            this.Loaded += Inicializa;
        }

        private void Inicializa(object sender, RoutedEventArgs e)
        {
            //Inicializacion de la imagen
            BitmapImage bi3 = new BitmapImage();
            bi3.UriSource = new Uri(foto, UriKind.Relative);
            FotoUsuario.Stretch = Stretch.Fill;
            FotoUsuario.Source = bi3;

            NombreUsuario.Text = nombre;

            MensajeUsuario.Text = comentario;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            nombre = NavigationContext.QueryString["name"];
            comentario = NavigationContext.QueryString["message"];
            foto = NavigationContext.QueryString["photo"];
        }
    }
}