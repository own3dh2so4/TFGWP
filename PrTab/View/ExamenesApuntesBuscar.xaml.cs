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
using PrTab.Utiles;
using PrTab.Model.Comunicacion;

namespace PrTab.View
{
    public partial class ExamenesApuntes : PhoneApplicationPage
    {

        private CDB_Facultad BD_Facultad = new CDB_Facultad();
        private CDB_Universidad BD_Universidad = new CDB_Universidad();
        private CDB_AsignaturaExamen BD_AsignaturaExamen = new CDB_AsignaturaExamen();
        private CDB_AsignaturaCursoAgregar BD_AsignaturaCurso = new CDB_AsignaturaCursoAgregar();
        private List<Asignatura> asignaturas = new List<Asignatura>();
        private List<Tema> temas = new List<Tema>();
        private CDB_TemaExamen BD_TemasExamen = new CDB_TemaExamen();


        public ExamenesApuntes()
        {
            InitializeComponent();
            this.Loaded += Inicilizar;
        }

        private void Inicilizar(object sender, RoutedEventArgs e)
        {

            if (ListItemCurso.Items.Count == 0)// Este if evita el problema de que salga mucash veces el 1 2 3 4 
            {
                //LLenamos la lista de que quiere buscar
                ListItemBuscar.Items.Add("");
                ListItemBuscar.Items.Add("Examen");
                ListItemBuscar.Items.Add("Apuntes");

                //Empezamos rellenado el ListItem del curso
                ListItemCurso.Items.Add("");
                ListItemCurso.Items.Add("1");
                ListItemCurso.Items.Add("2");
                ListItemCurso.Items.Add("3");
                ListItemCurso.Items.Add("4");
            }


        }

        private async void ListPicker_CursoSelecionado(object sender, SelectionChangedEventArgs e)
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

        private async void ListPicker_AsignaturaSelecionada(object sender, SelectionChangedEventArgs e)
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
                BotonAgregarAsignatura.IsEnabled = true;
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

        private void BotonAgregarAsignatura_Click(object sender, RoutedEventArgs e)
        {
            if (ListItemBuscar.SelectedItem == null || ListItemBuscar.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Selecciona que quieres buscar");
                return;
            }
            if(ListItemCurso.SelectedItem == null || ListItemCurso.SelectedItem.ToString()=="")
            {
                MessageBox.Show("Selecciona un curso");
                return;
            }
            if(ListItemAsignatura.SelectedItem == null || ListItemCurso.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Selecciona una asignatura");
                return;
            }
            string idTheme = "a";
            string theme = "a";
            if (ListItemTema.SelectedItem != null && ListItemTema.SelectedItem.ToString() != "")
            {
                idTheme = searchIdTheme() + "";
                theme = ListItemTema.SelectedItem.ToString();
            }
            NavigationService.Navigate(new Uri("/View/ExamenApuntesLista.xaml?idsubject="+ searchIdAsignatrua()+
                                                    "&subject=" + ListItemAsignatura.SelectedItem.ToString() +
                                                    "&idTheme=" + idTheme +
                                                    "&theme=" + theme +
                                                    "&type=" + ListItemBuscar.SelectedItem.ToString(), UriKind.Relative));
        }
        

       
    }
}