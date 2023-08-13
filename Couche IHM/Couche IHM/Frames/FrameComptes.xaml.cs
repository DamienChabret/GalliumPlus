﻿using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Log;
using Modeles;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Couche_IHM.Frames
{
    /// <summary>
    /// Logique d'interaction pour FrameComptes.xaml
    /// </summary>
    public partial class FrameComptes : Page
    {
        private UserManager userManager = new UserManager();
        private bool createCompte;
        private ILog log;

        // Attributs qui gèrent si la liste est triée
        private int isSortingMail = 0;
        private int isSortingRole = 0;
        private int isSortingIdentite = 0;
        public FrameComptes()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.Instance;

            // Met à jour l'affichage
            UpdateView();

            this.log = new LogToTxt();

 
        }

        /// <summary>
        /// Créer un utilisateur
        /// </summary>
        /// <param name="u"></param>
        private void CreateAnUser(User u)
        {
            this.userManager.CreateCompte(u);
            //log.registerLog(CategorieLog.CREATE, $"Création de l'utilisateur {u.NomComplet}", MainWindow.CompteConnected);
        }

        /// <summary>
        /// Met à jour un utilisateur
        /// </summary>
        /// <param name="baseUser"> base utilisateur </param>
        /// <param name="u"> utilisateur update </param>
        private void UpdateAnUser(User baseUser, User u)
        {
            this.userManager.UpdateCompte(u);

            // LOG
            //log.registerLog(CategorieLog.UPDATE, u, MainWindow.CompteConnected);
        }

        /// <summary>
        /// Permet de mettre à jour la liste des utilisateurs
        /// </summary>
        private void UpdateView()
        {
            listUser.ItemsSource = null;
            listUser.ItemsSource = userManager.GetComptes();
        }


        #region events

      



        #endregion

        #region tri
        /// <summary>
        /// Permet de trier les utilisateurs selon leur identité
        /// </summary>
        private void SortIdentite(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("identiteTri", this.listUser) as Image;
            switch (isSortingIdentite)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("NomComplet", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("NomComplet", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("NomComplet", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("NomComplet", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingIdentite = (isSortingIdentite + 1) % 3;
        }

        /// <summary>
        /// Permet de trier les adhérents selon leur role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortRole(object sender, RoutedEventArgs e)
        {

            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("roleTri", this.listUser) as Image;

            switch (isSortingRole)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Role", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Role", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Role", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Role", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingRole = (isSortingRole + 1) % 3;

        }

        /// <summary>
        /// Permet de trier les adhérents selon leur mail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortMail(object sender, RoutedEventArgs e)
        {
            ControlTemplate template = this.listUser.Template;
            Image myImage = template.FindName("mailTri", this.listUser) as Image;
            switch (isSortingMail)
            {
                case 0:
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Mail", ListSortDirection.Ascending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triAsc.png", UriKind.Relative));
                    break;
                case 1:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Mail", ListSortDirection.Ascending));
                    this.listUser.Items.SortDescriptions.Add(new SortDescription("Mail", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Visible;
                    myImage.Source = new BitmapImage(new Uri("/Images/triDesc.png", UriKind.Relative));
                    break;
                case 2:
                    this.listUser.Items.SortDescriptions.Remove(new SortDescription("Mail", ListSortDirection.Descending));
                    myImage.Visibility = Visibility.Hidden;
                    break;
            }
            isSortingMail = (isSortingMail + 1) % 3;
        }

        #endregion

        private void PermButton(object sender, MouseEventArgs e)
        {
            //if (MainWindow.CompteConnected.Role != RolePerm.PRESIDENT)
            //{
            //    (sender as Button).IsEnabled = false;
            //}
            
        }

        /// <summary>
        /// Ajoute un nouvelle utilisateur
        /// </summary>
        private void AddAnUser(object sender, RoutedEventArgs e)
        {
            User newUser = new User();
            FenetreAddUser fa = new FenetreAddUser(newUser);

            // Si l'utilisateur valide la création
            if (fa.ShowDialog().Value)
            {
                this.userManager.CreateCompte(newUser);
                UpdateView();
            };
        }

        /// <summary>
        /// Met à jour un utilisateur
        /// </summary>
        private void UpdateAnUser(object sender, RoutedEventArgs e)
        {
            // Récupère l'utilisateur selectionné
            ListViewItem item = FindAncestor<ListViewItem>((sender as Button));
            User userSelected = (User)item.DataContext;

            User newUser = new User((User)userSelected);
            FenetreAddUser fa = new FenetreAddUser(newUser);

            // Si l'utilisateur valide la création
            if (fa.ShowDialog().Value)
            {
                this.userManager.UpdateCompte(newUser);
                UpdateView();
            };
        }

        /// <summary>
        /// Trouve l'élément d'un parent spécifique
        /// </summary>
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            T res = null;
            do
            {
                if (current is T ancestor) 
                    res = ancestor;
                current = VisualTreeHelper.GetParent(current);
            } while (current != null);
            return res;
        }
    }
}