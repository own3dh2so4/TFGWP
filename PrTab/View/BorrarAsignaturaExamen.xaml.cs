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
using PrTab.Model.Comunicacion;
using PrTab.Utiles;

namespace PrTab.View
{
    public partial class BorrarAsignaturaExamen : PhoneApplicationPage
    {
        private CDB_AsignaturaExamen BD_Asignaturas = new CDB_AsignaturaExamen();
        private List<Asignatura> asignaturas;


        public BorrarAsignaturaExamen()
        {
            InitializeComponent();
            this.Loaded += Inicilizar;
        }

        private void Inicilizar(object sender, RoutedEventArgs e)
        {
            
            asignaturas = BD_Asignaturas.getAsignaturasExamen();
            rellenarList();
            
        }

        private void rellenarList()
        {

            List<string> asig = new List<string>();
            asig.Add("");
            foreach (var a in asignaturas)
                asig.Add(a.nombre);
            ListItemAsignatura.ItemsSource = asig;
        }

        private void ListPicker_AsignaturaSelecionada(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemAsignatura.SelectedItem != null && ListItemAsignatura.SelectedItem.ToString() != "")
            {
                BotonBorrarAsignatura.IsEnabled = true;
            }
        }



        private Asignatura elementoSelecionadoAsignatura()
        {
            Asignatura i = new Asignatura();
            foreach (var a in asignaturas)
            {
                if (a.nombre == ListItemAsignatura.SelectedItem.ToString())
                {
                    return a;
                }
            }
            return i;
        }

        private void BotonBorrarAsignatura_Click(object sender, RoutedEventArgs e)
        {
            Comunicacion_Asignatura.deleteAsingaturaFavorito(AplicationSettings.getToken(), elementoSelecionadoAsignatura());
            var asignatura =elementoSelecionadoAsignatura().nombre;
            Notificacion.Text = asignatura+ " ha sido borrada";
            asignaturas.Remove(elementoSelecionadoAsignatura());
            rellenarList();
            BotonBorrarAsignatura.IsEnabled = false;
            Notificacion.Text = "";
        }
    }
}