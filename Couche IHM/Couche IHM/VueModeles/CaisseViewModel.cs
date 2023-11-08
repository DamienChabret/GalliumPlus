﻿
using Couche_Métier.Manager;
using Couche_Métier.Utilitaire;
using MaterialDesignThemes.Wpf;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Couche_IHM.VueModeles
{
    public class CaisseViewModel : INotifyPropertyChanged
    {
        #region attributes
        private ObservableDictionary<ProductViewModel, int> productOrder = new ObservableDictionary<ProductViewModel, int>();
        private string currentPaiement;
        private bool showPayAcompte = false;
        private bool showPayPaypal = false;
        private bool showPayLiquide = false;
        private bool showPayBanque = false;
        private bool isAdherent = true;
        private string prixIhm;
        private AcompteViewModel adherentPayer = null;
        private StatProduitManager statProduitManager;
        private StatAcompteManager statAcompteManager;
        private OrderManager orderManager;
        private bool retourAcompteArgent = false;
        private string textArgentAdherentRetour;
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur de caisse vue modele
        /// </summary>
        public CaisseViewModel(StatAcompteManager statAcompte, StatProduitManager statProduit, OrderManager orderManager)
        {
            // Initialisation objets métier
            this.statProduitManager = statProduit;
            this.statAcompteManager = statAcompte;
            this.orderManager = orderManager;

            // Initialisation events
            this.AddProd = new RelayCommand(prodIHM => AddProduct(prodIHM));
            this.RemoveProd = new RelayCommand(prodIHM => RemoveProduct(prodIHM));
            this.ShowPay = new RelayCommand(x => PreviewPayArticles());
            this.CancelPay = new RelayCommand(x =>
            {
                this.ShowPayPaypal = false;
                this.ShowPayBanque = false;
                this.ShowPayAcompte = false;
                this.ShowPayLiquide = false;
            }
            );
            this.Pay = new RelayCommand(tuple => PayArticles(tuple));
            this.ClearProd = new RelayCommand(x =>
            {
                this.ProductOrder.Clear();
                this.NotifyPropertyChanged(nameof(this.PriceAdherIHM));

                this.NotifyPropertyChanged(nameof(this.PriceNonAdherIHM));
            });
            this.CurrentPaiement = Paiements[0];
        }
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region events

        public RelayCommand ClearProd { get; set; }
        public RelayCommand AddProd { get; set; }
        public RelayCommand RemoveProd { get; set; }
        public RelayCommand CancelPay { get; set; }
        public RelayCommand Pay { get; set; }
        public RelayCommand ShowPay { get; set; }
        #endregion

        #region properties
        /// <summary>
        /// Permet d'afficher une popup avec l'argent qu'il reste à l'adhérent
        /// </summary>
        public bool RetourAcompteArgent
        {
            get
            {
                return this.retourAcompteArgent;
            }
            set
            {
                this.retourAcompteArgent = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Liste des moyens de paiements
        /// </summary>
        public List<string> Paiements
        {
            get
            {
                return new List<string>()
                { "Acompte", "Paypal", "Carte", "Liquide" };
            }
        }

        /// <summary>
        /// Renvoie le prix total du panier pour les adhérents
        /// </summary>
        public float PriceAdher
        {
            get
            {
                float prixTotal = 0.00f;
                foreach (ProductViewModel product in productOrder.Keys)
                {
                    prixTotal += (float)ConverterFormatArgent.ConvertToDouble(product.PrixAdherentIHM) * productOrder[product];
                }
                return prixTotal;
            }
        }
        /// <summary>
        /// Prix adhérent formatté
        /// </summary>
        public string PriceAdherIHM
        {
            get
            {
                return $"{ConverterFormatArgent.ConvertToString(PriceAdher)}";
            }
        }
        /// <summary>
        /// Prix non adhérent formatté
        /// </summary>
        public string PriceNonAdherIHM
        {
            get
            {
                return $"({ConverterFormatArgent.ConvertToString(PriceNanAdher)})";
            }
        }
        /// <summary>
        /// Renvoie le prix total du panier pour les non adhérents
        /// </summary>
        public float PriceNanAdher
        {
            get
            {
                float prixTotal = 0.00f;
                foreach (ProductViewModel product in productOrder.Keys)
                {
                    prixTotal += (float)ConverterFormatArgent.ConvertToDouble(product.PrixNonAdherentIHM) * productOrder[product];
                }
                return prixTotal;
            }

        }

        /// <summary>
        /// Représente les produits du panier
        /// </summary>
        public ObservableDictionary<ProductViewModel, int> ProductOrder
        {
            get => productOrder;
            set => productOrder = value;
        }

        /// <summary>
        /// Représente le moyen de paiement sélectionné
        /// </summary>
        public string CurrentPaiement
        {
            get => currentPaiement;
            set
            {
                currentPaiement = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Représente l'adhérent qui va payer
        /// </summary>
        public AcompteViewModel AdherentPayer
        {
            get => adherentPayer;
            set
            {
                adherentPayer = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Permet d'afficher la sélection d'acompte
        /// </summary>
        public bool ShowPayAcompte
        {
            get => showPayAcompte;
            set
            {
                showPayAcompte = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Est ce que l'acheteur est adhérent
        /// </summary>
        public bool IsAdherent
        {
            get => isAdherent;
            set
            {
                isAdherent = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowPayPaypal
        {
            get => showPayPaypal;
            set
            {
                showPayPaypal = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowPayBanque
        {
            get => showPayBanque;
            set
            {
                showPayBanque = value;
                NotifyPropertyChanged();
            }
        }

        public string PrixIHM
        {
            get { return prixIhm; }
            set
            {
                prixIhm = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowPayLiquide
        {
            get => showPayLiquide;
            set
            {
                showPayLiquide = value;
                NotifyPropertyChanged();
            }
        }

        public string TextArgentAdherentRetour 
        { 
            get => textArgentAdherentRetour;
            set
            {
                textArgentAdherentRetour = value;
                NotifyPropertyChanged();
            } 
        }


        #endregion

        #region methods

        private record class InfosPaiementAcompte(AcompteViewModel acompte, float argent, float prix);

        private static string FindPaymentCodeFor(string paymentName)
        {
            return paymentName switch
            {
                "Acompte" => "Deposit",
                "Paypal" => "Paypal",
                "Carte" => "CreditCard",
                "Liquide" => "Cash",
                _ => throw new ArgumentException($"Méthode de paiement inconnue : « {paymentName} »")
            };
        }

        /// <summary>
        /// Permet de payer les articles
        /// </summary>
        private async void PayArticles(object tuple = null)
        {
            string messageLog = $"Achat par {currentPaiement} ";

            try
            {
                InfosPaiementAcompte? paiementAcompte = null;
                
                // Paiement par acompte
                if (tuple is (AcompteViewModel acompte, bool isAdherentCheckboxChecked))
                {
                    float prix;
                    if (isAdherentCheckboxChecked)
                    {
                        prix = this.PriceAdher;
                    }
                    else
                    {
                        prix = this.PriceNanAdher;
                    }
                    paiementAcompte = new InfosPaiementAcompte(
                        acompte,
                        argent: ConverterFormatArgent.ConvertToDouble(acompte.ArgentIHM),
                        prix
                    );

                    if (paiementAcompte.argent - prix < 0)
                    {
                        throw new Exception("Pas assez d'argent sur l'acompte");
                    }

                    orderManager.NewOrder("Deposit", acompte.IdentifiantIHM);
                }
                else
                {
                    if (isAdherent)
                    {
                        orderManager.NewOrder(
                            FindPaymentCodeFor(currentPaiement),
                            Order.ANONYMOUS_MEMBER // le client est un adhérent, juste on sait pas qui
                        );
                        messageLog += $"({this.PriceAdherIHM}) : ";
                    }
                    else
                    {
                        orderManager.NewOrder(FindPaymentCodeFor(currentPaiement));
                        messageLog += $"{this.PriceNonAdherIHM} : ";
                    }
                }

                foreach (var kvp in productOrder)
                {
                    orderManager.CurrentOrder.AddProduct(kvp.Key.Id, kvp.Value);
                    messageLog += kvp.Key.NomProduitIHM + ", ";
                }

                // Envoyer le paiement à l'API
                orderManager.ProcessOrder();
                
                // Terminer le paiment par acompte
                if (paiementAcompte is InfosPaiementAcompte infos)
                {
                    _ = Task.Run(() =>
                    {
                        StatAcompte stat = new StatAcompte(0, DateTime.Now, infos.prix, infos.acompte.Id);
                        MainWindowViewModel.Instance.StatViewModel.AddStatAcompte(stat);
                        statAcompteManager.CreateStat(stat);
                    });

                    string prixFormatted = ConverterFormatArgent.ConvertToString(infos.prix);
                    infos.acompte.ArgentIHM = ConverterFormatArgent.ConvertToString(infos.argent - infos.prix);
                    messageLog += $"{infos.acompte.IdentifiantIHM} ";
                    messageLog += $"({prixFormatted}) : ";
                    infos.acompte.UpdateAcompte(false, false);
                    this.TextArgentAdherentRetour = $"Il vous reste {infos.acompte.ArgentIHM}";
                    this.RetourAcompteArgent = true;
                }

                // Gérer les stats 
                ObservableDictionary<ProductViewModel, int> productOrder2 = new ObservableDictionary<ProductViewModel, int>(productOrder);
                _ = Task.Run(() =>
                    {
                        foreach (ProductViewModel product in productOrder2.Keys)
                        {
                            StatProduit stat = new StatProduit(0, DateTime.Now, productOrder2[product], product.Id);
                            MainWindowViewModel.Instance.StatViewModel.AddStatProduit(stat);
                            statProduitManager.CreateStat(stat);
                        }
                    });

                // Log l'action
                Log log = new Log(DateTime.Now, 5, messageLog, MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                _ = Task.Run(() => MainWindowViewModel.Instance.LogManager.CreateLog(log));
                MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));

                this.ProductOrder.Clear();
                NotifyPropertyChanged(nameof(PriceAdherIHM));
                NotifyPropertyChanged(nameof(PriceNonAdherIHM));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            // Notifier la vue
            this.ShowPayAcompte = false;
            this.ShowPayPaypal = false;
            this.ShowPayLiquide = false;
            this.ShowPayBanque = false;
            
            

        }

        /// <summary>
        /// Permet de préparer le paiement des articles
        /// </summary>
        private void PreviewPayArticles()
        {
            if (this.PriceAdher != 0 && this.PriceNanAdher != 0)
            {
                switch (currentPaiement)
                {
                    case "Acompte":
                        this.ShowPayAcompte = true;
                        break;
                    case "Paypal":
                        PrixIHM = "Montant : " + (this.isAdherent ? this.PriceAdherIHM : this.PriceNonAdherIHM);
                        this.ShowPayPaypal = true;
                        break;
                    case "Carte":
                        PrixIHM = "Montant : " + (this.isAdherent ? this.PriceAdherIHM : this.PriceNonAdherIHM);
                        this.ShowPayBanque = true;
                        break;
                    case "Liquide":
                        PrixIHM = "Montant : " + (this.isAdherent ? this.PriceAdherIHM : this.PriceNonAdherIHM);
                        this.ShowPayLiquide = true;
                        break;
                }
            }

        }

        /// <summary>
        /// Ajoute un produit au panier
        /// </summary
        public void AddProduct(object product)
        {

            ProductViewModel produitIHM = (ProductViewModel)product;

            if (this.productOrder.ContainsKey(produitIHM))
            {
                if (produitIHM.QuantiteIHM - productOrder[produitIHM] > 0)
                {
                    this.productOrder[produitIHM]++;

                }

            }
            else
            {
                this.productOrder[produitIHM] = 1;
            }

            NotifyPropertyChanged(nameof(PriceAdherIHM));
            NotifyPropertyChanged(nameof(PriceNonAdherIHM));
        }

        /// <summary>
        /// Permet d'enlever un produit
        /// </summary>
        private void RemoveProduct(object product)
        {
            ProductViewModel produitIHM = (ProductViewModel)product;
            if (this.productOrder[produitIHM] == 1)
            {
                this.productOrder.Remove(produitIHM);
            }
            else
            {
                this.productOrder[produitIHM]--;
            }

            NotifyPropertyChanged(nameof(PriceAdherIHM));
            NotifyPropertyChanged(nameof(PriceNonAdherIHM));
        }



        #endregion
    }
}
