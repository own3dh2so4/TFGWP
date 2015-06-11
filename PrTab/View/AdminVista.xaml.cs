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
    public partial class AdminVista : PhoneApplicationPage
    {
        public AdminVista()
        {
            InitializeComponent();
            webview.Navigate(new Uri(Comunicacion.baseURL + "admin/"));
        }
    }
}