﻿using System;
using System.Collections.Generic;
using System.IO;
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
            this.AccueilFrame.Focus();
            ChargementPhotoDeProfil();
        }


        /// <summary>
        /// Bouton qui affiche l'accueil
        /// </summary>
        private void AccueilFrame_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AccueilFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Accueil";
        }

        /// <summary>
        /// Bouton qui affiche la caisse
        /// </summary>
        private void CaisseFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/CaisseFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Caisse";
        }

        /// <summary>
        /// Bouton qui affiche la caisse
        /// </summary>
        private void StockFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/StockFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Stock";
        }

        /// <summary>
        /// Bouton qui affiche les acomptes
        /// </summary>
        private void AcompteFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AcompteFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Acompte";
        }

        /// <summary>
        /// Bouton qui affiche les comptes utilisateurs
        /// </summary>
        private void CompteFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/CompteFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Compte";
        }

        /// <summary>
        /// Affiche la vue d'administration 
        /// </summary>
        private void AdministrationFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AdministrationFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Administration";
        }

        /// <summary>
        /// Affiche la vue client
        /// </summary>
        private void VueClientFrame_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=LYG7sMtb6u8t");
        }

        /// <summary>
        /// Bouton qui permet la déconnexion
        /// </summary>
        private void DéconnexionFrame_Click(object sender, RoutedEventArgs e)
        {
            ConnexionFenetre connexion = new ConnexionFenetre();
            this.Close();
            connexion.Show();
        }

        private void ChargementPhotoDeProfil()
        {
            Random random = new Random();

            // Enregistre dans une liste tous les éléments présent dans le dossier PhotoProfil
            string[] files = Directory.GetFiles(@".\Vue\Assets\PhotoProfil", "*.png");
            int msc = random.Next(files.Count());

            // Sélectionne une photo de profil aléatoirement selon la liste
            Uri urlImage = new Uri(files[msc], UriKind.Relative);
            BitmapImage sourceImage = new BitmapImage(urlImage);
            PhotoDeProfil.Source = sourceImage;
            


        }


    }
}
