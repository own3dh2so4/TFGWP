﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PrTab.View
{
    public partial class Examen : PhoneApplicationPage
    {
        public Examen()
        {
            InitializeComponent();
            //Le decimos que no nos guarde en cache esta vista
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
        }
    }
}