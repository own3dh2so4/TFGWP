using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using PrTab.Model.Charts;
using System.Windows.Media.Imaging;
using PrTab.Utiles;
using PrTab.Model.Modelo;
using PrTab.Model.Comunicacion;

namespace PrTab.View
{
    public partial class UsuarioVista : PhoneApplicationPage
    {

        private ResumenEstadisitcas estadisticas;
        public UsuarioVista()
        {
            InitializeComponent();
            BitmapImage bi2 = new BitmapImage(new Uri(Model.Comunicacion.Comunicacion.baseURL + "media/users/pic_image_"+AplicationSettings.getIdUsuario()+".jpg"));
            fotoUsuario.Source = bi2;
        }



        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            ////Pie Chart Data Source
            //ObservableCollection<PieData> PieDataCollection = new ObservableCollection<PieData>()
            //{
            //    new PieData() { Title = "Correctas", Value = 60 },
            //    new PieData() { Title = "Falladas", Value = 25 },
            //    new PieData() { Title = "Sin Contestar", Value = 5 }
            //};
            //PieChart.DataSource = PieDataCollection;

            ////Line Chart Data Source
            //ObservableCollection<LineData> LineDataCollection = new ObservableCollection<LineData>()
            //{
            //    new LineData { Category = "E1", Line1 = 80, Line2 = 40, Line3 = 50 },
            //    new LineData { Category = "E2", Line1 = 50, Line2 = 70, Line3 = 40 },
            //    new LineData { Category = "E3", Line1 = 60, Line2 = 50, Line3 = 20 },
            //    new LineData { Category = "E4", Line1 = 10, Line2 = 30, Line3 = 50 },
            //    new LineData { Category = "E5", Line1 = 40, Line2 = 10, Line3 = 70 }
            //};
            //LineChart.DataSource = LineDataCollection;
            estadisticas = await Comunicacion.getStatic(AplicationSettings.getToken());
            if (estadisticas.test!=null)
            {
                ObservableCollection<PieData> PieDataCollection = new ObservableCollection<PieData>();
                PieDataCollection.Add(new PieData("Correctas", estadisticas.per_correcta));
                PieDataCollection.Add(new PieData("Incorrectas", estadisticas.per_incorrecta));
                PieDataCollection.Add(new PieData("No Respondidas", estadisticas.per_noRespondida));

                PieChart.DataSource = PieDataCollection;

                ObservableCollection<LineData> LineDataCollection = new ObservableCollection<LineData>();
                int i = 1;
                foreach (var a in estadisticas.test)
                {
                    var q = a.correctasSobreCien();
                    var t = a.falladasSobreCien();
                    var y = a.sinResponderSobreCien();
                    LineDataCollection.Add(new LineData { Category = "Test" + i, Line1 = a.correctasSobreCien(), Line2 = a.falladasSobreCien(), Line3 = a.sinResponderSobreCien() });
                    i++;
                }
                LineChart.DataSource = LineDataCollection;

                NotaMedia.Text = estadisticas.avg_nota+"";
                TiempoMedio.Text = estadisticas.avg_tiempo / 1000 + " s";
            }
            
        }

        private void CambiarDatos_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/EditarPerfil.xaml", UriKind.Relative));
        }
    }
}