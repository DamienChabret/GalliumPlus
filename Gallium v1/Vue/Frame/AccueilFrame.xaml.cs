﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gallium_v1.Vue.Frame
{
    /// <summary>
    /// Logique d'interaction pour AccueilFrame.xaml
    /// </summary>
    public partial class AccueilFrame : Page
    {
        public AccueilFrame()
        {
            InitializeComponent();
        }

        private void IsClicked(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }
    }
}
