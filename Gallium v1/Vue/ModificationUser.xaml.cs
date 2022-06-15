﻿using Gallium_v1.Logique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gallium_v1.Vue
{
    /// <summary>
    /// Logique d'interaction pour ModificationUser.xaml
    /// </summary>
    public partial class ModificationUser : Window
    {
        User user;

        public ModificationUser(User u)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
        }

        private void ValiderModif(object sender, RoutedEventArgs e)
        {

        }

        private void AnnulerModif(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
