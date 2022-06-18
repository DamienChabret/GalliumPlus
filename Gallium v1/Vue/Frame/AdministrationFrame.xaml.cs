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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gallium_v1.Vue.Frame
{
    /// <summary>
    /// Logique d'interaction pour AdministrationFrame.xaml
    /// </summary>
    public partial class AdministrationFrame : Page
    {
        public AdministrationFrame()
        {
            InitializeComponent();
            userList.ItemsSource = ListUser.UsersList;
            //this.userList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Role", System.ComponentModel.ListSortDirection.Descending));
        }

        /// <summary>
        /// Permet de rechercher un utilisateur
        /// </summary>
        private void Search(object sender, TextChangedEventArgs e)
        {
            if (this.rechercheUser.Text != "")
            {
                InfoUser.Visibility = Visibility.Visible;
                AfficheUser(this.rechercheUser.Text);
            }
            else
            {
                InfoUser.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Permet d'afficher les informations d'un user
        /// </summary>
        /// <param name="nomUser"> Nom de l'utilisateur </param>
        private void AfficheUser(string nomUser)
        {
            User user = ListUser.findUser(nomUser);
            if (user != null)
            {
                this.nomUser.Text = user.NomUser;
                this.prénomUser.Text = user.PrenomUser;
                this.identifiantUser.Text = user.IdentifiantUser;
                this.roleUser.Text = user.RangUser.ToString();

            }
            else
            {
                InfoUser.Visibility = Visibility.Hidden;
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            // Initialise le produit
            User u = this.userList.SelectedItem as User;

            if (u == null)
            {
                u = ListUser.findUser(this.rechercheUser.Text);
            }

            // Message demandant si vous voulez vraiment supprimer le produit
            MessageBoxResult result = MessageBox.Show("Êtes-vous sur de vouloir supprimer ce produit ?", $"Supression de {u.PrenomUser}", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ListUser.RemoveUser(u);
                this.userList.UnselectAll();
                this.UpdateListProduits();
                InfoUser.Visibility = Visibility.Hidden;
            }

        }

        private void ModifUser(object sender, RoutedEventArgs e)
        {
            // Initialise le produit
            User u = this.userList.SelectedItem as User;

            if (u == null)
            {
                u = ListUser.findUser(this.rechercheUser.Text);
            }

            // Fenetre de modification en mode modale
            ModificationUser modificationUser = new ModificationUser(u);
            modificationUser.ShowDialog();

            // Modification de l'utilisateur
            
            this.userList.SelectedItem = modificationUser.User;
            this.UpdateListProduits();
            
        }


        /// <summary>
        /// Met à jour la liste des stocks
        /// </summary>
        private void UpdateListProduits()
        {
            this.userList.ItemsSource = null;
            this.userList.ItemsSource = Stock.StockProduits;
        }
    }
}
