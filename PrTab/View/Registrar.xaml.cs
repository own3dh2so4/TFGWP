using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.Model;
using PrTab.ViewModel;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace PrTab.View
{
    public partial class Registrar : PhoneApplicationPage
    {
        private List<Provincia> provincias = new List<Provincia>();
        private List<Universidad> universidades = new List<Universidad>();
        private List<Facultad> facultades = new List<Facultad>();

        private int id_provincia = 0;
        private int id_universidad=0;
        private int id_facultad = 0;

        public Registrar()
        {
            InitializeComponent();
            this.Loaded += InicializaProvincias;
        }

        private async void InicializaProvincias(object sender, RoutedEventArgs e)
        {
            ListItemProvincias.Items.Add("");
            provincias = await Comunicacion.getProvicias();
            foreach(var provincia in provincias)
            {
                ListItemProvincias.Items.Add(provincia.nombre);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void ListPicker_ProvicniaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemProvincias.SelectedItem != null && ListItemProvincias.SelectedItem.ToString() != "")
            {
                List<string> unis = new List<string>();
                unis.Add("");                
                universidades = await Comunicacion.getUniversidadesProvincia(elementoSelecionadoProvincia(ListItemProvincias.SelectedItem.ToString()));
                foreach(var uni in universidades)
                {
                    unis.Add(uni.nombre);
                }
                ListItemUniversidad.ItemsSource = unis;
                ListItemUniversidad.IsEnabled = true;
            }
                
        }

        private async void ListPicker_UniversidadSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemUniversidad.SelectedItem != null && ListItemUniversidad.SelectedItem.ToString() != "")
            {
                List<string> faculs = new List<string>();
                faculs.Add("");
                facultades = await Comunicacion.getFacultadesUniversidad(elementoSelecionadoUniversidad(ListItemUniversidad.SelectedItem.ToString()));
                foreach(var fac in facultades)
                {
                    faculs.Add(fac.nombre);
                }
                ListItemFacultades.ItemsSource = faculs;
                ListItemFacultades.IsEnabled = true;
            }
        }

        private void ListPicker_FacultadSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemFacultades.SelectedItem != null && ListItemFacultades.SelectedItem.ToString() != "")
            {
                elementoSelecionadoFacultad(ListItemFacultades.SelectedItem.ToString());
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (NombreUsuario.Text.Length < 3)
            {
                NombreUsuario.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Nombre de usuario demasiado corto");
            }
            else if (PasswordUsuario.Password.Length < 6)
            {
                PasswordUsuario.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Contraseña demasiado corto");
            }
            else if (!email_bien_escrito())
            {
                EmailUsuario.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Email no valido");
            }
            else if (id_provincia==0)
            {
                ListItemProvincias.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Seleciona una provincia");
            }
            else if (id_universidad==0)
            {
                ListItemUniversidad.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Seleciona una universidad");
            }
            else if (id_facultad==0)
            {
                ListItemFacultades.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Seleciona una facultad");
            }
            else if (await Comunicacion_Usuario.RegistrarUsuario(NombreUsuario.Text, PasswordUsuario.Password, EmailUsuario.Text, id_universidad + "", id_facultad+""))
            {
                AplicationSettings.RegistrarUsuario(NombreUsuario.Text, PasswordUsuario.Password);
                NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show(AplicationSettings.getErrorServer());
                AplicationSettings.setErrorServer("");
            }
        }

        private int elementoSelecionadoProvincia(String name)
        {
            int i = 0;
            foreach (var provincia in provincias)
            {
                if(provincia.nombre==name)
                {
                    id_provincia = provincia.identificador;
                    return provincia.identificador;
                }
                i++;
            }
            return -1;
        }

        private int elementoSelecionadoUniversidad(String name)
        {
            int i = 0;
            foreach (var uni in universidades)
            {
                if (uni.nombre == name)
                {
                    id_universidad = uni.identificador;
                    return uni.identificador;
                }
                i++;
            }
            return -1;
        }

        private int elementoSelecionadoFacultad(String name)
        {
            int i = 0;
            foreach (var fac in facultades)
            {
                if (fac.nombre == name)
                {
                    id_facultad = fac.identificador;
                    return fac.identificador;
                }
                i++;
            }
            return -1;
        }

        private Boolean email_bien_escrito()
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(EmailUsuario.Text, expresion))
            {
                if (Regex.Replace(EmailUsuario.Text, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void EmailUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!email_bien_escrito())
            {
                EmailUsuario.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                EmailUsuario.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
        }

       
        

        
    }
}