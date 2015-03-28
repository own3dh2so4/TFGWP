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
using PrTab.Model.Modelo;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using PrTab.Model.Comunicacion;
using PrTab.Utiles;

namespace PrTab.View
{
    public partial class SubirApuntes : PhoneApplicationPage
    {
        private CDB_AsignaturaCursoAgregar BD_AsignaturaCurso = new CDB_AsignaturaCursoAgregar();
        private List<Asignatura> asignaturas = new List<Asignatura>();
        private CDB_TemaExamen BD_TemasExamen = new CDB_TemaExamen();
        private List<Tema> temas = new List<Tema>();

        //Seleccion de fotos
        PhotoChooserTask photoChooserTask;
        int botonPulsado = 0;

        //fotos
        private List<byte[]> fotos = new List<byte[]>();

        public SubirApuntes()
        {
            InitializeComponent();
            this.Loaded += InicializaLsitPicker;
            this.photoChooserTask = new PhotoChooserTask();
            this.photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed); 
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.ChosenPhoto != null)
            {
                BitmapImage a = new BitmapImage();
                fotos.Add(ReadFully(e.ChosenPhoto));
                a.SetSource(e.ChosenPhoto);
                switch(botonPulsado)
                { 
                    case 1:
                        Image1.Source = a;
                        Boton1.Visibility = System.Windows.Visibility.Collapsed;
                        Boton2.Visibility = System.Windows.Visibility.Visible;
                        Image2.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 2:
                        Image2.Source = a;
                        Boton2.Visibility = System.Windows.Visibility.Collapsed;
                        Boton3.Visibility = System.Windows.Visibility.Visible;
                        Image3.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 3:
                        Image3.Source = a;
                        Boton3.Visibility = System.Windows.Visibility.Collapsed;
                        Boton4.Visibility = System.Windows.Visibility.Visible;
                        Image4.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 4:
                        Image4.Source = a;
                        Boton4.Visibility = System.Windows.Visibility.Collapsed;
                        Boton5.Visibility = System.Windows.Visibility.Visible;
                        Image5.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 5:
                        Image5.Source = a;
                        Boton5.Visibility = System.Windows.Visibility.Collapsed;
                        Boton6.Visibility = System.Windows.Visibility.Visible;
                        Image6.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 6:
                        Image6.Source = a;
                        Boton6.Visibility = System.Windows.Visibility.Collapsed;
                        Boton7.Visibility = System.Windows.Visibility.Visible;
                        Image7.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 7:
                        Image7.Source = a;
                        Boton7.Visibility = System.Windows.Visibility.Collapsed;
                        Boton8.Visibility = System.Windows.Visibility.Visible;
                        Image8.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 8:
                        Image8.Source = a;
                        Boton8.Visibility = System.Windows.Visibility.Collapsed;
                        break;

                }
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void InicializaLsitPicker(object sender, RoutedEventArgs e)
        {
            if (ListItemAsignatura.Items.Count == 0)
            {

                ListItemCurso.Items.Add("");
                ListItemCurso.Items.Add("1");
                ListItemCurso.Items.Add("2");
                ListItemCurso.Items.Add("3");
                ListItemCurso.Items.Add("4");

                ListItemAño.Items.Add("");
                ListItemAño.Items.Add("2000");
                ListItemAño.Items.Add("2001");
                ListItemAño.Items.Add("2002");
                ListItemAño.Items.Add("2003");
                ListItemAño.Items.Add("2004");
                ListItemAño.Items.Add("2005");
                ListItemAño.Items.Add("2006");
                ListItemAño.Items.Add("2007");
                ListItemAño.Items.Add("2008");
                ListItemAño.Items.Add("2009");
                ListItemAño.Items.Add("2010");
                ListItemAño.Items.Add("2011");
                ListItemAño.Items.Add("2012");
                ListItemAño.Items.Add("2013");
                ListItemAño.Items.Add("2014");
                ListItemAño.Items.Add("2015");

                
            }
        }

        private async void ListPicker_CursoSeleccioando(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemCurso.SelectedItem != null && ListItemCurso.SelectedItem.ToString() != "")
            {
                List<string> asig = new List<string>();
                asig.Add("");
                asignaturas = BD_AsignaturaCurso.getAsignaturasDelCurso(ListItemCurso.SelectedItem.ToString());
                if (asignaturas.Count == 0)
                    asignaturas = await Comunicacion_Asignatura.getAsignaturas(AplicationSettings.getToken(), ListItemCurso.SelectedItem.ToString(), AplicationSettings.getIdFacultadUsuario());
                foreach (var a in asignaturas)
                {
                    asig.Add(a.abreviatura);
                }
                ListItemAsignatura.ItemsSource = asig;
                ListItemAsignatura.IsEnabled = true;
            }
        }

        private async void ListPicker_AsignaturaSeleccioando(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemAsignatura.SelectedItem != null && ListItemAsignatura.SelectedItem.ToString() != "")
            {
                List<string> t = new List<string>();
                t.Add("");
                var i = searchIdAsignatrua();
                temas = BD_TemasExamen.getTemasDeAsignaturas(i + "");
                if (temas.Count == 0)
                {
                    Comunicacion_Examen ca = new Comunicacion_Examen();
                    if (await ca.getThemeFromServer(i + ""))
                    {
                        temas = BD_TemasExamen.getTemasDeAsignaturas(i + "");
                    }

                }
                foreach (var te in temas)
                {
                    t.Add(te.nombre);
                }
                ListItemTema.ItemsSource = t;
                ListItemTema.IsEnabled = true;
            }
        }
        private int searchIdAsignatrua()
        {
            foreach (var a in asignaturas)
                if (a.abreviatura == ListItemAsignatura.SelectedItem.ToString())
                    return a.identificador;
            return -1;
        }

        private int searchIdTheme()
        {
            foreach (var a in temas)
                if (a.nombre == ListItemTema.SelectedItem.ToString())
                    return a.identificador;
            return -1;
        }

        private void Boton1_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 1;
            photoChooserTask.Show();
        }

        private void Boton2_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 2;
            photoChooserTask.Show();
        }

        private void Boton3_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 3;
            photoChooserTask.Show();
        }

        private void Boton4_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 4;
            photoChooserTask.Show();
        }

        private void Boton5_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 5;
            photoChooserTask.Show();
        }

        private void Boton6_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 6;
            photoChooserTask.Show();
        }

        private void Boton7_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 7;
            photoChooserTask.Show();
        }

        private void Boton8_Click(object sender, RoutedEventArgs e)
        {
            botonPulsado = 8;
            photoChooserTask.Show();
        }

        private async void Enviar_Click(object sender, RoutedEventArgs e)
        {
            if (ListItemCurso.SelectedItem == null || ListItemCurso.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Selecciona un curso");
                return;
            }
            if (ListItemAño.SelectedItem == null || ListItemAño.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Selecciona un año");
                return;
            }
            if (ListItemAsignatura.SelectedItem == null || ListItemAsignatura.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Selecciona una asignatura");
                return;
            }
            if (botonPulsado == 0)
            {
                MessageBox.Show("Selecciona almenos una imagen (Pulsa el boton + )");
                return;
            }
            GridCargando.Visibility = System.Windows.Visibility.Visible;
            string eltema = null;
            if (ListItemTema.SelectedItem != null && ListItemTema.SelectedItem.ToString() != "")
                eltema = searchIdTheme() + "";
            string masDeUnaImagen = "True";
            if (botonPulsado > 1)
                masDeUnaImagen = "False";
            string token = await Comunicacion.createDocument(AplicationSettings.getToken(),
                        searchIdAsignatrua() + "",
                        eltema,
                        ListItemAño.SelectedItem.ToString(),
                        "",
                        "notes",
                        masDeUnaImagen,
                        TextBoxDescription.Text,
                        fotos.ElementAt(0));
            for(int i=1; i<fotos.Count; i++)
            {
                if (i+1==fotos.Count)
                    await Comunicacion.updateDocument(token, "notes", "True",(i+1)+"", fotos.ElementAt(i));
                else
                    await Comunicacion.updateDocument(token, "notes", "False",(i+1)+"", fotos.ElementAt(i));
            }
            GridCargando.Visibility = System.Windows.Visibility.Collapsed;
            MessageBox.Show("Mensaje subido, gracias por tu colaboracion");

        }

        


    }
}