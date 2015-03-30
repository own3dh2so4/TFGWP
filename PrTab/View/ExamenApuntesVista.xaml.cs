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
using System.Windows.Input;

namespace PrTab.View
{
    public partial class ExamenVista : PhoneApplicationPage
    {
        string theme;
        string subject;
        string type;
        string fotoUsuario;
        string nombreUsuario;
        string ano;
        string mes="";
        string descripcion = "";
        int numImages;
        List<string> imagenes = new List<string>();
        int count;



        public ExamenVista()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            theme = NavigationContext.QueryString["theme"];
            subject = NavigationContext.QueryString["subject"];
            type = NavigationContext.QueryString["type"];
            fotoUsuario = NavigationContext.QueryString["fotoUsuario"];
            nombreUsuario = NavigationContext.QueryString["nombreUser"];
            ano = NavigationContext.QueryString["ano"];
            if (type == "exam")
                mes = NavigationContext.QueryString["mes"];
            else
                descripcion = NavigationContext.QueryString["descripcion"]; 
            numImages = Convert.ToInt32(NavigationContext.QueryString["numPhotos"]);
            for(int i = 0; i<numImages; i++)
            {
                imagenes.Add(NavigationContext.QueryString["foto"+i]);
            }
            count = 0;
            inicializa();
        }

        private void inicializa()
        {
            titulo.Text = subject;
            if (theme != "a")
                subtitulo.Text = theme;
            BitmapImage bi = new BitmapImage(new Uri(fotoUsuario));
            imagenUsuario.Source = bi;
            textBlocknombreUsuario.Text = nombreUsuario;
            BitmapImage bi2 = new BitmapImage(new Uri(Model.Comunicacion.Comunicacion.baseURL + imagenes.ElementAt(0)));
            visorDocumentos.Source = bi2;
            Fecha.Text = numeroMesToNombreMes(ano) + " " + mes;
            description.Text = descripcion;
            if (imagenes.Count == 1)
                botonDrc.Visibility = System.Windows.Visibility.Collapsed;
            FotoCount.Text = (count + 1)+"";
            FotoTotal.Text = imagenes.Count + "";

        }

        private void botonDrc_Click(object sender, RoutedEventArgs e)
        {
            if(count<imagenes.Count)
            {
                count++;
                FotoCount.Text = (count + 1) + "";
                BitmapImage bi2 = new BitmapImage(new Uri(Model.Comunicacion.Comunicacion.baseURL + imagenes.ElementAt(count)));
                visorDocumentos.Source = bi2;
                if (count == 1)
                    botonIzq.Visibility = System.Windows.Visibility.Visible;
                if (count + 1 == imagenes.Count)
                    botonDrc.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void botonIzq_Click(object sender, RoutedEventArgs e)
        {
            if(count>0)
            {
                count--;
                FotoCount.Text = (count + 1) + "";
                BitmapImage bi2 = new BitmapImage(new Uri(Model.Comunicacion.Comunicacion.baseURL + imagenes.ElementAt(count)));
                visorDocumentos.Source = bi2;
                if (count + 1 < imagenes.Count)
                    botonDrc.Visibility = System.Windows.Visibility.Visible;
                if (count == 0)
                    botonIzq.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

       

        private void visorDocumentos_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/VisorImagen.xaml?imagen="+Model.Comunicacion.Comunicacion.baseURL + imagenes.ElementAt(count) , UriKind.Relative));              

        }

        private string numeroMesToNombreMes(string numeroMes)
        {
            string result = "";
            switch (numeroMes)
            {
                case "1": result = "Enero";
                    break;
                case "2": result = "Febrero";
                    break;
                case "3": result = "Marzo";
                    break;
                case "4": result = "Abril";
                    break;
                case "5": result = "Mayo";
                    break;
                case "6": result = "Junio";
                    break;
                case "7": result = "Julio";
                    break;
                case "8": result = "Agosto";
                    break;
                case "9": result = "Septiembre";
                    break;
                case "10": result = "Octubre";
                    break;
                case "11": result = "Noviembre";
                    break;
                case "12": result = "Diciembre";
                    break;
            }
            return result;
        }
        
    }
}