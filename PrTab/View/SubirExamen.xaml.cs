using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.Model.Modelo;
using PrTab.Model.Base_de_Datos;
using PrTab.Model.Comunicacion;
using PrTab.Utiles;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace PrTab.View
{
    public partial class SubirExamen : PhoneApplicationPage
    {
        private CDB_AsignaturaCursoAgregar BD_AsignaturaCurso = new CDB_AsignaturaCursoAgregar();
        private List<Asignatura> asignaturas = new List<Asignatura>();
        private CDB_TemaExamen BD_TemasExamen = new CDB_TemaExamen();
        private List<Tema> temas = new List<Tema>();

        //Seleccion de fotos
        PhotoChooserTask photoChooserTask;
        int botonPulsado = 0;
     

        byte[] foto1;
        byte[] foto2;
        byte[] foto3;
        byte[] foto4;
        byte[] foto5;

        public SubirExamen()
        {
            InitializeComponent(); 
            this.Loaded += InicializaLsitPicker;
            this.photoChooserTask = new PhotoChooserTask();
            this.photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed); 
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.ChosenPhoto == null)
            {
                if (botonPulsado == 1)
                    botonPulsado = 0;
            }
            else
            {
                BitmapImage a = new BitmapImage();
                switch (botonPulsado)
                {
                    case 1:
                        foto1 = ReadFully(e.ChosenPhoto);                        
                        a.SetSource(e.ChosenPhoto);
                        Image1.Source = a;
                        Boton1.Visibility = System.Windows.Visibility.Collapsed;
                        Boton2.Visibility = System.Windows.Visibility.Visible;
                        Image2.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 2:
                        foto2 = ReadFully(e.ChosenPhoto);
                        a.SetSource(e.ChosenPhoto);
                        Image2.Source = a;
                        Boton2.Visibility = System.Windows.Visibility.Collapsed;
                        Boton3.Visibility = System.Windows.Visibility.Visible;
                        Image3.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 3:
                        foto3 = ReadFully(e.ChosenPhoto);
                        a.SetSource(e.ChosenPhoto);
                        Image3.Source = a;
                        Boton3.Visibility = System.Windows.Visibility.Collapsed;
                        Boton4.Visibility = System.Windows.Visibility.Visible;
                        Image4.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 4: 
                        foto4 = ReadFully(e.ChosenPhoto);
                        a.SetSource(e.ChosenPhoto);
                        Image4.Source = a;
                        Boton4.Visibility = System.Windows.Visibility.Collapsed;
                        Boton5.Visibility = System.Windows.Visibility.Visible;
                        Image5.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 5: 
                        foto5 = ReadFully(e.ChosenPhoto);
                        a.SetSource(e.ChosenPhoto);
                        Image5.Source = a;
                        Boton5.Visibility = System.Windows.Visibility.Collapsed;
                        break;

                }
            }
            

            
        } 

        private void InicializaLsitPicker(object sender, RoutedEventArgs e)
        {
            if(ListItemAsignatura.Items.Count==0)
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

                ListItemMes.Items.Add("");
                ListItemMes.Items.Add("Enero");
                ListItemMes.Items.Add("Febrero");
                ListItemMes.Items.Add("Marzo");
                ListItemMes.Items.Add("Abril");
                ListItemMes.Items.Add("Mayo");
                ListItemMes.Items.Add("Junio");
                ListItemMes.Items.Add("Julio");
                ListItemMes.Items.Add("Agosto");
                ListItemMes.Items.Add("Septiembre");
                ListItemMes.Items.Add("Octubre");
                ListItemMes.Items.Add("Noviembre");
                ListItemMes.Items.Add("Diciembre");
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
                temas = BD_TemasExamen.getTemasDeAsignaturas(i+"");
                if(temas.Count == 0)
                {
                    Comunicacion_Examen ca = new Comunicacion_Examen();
                    if(await ca.getThemeFromServer(i+""))
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

        



        private void ListPicker_AñoSeleccioando(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListPicker_MesSeleccioando(object sender, SelectionChangedEventArgs e)
        {

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
                return ;
            }
            if (ListItemAsignatura.SelectedItem == null || ListItemAsignatura.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Selecciona una asignatura");
                return;
            }
            if (ListItemMes.SelectedItem == null || ListItemMes.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Selecciona un mes");
                return;
            }
            if (botonPulsado==0)
            {
                MessageBox.Show("Selecciona almenos una imagen (Pulsa el boton + )");
                return;
            }
            //MessageBox.Show("Mandado.");
            GridCargando.Visibility = System.Windows.Visibility.Visible;
            string eltema = null;
            if (ListItemTema.SelectedItem != null && ListItemTema.SelectedItem.ToString() != "")
                eltema = searchIdTheme() + "";
           
           string token = await Comunicacion.createExam(AplicationSettings.getToken(),
                        searchIdAsignatrua() + "",
                        eltema,
                        ListItemAño.SelectedItem.ToString(),
                        monthToNumber() + "",
                        "False",
                        foto1);
          switch(botonPulsado)
          {
                        case 2:
                            await Comunicacion.updateExam(token, "2", "True", foto2);
                            break;
                        case 3:
                            await Comunicacion.updateExam(token, "2", "False", foto2);
                            await Comunicacion.updateExam(token, "3", "True", foto3);
                            break;
                        case 4:
                            await Comunicacion.updateExam(token, "2", "False", foto2);
                            await Comunicacion.updateExam(token, "3", "False", foto3);
                            await Comunicacion.updateExam(token, "4", "True", foto4);
                            break;
                        case 5:
                            await Comunicacion.updateExam(token, "2", "False", foto2);
                            await Comunicacion.updateExam(token, "3", "False", foto3);
                            await Comunicacion.updateExam(token, "4", "False", foto4);
                            await Comunicacion.updateExam(token, "5", "True", foto5);
                            break;
                    }
             
            GridCargando.Visibility = System.Windows.Visibility.Collapsed;
            MessageBox.Show("Mensaje subido, gracias por tu colaboracion");

        }

        public int monthToNumber()
        {
            int ret = 0;
            if(ListItemMes.SelectedItem !=null && ListItemMes.SelectedItem.ToString()!= "")
                switch(ListItemMes.SelectedItem.ToString())
                {
                    case "Enero": ret = 1; break;
                    case "Febrero": ret = 2; break ;
                    case "Marzo": ret = 3; break ;
                    case "Abril": ret = 4; break ;
                    case "Mayo": ret = 5; break ;
                    case "Junio": ret = 6; break ;
                    case "Julio": ret = 7; break ;
                    case "Agosto": ret = 8; break;
                    case "Septiembre": ret = 9; break;
                    case "Octubre": ret = 10; break;
                    case "Noviembre": ret = 11; break;
                    case "Diciembre": ret = 12; break;
                }
            return ret;
        }

        public static byte[] ConvertToBytes( BitmapImage bitmapImage)
        {  
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap
                    (bitmapImage.PixelWidth, bitmapImage.PixelHeight);

                // write an image into the stream
                Extensions.SaveJpeg(btmMap, ms,
                    bitmapImage.PixelWidth, bitmapImage.PixelHeight, 0, 100);

                return ms.ToArray();
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



    }
}