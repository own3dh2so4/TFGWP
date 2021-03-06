﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.Model.Base_de_Datos;
using PrTab.Utiles;
using PrTab.Model.Modelo;
using PrTab.Model.Comunicacion;

namespace PrTab.View
{
    public partial class AgregarAsignaturaExamen : PhoneApplicationPage
    {
        private CDB_Facultad BD_Facultad = new CDB_Facultad();
        private CDB_Universidad BD_Universidad = new CDB_Universidad();
        private CDB_AsignaturaExamen BD_AsignaturaExamen = new CDB_AsignaturaExamen();
        private CDB_AsignaturaCursoAgregar BD_AsignaturaCurso = new CDB_AsignaturaCursoAgregar();
        private List<Asignatura> asignaturas = new List<Asignatura>();

        public AgregarAsignaturaExamen()
        {
            InitializeComponent();
            
            this.Loaded += Inicilizar;
        }

        private void Inicilizar(object sender, RoutedEventArgs e)
        {
            
            if(ListItemCurso.Items.Count==0)// Este if evita el problema de que salga mucash veces el 1 2 3 4 
            {
                //Empezamos rellenado el ListItem del curso
                ListItemCurso.Items.Add("");
                ListItemCurso.Items.Add("1");
                ListItemCurso.Items.Add("2");
                ListItemCurso.Items.Add("3");
                ListItemCurso.Items.Add("4");
                //Asignamos El nombre de la facultad
                nombreFacultad.Text = BD_Facultad.selectById(Convert.ToInt32(AplicationSettings.getIdFacultadUsuario())).nombre;
                //Asignamos el nombre de la universidad
                nombreUniversidad.Text = BD_Universidad.selectById(Convert.ToInt32(AplicationSettings.getIdUniversidadUsuario())).nombre;
            }
            
        }

        private async void ListPicker_CursoSelecionado(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemCurso.SelectedItem != null && ListItemCurso.SelectedItem.ToString() != "")
            {
                List<string> asig = new List<string>();
                asig.Add("");
                asignaturas = BD_AsignaturaCurso.getAsignaturasDelCurso(ListItemCurso.SelectedItem.ToString());
                if (asignaturas.Count==0)
                    asignaturas = await Comunicacion_Asignatura.getAsignaturas(AplicationSettings.getToken(), ListItemCurso.SelectedItem.ToString(), AplicationSettings.getIdFacultadUsuario());
                foreach (var a in asignaturas)
                {
                    asig.Add(a.abreviatura);
                }
                ListItemAsignatura.ItemsSource = asig;
                ListItemAsignatura.IsEnabled = true;

            }
        }

        private void ListPicker_AsignaturaSelecionada(object sender, SelectionChangedEventArgs e)
        {
            if (ListItemAsignatura.SelectedItem != null && ListItemAsignatura.SelectedItem.ToString() != "")
            {
                BotonAgregarAsignatura.IsEnabled = true;
            }
        }


        //private int elementoSelecionadoCurso()
        //{
        //    int i = 0;
        //    switch (ListItemCurso.SelectedItem.ToString())
        //    {
        //        case "1": i = 1; break;
        //        case "2": i = 2; break;
        //        case "3": i = 3; break;
        //        case "4": i = 4; break;
        //    }
        //    return i;
        //}

        private Asignatura elementoSelecionadoAsignatura()
        {
            Asignatura i = new Asignatura();
            foreach (var a in asignaturas)
            {
                if (a.abreviatura == ListItemAsignatura.SelectedItem.ToString())
                {
                    return a;
                }
            }
            return i;
        }

        private void BotonAgregarAsignatura_Click(object sender, RoutedEventArgs e)
        {
            //BD_AsignaturaExamen.insert(elementoSelecionadoAsignatura());
            Comunicacion_Asignatura.addAsingaturaFavorito(AplicationSettings.getToken(), elementoSelecionadoAsignatura());
            BotonAgregarAsignatura.IsEnabled = false;
            Notificacion.Text = elementoSelecionadoAsignatura().nombre + " se añadio";
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/View/AsignaturasExamen.xaml", UriKind.Relative));
        }
    }
}