﻿using Gallium_v1.Data;
using Gallium_v1.Logique;
using System;
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
        // Utilisateur connecté à gallium
        private User actualUser;

        public GalliumFenetre(User u)
        {
            InitializeComponent();
            this.AccueilFrame.Focus();
            actualUser = u;
            ChargementProfil();
        }


        /// <summary>
        /// Bouton qui affiche l'accueil
        /// </summary>
        /// <Author> Damien.C </Author>
        private void AccueilFrame_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AccueilFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Accueil";
        }

        /// <summary>
        /// Bouton qui affiche la caisse
        /// </summary>
        /// <Author> Damien.C </Author>
        private void CaisseFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/CaisseFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Caisse";
        }

        /// <summary>
        /// Bouton qui affiche la caisse
        /// </summary>
        /// <Author> Damien.C </Author>
        private void StockFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/StockFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Stock";
        }

        /// <summary>
        /// Bouton qui affiche les acomptes
        /// </summary>
        /// <Author> Damien.C </Author>
        private void AcompteFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AcompteFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Acompte";
        }

        /// <summary>
        /// Bouton qui affiche les comptes utilisateurs
        /// </summary>
        /// <Author> Damien.C </Author>
        private void CompteFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/CompteFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Compte";
        }

        /// <summary>
        /// Affiche la vue d'administration 
        /// </summary>
        /// <Author> Damien.C </Author>
        private void AdministrationFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/AdministrationFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Administration";
        }

        /// <summary>
        /// Affiche la vue client
        /// </summary>
        /// <Author> Damien.C </Author>
        private void VueClientFrame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Source = new Uri("Frame/StatFrame.xaml", UriKind.Relative);
            this.NomFrame.Text = "Statistiques";
        }

        /// <summary>
        /// Bouton qui permet la déconnexion
        /// </summary>
        /// <Author> Damien.C </Author>
        private void DéconnexionFrame_Click(object sender, RoutedEventArgs e)
        {
            ConnexionFenetre connexion = new ConnexionFenetre();
            dbsDAO.Instance.CloseDatabase();
            this.Close();
            connexion.Show();
        }

        /// <summary>
        /// Charge le profil
        /// </summary>
        /// <Author> Damien.C </Author>
        private void ChargementProfil()
        {
            Random random = new Random();
            NomUtilisateur.Content = actualUser.NomComplet;
            RoleUtilisateur.Content = actualUser.RangUser; 

            // Recupère toutes les photos du dossier PhotoProfil
            string[] files = Directory.GetFiles("./Vue/Assets/PhotoProfil", "*.png");
            int msc = random.Next(files.Count());

            // Photo de profile tiré aléatoirement
            string photoProfile = files[msc].Split('.')[1] + ".png";

            // Change source de l'image par la nouvelle photo
            Uri urlImage = new Uri(photoProfile, UriKind.Relative);
            BitmapImage sourceImage = new BitmapImage(urlImage);
            PhotoDeProfil.Source = sourceImage;
        }

        private void TestPage(object sender, RoutedEventArgs e)
        {
            Test test = new Test();
            test.Show();
        }
       

    }
}
