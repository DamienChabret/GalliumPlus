﻿
using Modeles;

namespace Couche_Data
{
    /// <summary>
    /// Produit
    /// </summary>
    public interface IProductDAO
    {
        /// <summary>
        /// Récupère tous les produits
        /// </summary>
        /// <returns> liste de produits </returns>
        public List<Product> GetProducts();

     

        /// <summary>
        /// retire un produit
        /// </summary>
        /// <param name="product"> produit à retirer </param>
        public void RemoveProduct(Product product);

        /// <summary>
        /// Créer un produit 
        /// </summary>
        /// <param name="product"> produit à créer </param>
        public void CreateProduct(Product product);

        /// <summary>
        /// Modifie un produit
        /// </summary>
        /// <param name="product"> Produit à modifier </param>
        public void UpdateProduct(Product product);

 
    }
}
