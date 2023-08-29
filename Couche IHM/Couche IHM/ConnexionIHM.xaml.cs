﻿
using Couche_IHM.VueModeles;
using Couche_Métier.Manager;
using MaterialDesignThemes.Wpf;
using Modeles;
using System;
using System.Windows;



namespace Couche_IHM
{
    /// <summary>
    /// Logique d'interaction pour ConnexionIHM.xaml
    /// </summary>
    public partial class ConnexionIHM : Window
    {
        #region attributes
        private UserManager userManager;
        private LogManager logManager;
        private string identifiant = "";
        private string password = "";
        #endregion

        public ConnexionIHM()
        {
            InitializeComponent();
            DataContext = this;
            this.messageQueue = new SnackbarMessageQueue(new TimeSpan(0, 0, 2));
        }

        public string Identifiant { get => identifiant; set => identifiant = value; }
        public string Password { get => password; set => password = value; }
        public SnackbarMessageQueue MessageQueue { get => messageQueue; set => messageQueue = value; }

        private SnackbarMessageQueue messageQueue;

        /// <summary>
        /// Permet de se connecter à son compte et de créer la mainWindows
        /// </summary>
        private void ConnectToAccount(object sender, RoutedEventArgs e)
        {      
            if (password == "" || identifiant == "")
            {
                messageQueue.Enqueue("Vous devez remplir les deux champs !");
            }
            else
            {
                try
                {
                    userManager = MainWindowViewModel.Instance.UserManager;
                    logManager = MainWindowViewModel.Instance.LogManager;
                    User? user = this.userManager.ConnectCompte(identifiant, password);
                    if (user != null)
                    {
                        Log log = new Log(DateTime.Now, 1, $"Connexion de {user.Prenom} {user.Nom}", $"{user.Prenom} {user.Nom}");
                        logManager.CreateLog(log);
                        MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
                        MainWindow mainWindow = new MainWindow(user, logManager, userManager);
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        messageQueue.Enqueue("Mauvais mot de passe");
                    }
                }
                catch(Exception ex)
                {
                    messageQueue?.Enqueue("Vous n'êtes pas connecté à Internet");
                }

                
            }
          

            
        }
    }
}
