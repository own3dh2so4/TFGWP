using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.Model.Comunicacion;
using PrTab.ViewModel;
using PrTab.Model.Modelo;

namespace PrTab.View
{
    public partial class ExamenApuntesLista : PhoneApplicationPage
    {
        string idSubject;
        string idtheme;
        string type ;
        string theme;

        private ExamenApuntesViewModel _viewModel;

        public ExamenApuntesLista()
        {
            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            idSubject = NavigationContext.QueryString["idsubject"];
            idtheme = NavigationContext.QueryString["idTheme"];
            type = NavigationContext.QueryString["type"];
            theme = NavigationContext.QueryString["theme"];
            if (idtheme == "a")
            {
                idtheme = "";
                theme = "";
            }
            string name = NavigationContext.QueryString["subject"] + "  " + theme;
            if (type == "Examen")
            {
                type = "exam";
            }
            else
            {
                type = "notes";
            }
            _viewModel = new ExamenApuntesViewModel(idSubject,  idtheme,  type);
            ContentPanel.DataContext = _viewModel;
            titulo.Text = name;
            tipo.Text = NavigationContext.QueryString["type"];
        }

        private void ListaDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type == "Examen")
            {
                var doc = ((LongListSelector)sender).SelectedItem as Examen;
                //Todo cuntinuar desde aqui
            }
        }
    }
}