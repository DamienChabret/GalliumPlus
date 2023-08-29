﻿using Couche_Data;
using Couche_IHM.Frames;
using Couche_IHM.VueModeles;
using Couche_Métier;
using Couche_Métier.Manager;
using Modeles;
using System;
using System.Windows;

namespace Couche_IHM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Constructeur de la mainwindow
        /// </summary>
        public MainWindow(User user,LogManager logManager,UserManager userManager)
        {
            InitializeComponent();
            MainWindowViewModel.Instance.CompteConnected = new UserViewModel(user,userManager);
            DataContext = MainWindowViewModel.Instance;
        }

        /// <summary>
        /// Permet de se déconnecter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnect(object sender, RoutedEventArgs e)
        {
            ConnexionIHM connexionIHM = new ConnexionIHM();
            connexionIHM.Show();
            this.Close();
        }
    }
}
