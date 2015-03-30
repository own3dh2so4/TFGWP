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
        string subject;

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
            subject = NavigationContext.QueryString["subject"];
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
            if (theme == "")
            {
                theme = "a";
            }
            if (type == "exam")
            {
                
                var examen = ((LongListSelector)sender).SelectedItem as PrTab.Model.Modelo.Examen;
                var datos = "?theme="+theme+"&subject="+subject+"&type="+type+"&fotoUsuario="+examen.fotoUsuario+"&nombreUser="+examen.nombreUsuario+"&ano="+examen.ano+"&mes="+examen.mes+"&numPhotos="+examen.imagenes.Count;
                for(int i=0; i<examen.imagenes.Count; i++)
                {
                    datos = datos + "&foto" + i + "=" + examen.imagenes.ElementAt(i);
                }
                //Todo cuntinuar desde aqui
                NavigationService.Navigate(new Uri("/View/ExamenApuntesVista.xaml"+datos, UriKind.Relative));
            }
            else
            {
                var apuntes = ((LongListSelector)sender).SelectedItem as PrTab.Model.Modelo.Apuntes;
                var datos = "?theme=" + theme + "&subject=" + subject + "&type=" + type + "&fotoUsuario=" + apuntes.fotoUsuario + "&nombreUser=" + apuntes.nombreUsuario + "&ano=" + apuntes.ano + "&descripcion=" + apuntes.descipcion + "&numPhotos=" + apuntes.imagenes.Count;
                for (int i = 0; i < apuntes.imagenes.Count; i++)
                {
                    datos = datos + "&foto" + i + "=" + apuntes.imagenes.ElementAt(i);
                }
                //Todo cuntinuar desde aqui
                NavigationService.Navigate(new Uri("/View/ExamenApuntesVista.xaml" + datos, UriKind.Relative));
            }
        }
    }
}