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
using PrTab.Model.Comunicacion;
using PrTab.Utiles;

namespace PrTab.View
{
    public partial class EditarPerfilDatosPersonales : PhoneApplicationPage
    {

        private List<Provincia> provincias = new List<Provincia>();
        private List<Universidad> universidades = new List<Universidad>();
        private List<Facultad> facultades = new List<Facultad>();

        private int id_provincia = 0;
        private int id_universidad = 0;
        private int id_facultad = 0;

        public EditarPerfilDatosPersonales()
        {
            InitializeComponent();
            this.Loaded += InicializaProvincias;
        }

        private async void InicializaProvincias(object sender, RoutedEventArgs e)
        {
            ListItemProvincias.Items.Add("");
            provincias = await Comunicacion.getProvicias();
            foreach (var provincia in provincias)
            {
                ListItemProvincias.Items.Add(provincia.nombre);
            }
        }


        private async void ListPicker_ProvicniaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemProvincias.SelectedItem != null && ListItemProvincias.SelectedItem.ToString() != "")
            {
                List<string> unis = new List<string>();
                unis.Add("");
                universidades = await Comunicacion.getUniversidadesProvincia(elementoSelecionadoProvincia(ListItemProvincias.SelectedItem.ToString()));
                foreach (var uni in universidades)
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
                foreach (var fac in facultades)
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

        private int elementoSelecionadoProvincia(String name)
        {
            int i = 0;
            foreach (var provincia in provincias)
            {
                if (provincia.nombre == name)
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AntiguaContraseña.Password != "" && AntiguaContraseña.Password != null)
            {
                
                if (id_facultad != 0)
                {
                    var respuesta = await Comunicacion_Usuario.CambiarFacultad(id_facultad + "", AntiguaContraseña.Password);
                    if (!respuesta)
                    {
                        MessageBox.Show(AplicationSettings.getErrorServer());
                        
                    }
                }
                if (NuevaContraseña.Password != "" && NuevaContraseña.Password != null)
                {
                    if (NuevaContraseña.Password.Length < 6)
                    {
                        MessageBox.Show("Las contraseñas tienen que tener 6 caracteres minimo.");
                    }
                    else
                    {
                        var respuesta = await Comunicacion_Usuario.CambiarContraseña(AntiguaContraseña.Password, NuevaContraseña.Password);
                        if (!respuesta)
                        {
                            //MessageBox.Show(AplicationSettings.getErrorServer());
                            MessageBox.Show("Contraseña incorrecta");
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Introduce tu contraseña actual.");
            }
        }
    }
}