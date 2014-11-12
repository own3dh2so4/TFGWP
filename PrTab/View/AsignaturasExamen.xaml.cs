using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PrTab.ViewModel;

namespace PrTab.View
{
    public partial class AsignaturasExamen : PhoneApplicationPage
    {
        private AsignaturasExamenViewModel _viewModel;
        public AsignaturasExamen()
        {
            InitializeComponent();
            //Le decimos que no nos guarde en cache esta vista
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
            this.Loaded += InicializarViewModel;
        }

        private void InicializarViewModel(object sender, RoutedEventArgs e)
        {
            _viewModel = new AsignaturasExamenViewModel();
            ContentPanel.DataContext = _viewModel;
        }

        private void AddAplicationBar_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AgregarAsignaturaExamen.xaml", UriKind.Relative));
        }
    }
}