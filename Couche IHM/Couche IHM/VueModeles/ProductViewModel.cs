﻿

using Couche_IHM.ImagesProduit;
using Couche_Métier;
using Couche_Métier.Utilitaire;
using Microsoft.Win32;
using Modeles;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Couche_IHM.VueModeles
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        #region attributes
        /// <summary>
        /// Représente le modèle produit
        /// </summary>
        private Product product;
        private ImageManager imageManager;
        private ConverterFormatArgent formatArgent;
        private BitmapImage image;
        private ProductManager productManager;
        private CategoryManager categoryManager;
        private int quantiteIHM;
        private string nomProduitIHM;
        private CategoryViewModel categoryIHM;
        private string prixAdherentIHM;
        private string prixNonAdherentIHM;
        private int purchaseCount;
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region events
        public RelayCommand CreateProd { get; set; }
        public RelayCommand ResetProd { get; set; }
        public RelayCommand UpdateProd { get; set; }
        public RelayCommand DeleteProd { get; set; }

        public RelayCommand ChangeImage { get;set; }
        #endregion

        #region properties

        /// <summary>
        /// Permet de savoir si le produit est disponible
        /// </summary>
        public bool isDisponible
        {
            get
            {
                return (product.Quantite > 0);
            }
        }

        /// <summary>
        /// Renvoie la quantite du produit
        /// </summary>
        public int QuantiteIHM
        {
            get
            {
                return quantiteIHM;
            }
            set 
            { 
                quantiteIHM = value;
                MainWindowViewModel.Instance.ProductViewModel.ShowModifButtons = true;
            }
        }

        /// <summary>
        /// Nom du produit
        /// </summary>
        public string NomProduitIHM
        {
            get => nomProduitIHM;
            set
            {
                nomProduitIHM = value;
                MainWindowViewModel.Instance.ProductViewModel.ShowModifButtons = true;
            }
        }

        /// <summary>
        /// Prix Adherent formatté pour l'afficher
        /// </summary>
        public string PrixAdherentIHM
        {
            get => prixAdherentIHM;
            set 
            { 
                prixAdherentIHM = value;
            }
        }
        /// <summary>
        /// Prix non adhérent formatté pour l'afficher
        /// </summary>
        public string PrixNonAdherentIHM
        {
            get => prixNonAdherentIHM;
            set 
            { 
                prixNonAdherentIHM = value; 
            }
        }

        /// <summary>
        /// Categorie du produit
        /// </summary>
        public CategoryViewModel CategoryIHM
        {
            get => categoryIHM;
            set 
            { 
                categoryIHM = value;
                MainWindowViewModel.Instance.ProductViewModel.ShowModifButtons = true;
            }
        }

        public string Action
        {
            get
            {
                return this.product.NomProduit == "" ? "NEW" : "UPDATE";
            }
        }

        /// <summary>
        /// Image du produit
        /// </summary>
        public BitmapImage ImageProduct 
        {
            get => image;
            set 
            { 
                image = value;
                NotifyPropertyChanged();
            }
            
        }

        public int PurchaseCount { get => purchaseCount; set => purchaseCount = value; }
        public Product Product { get => product; set => product = value; }




        #endregion

        public ProductViewModel(Product product,ProductManager productManager,CategoryManager categoryManager,CategoryViewModel categoryProduit,int r)
        {
            // Initialisation du modele
            this.product = product;
            this.purchaseCount = r;
            // Initialisation des objets metiers
            this.categoryManager = categoryManager;
            this.imageManager = new ImageManager();
            this.formatArgent = new ConverterFormatArgent();
            this.productManager = productManager;

            // Initialisation des attributsIHM
            this.categoryIHM = categoryProduit;
            this.quantiteIHM = product.Quantite;
            this.nomProduitIHM = product.NomProduit;
            this.image = new BitmapImage(new Uri(imageManager.GetImageFromProduct(this.NomProduitIHM), UriKind.Absolute));
            this.prixNonAdherentIHM = formatArgent.ConvertToString(product.PrixNonAdherent);
            this.prixAdherentIHM = formatArgent.ConvertToString(product.PrixAdherent);

            // Initialisation des events
            this.ResetProd = new RelayCommand(x => ResetProduct());
            this.UpdateProd = new RelayCommand(x => UpdateProduct());
            this.CreateProd = new RelayCommand(x => CreateProduct());
            this.DeleteProd = new RelayCommand(x => DeleteProduct());
            this.ChangeImage = new RelayCommand(x => ChangeImageProduct()); 

        }

        #region methods
        /// <summary>
        /// Permet de changer l'image du produit
        /// </summary>
        public void ChangeImageProduct()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.png)|*.jpg; *.png|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string nomImage =openFileDialog.FileName;
                this.ImageProduct = new BitmapImage(new Uri(nomImage, UriKind.Absolute));
                NotifyPropertyChanged(nameof(ImageProduct));
            }
        }

        public void DeleteCatNotify()
        {
            this.categoryIHM = null;
            NotifyPropertyChanged(nameof(this.CategoryIHM));
        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void UpdateProduct(bool doLog = true)
        {
            if (this.categoryIHM != null)
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

                // Changer la data
                this.product.Quantite = this.quantiteIHM;
                this.product.NomProduit = this.nomProduitIHM;
                ImageManager imageManager = new ImageManager();
                byte[] bitsImage = imageManager.ConvertImageToBlob(image.UriSource.ToString());
                imageManager.CreateImageFromBlob(this.nomProduitIHM, bitsImage);
                
                this.product.Categorie = this.categoryManager.Categories.Find(x => x.NomCategory == categoryIHM.NomCat).IdCat;
                this.product.PrixAdherent = formatArgent.ConvertToDouble(this.prixAdherentIHM);
                this.product.PrixNonAdherent = formatArgent.ConvertToDouble(this.prixNonAdherentIHM);
                this.productManager.UpdateProduct(this.product);

                // Log l'action
                if (doLog)
                {
                    Log log = new Log(0, DateTime.Now, 3, $"Modification du produit : {this.NomProduitIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                    MainWindowViewModel.Instance.LogManager.CreateLog(log);
                    MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
                }

                // Notifier la vue
                NotifyPropertyChanged(nameof(NomProduitIHM));
                NotifyPropertyChanged(nameof(QuantiteIHM));
                NotifyPropertyChanged(nameof(CategoryIHM));
                NotifyPropertyChanged(nameof(isDisponible));
                MainWindowViewModel.Instance.ProductViewModel.ShowProductDetail = false;
                MainWindowViewModel.Instance.ProductViewModel.ShowModifButtons = false;
            }
            else
            {
                MessageBox.Show("Vous n'avez pas sélectionné de catégory");
            }

        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void DeleteProduct()
        {
            // Changer la data
            this.productManager.RemoveProduct(this.product);

            // Log l'action
            Log log = new Log(0, DateTime.Now, 3, $"Suppression du produit : {this.NomProduitIHM}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
            MainWindowViewModel.Instance.LogManager.CreateLog(log);

            // Notifier la vue
            MainWindowViewModel.Instance.ProductViewModel.RemoveProduct(this);
            MainWindowViewModel.Instance.LogsViewModel.AddLog(new LogViewModel(log));
            MainWindowViewModel.Instance.ProductViewModel.ShowProductDetail = false;
            MainWindowViewModel.Instance.ProductViewModel.ShowModifButtons = false;

        }

        /// <summary>
        /// Permet de mettre à jour visuellement les modifications de l'adhérent
        /// </summary>
        public void CreateProduct()
        {
            if (this.categoryIHM != null)
            {
                ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

                // Changer la data
                this.product.Quantite = this.quantiteIHM;
                this.product.NomProduit = this.nomProduitIHM;
                ImageManager imageManager = new ImageManager();
                byte[] bitsImage = imageManager.ConvertImageToBlob(image.UriSource.ToString());
                imageManager.CreateImageFromBlob(this.nomProduitIHM, bitsImage);
                this.product.Categorie = this.categoryManager.Categories.Find(x => x.NomCategory == categoryIHM.NomCat).IdCat;
                this.product.PrixAdherent = formatArgent.ConvertToDouble(this.prixAdherentIHM);
                this.product.PrixNonAdherent = formatArgent.ConvertToDouble(this.prixNonAdherentIHM);
                this.productManager.CreateProduct(this.product);

                // Log l'action
                Log log = new Log(0, DateTime.Now, 3, $"Ajout du produit : {product.NomProduit}", MainWindowViewModel.Instance.CompteConnected.NomCompletIHM);
                MainWindowViewModel.Instance.LogManager.CreateLog(log);


                // Notifier la vue
                MainWindowViewModel.Instance.ProductViewModel.AddProduct(this);
                NotifyPropertyChanged(nameof(NomProduitIHM));
                NotifyPropertyChanged(nameof(QuantiteIHM));
                NotifyPropertyChanged(nameof(CategoryIHM));
                NotifyPropertyChanged(nameof(isDisponible));
                MainWindowViewModel.Instance.LogsViewModel.Logs.Insert(0, new LogViewModel(log));
                MainWindowViewModel.Instance.ProductViewModel.ShowProductDetail = false;
                MainWindowViewModel.Instance.ProductViewModel.ShowModifButtons = false;
            }
            else
            {
                MessageBox.Show("Vous n'avez pas sélectionné de catégory");
            }
            
        }

        /// <summary>
        /// Permet de reset les propriétés de l'adhérent
        /// </summary>
        public void ResetProduct()
        {
            ConverterFormatArgent converterFormatArgent = new ConverterFormatArgent();

            // Initialisation propriétés
            if(this.categoryIHM != null)
            {
                this.categoryIHM.NomCat = this.categoryManager.Categories.Find(x => x.IdCat == product.Categorie).NomCategory;
            }

           
            this.quantiteIHM = product.Quantite;
            this.nomProduitIHM = product.NomProduit;
            this.image = new BitmapImage(new Uri(imageManager.GetImageFromProduct(this.NomProduitIHM), UriKind.Absolute));
            this.prixNonAdherentIHM = formatArgent.ConvertToString(product.PrixNonAdherent);
            this.prixAdherentIHM = formatArgent.ConvertToString(product.PrixAdherent);

            // Notifier la vue
            NotifyPropertyChanged(nameof(ImageProduct));
            NotifyPropertyChanged(nameof(CategoryIHM));
            NotifyPropertyChanged(nameof(QuantiteIHM));
            NotifyPropertyChanged(nameof(NomProduitIHM));
            NotifyPropertyChanged(nameof(PrixNonAdherentIHM));
            NotifyPropertyChanged(nameof(PrixAdherentIHM));
            MainWindowViewModel.Instance.ProductViewModel.ShowProductDetail = false;
            MainWindowViewModel.Instance.ProductViewModel.ShowModifButtons = false;
        }

        public override bool Equals(object? obj)
        {
            return obj is ProductViewModel model &&
                   nomProduitIHM == model.nomProduitIHM;
        }


        #endregion
    }
}
