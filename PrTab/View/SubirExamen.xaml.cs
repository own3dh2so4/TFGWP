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

namespace PrTab.View
{
    public partial class SubirExamen : PhoneApplicationPage
    {
        private CDB_AsignaturaCursoAgregar BD_AsignaturaCurso = new CDB_AsignaturaCursoAgregar();
        private List<Asignatura> asignaturas = new List<Asignatura>();

        //Seleccion de fotos
        PhotoChooserTask photoChooserTask;
        int botonPulsado = 0;
        BitmapImage imagen1 = new BitmapImage();
        BitmapImage imagen2 = new BitmapImage();
        BitmapImage imagen3 = new BitmapImage();
        BitmapImage imagen4 = new BitmapImage();
        BitmapImage imagen5 = new BitmapImage();

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
                switch (botonPulsado)
                {
                    case 1: imagen1.SetSource(e.ChosenPhoto);
                        Image1.Source = imagen1;
                        Boton1.Visibility = System.Windows.Visibility.Collapsed;
                        Boton2.Visibility = System.Windows.Visibility.Visible;
                        Image2.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 2: imagen2.SetSource(e.ChosenPhoto);
                        Image2.Source = imagen2;
                        Boton2.Visibility = System.Windows.Visibility.Collapsed;
                        Boton3.Visibility = System.Windows.Visibility.Visible;
                        Image3.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 3: imagen3.SetSource(e.ChosenPhoto);
                        Image3.Source = imagen3;
                        Boton3.Visibility = System.Windows.Visibility.Collapsed;
                        Boton4.Visibility = System.Windows.Visibility.Visible;
                        Image4.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 4: imagen4.SetSource(e.ChosenPhoto);
                        Image4.Source = imagen4;
                        Boton4.Visibility = System.Windows.Visibility.Collapsed;
                        Boton5.Visibility = System.Windows.Visibility.Visible;
                        Image5.Visibility = System.Windows.Visibility.Visible;
                        break;
                    case 5: imagen5.SetSource(e.ChosenPhoto);
                        Image5.Source = imagen5;
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

        private void ListPicker_AsignaturaSeleccioando(object sender, SelectionChangedEventArgs e)
        {

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

        private void Enviar_Click(object sender, RoutedEventArgs e)
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
            MessageBox.Show("Mandado.");

            

        }



    }
}