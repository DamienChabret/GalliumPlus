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
using System.Windows.Shapes;

namespace Gallium_v1.Vue
{
    /// <summary>
    /// Logique d'interaction pour Test.xaml
    /// </summary>
    public partial class GalliumFenetre : Window
    {
        public GalliumFenetre()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Bouton qui affiche l'accueil
        /// </summary>
        private void AccueilFrame_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AccueilFrame.xaml", UriKind.Relative);
           
            
            
        }

        /// <summary>
        /// Bouton qui affiche la caisse
        /// </summary>
        private void CaisseFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/CaisseFrame.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Bouton qui affiche les acomptes
        /// </summary>
        private void AcompteFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AcompteFrame.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Bouton qui affiche les comptes utilisateurs
        /// </summary>
        private void CompteFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/CompteFrame.xaml", UriKind.Relative);
        }

        private void AdministrationFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AdministrationFrame.xaml", UriKind.Relative);
        }

        private void VueClientFrame_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Bouton qui permet la déconnexion
        /// </summary>
        private void DéconnexionFrame_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
