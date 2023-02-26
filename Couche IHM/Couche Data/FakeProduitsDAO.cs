﻿using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class FakeProduitsDAO : IProductDAO
    {
        public void CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product("Coca cola", 20, 0.80f, getRandomCategorie()));
            products.Add(new Product("Fanta", 20, 0.80f, getRandomCategorie()));
            products.Add(new Product("Monster", 20, 1.20f, getRandomCategorie()));
            products.Add(new Product("SUPER MONSTER", 1, 2.20f, getRandomCategorie()));
            products.Add(new Product("Pablo", 1, 500f, getRandomCategorie())); 
            products.Add(new Product("Carotte", 30, 0.20f, getRandomCategorie())); 
            products.Add(new Product("Chocolat blanc", 15, 1f, getRandomCategorie())); 
            products.Add(new Product("Chocolat rouge", 1, 1.50f, getRandomCategorie())); 
            products.Add(new Product("Monster ETIQ", 999, 50f, getRandomCategorie())); 
            products.Add(new Product("Monster Jus de pablo", 1, 220f, getRandomCategorie())); 
            products.Add(new Product("Monster Infernale", 3, 1.50f, getRandomCategorie())); 
            products.Add(new Product("Tomate noire", 23, 20f, getRandomCategorie())); 
            products.Add(new Product("Chaire pourrie", 122, 0.50f, getRandomCategorie())); 
            products.Add(new Product("Ane", 0, 10000f, getRandomCategorie())); 
            products.Add(new Product("Eau", 9, 0.20f, getRandomCategorie())); 
            products.Add(new Product("XXX", 666, 2f, getRandomCategorie())); 
            products.Add(new Product("TOP SECRET", 1, 999f, getRandomCategorie())); 
            products.Add(new Product("Larme de sardoches", 999, 0.20f, getRandomCategorie())); 
            products.Add(new Product("Epee de saske", 213, 21, getRandomCategorie())); 
            products.Add(new Product("Pyamide de france anglaise", 1, 500f, getRandomCategorie())); 
            products.Add(new Product("MATTEO BADET", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Chien en laisse", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Chien sans laisse", 3, 10f, getRandomCategorie())); 
            products.Add(new Product("API Sumup", 243, 1f, getRandomCategorie())); 
            products.Add(new Product("Mouguel", 2, 10f, getRandomCategorie())); 
            products.Add(new Product("BDSM", 23, 1000f, getRandomCategorie())); 
            products.Add(new Product("Songoku", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Thé", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Bière blonde", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Bière brune", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Arobase @", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Souris logitech", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Carte graphique", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Poudre de perlinpinpidoupin", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Gato", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Ice tea", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Kinder buenp", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Craprice des dieux", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Louis devie", 23, 10f, getRandomCategorie())); 
            products.Add(new Product("Non", 23, 10f, getRandomCategorie())); 
            return products;
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        private string getRandomCategorie()
        {
            CategoryManager c = new CategoryManager(new FakeCategoryDAO());
            List<string> categories = c.ListAllCategory();
            return categories[new Random().Next(0, categories.Count)];
        }
    }
}
