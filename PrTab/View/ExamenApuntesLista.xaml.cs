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

namespace PrTab.View
{
    public partial class ExamenApuntesLista : PhoneApplicationPage
    {
        public ExamenApuntesLista()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string idSubject = NavigationContext.QueryString["idsubject"];
            string type = NavigationContext.QueryString["idTheme"];
            string theme = NavigationContext.QueryString["type"];
            if (theme == "a")
                theme = "";
            //TODO
        }
    }
}