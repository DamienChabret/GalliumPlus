﻿
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    public class FakeProduitsDAO : IProductDAO
    {
        private List<Product> products = new List<Product>();

        public FakeProduitsDAO() 
        {
            //Connection
            string connString = String.Format("server={0};port={1};user id={2};password={3};database={4};SslMode={5}", "51.178.36.43", "3306", "c2_gallium", "DfD2no5UJc_nB", "c2_gallium", "none");
            MySqlConnection mySqlConnection = new MySqlConnection(connString);
            mySqlConnection.Open();

            //Requette SQL
            string stm = "SELECT * FROM Products ORDER BY name";
            MySqlCommand cmd = new MySqlCommand(stm, mySqlConnection);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<Product> products = new List<Product>();

            while (rdr.Read())
            {
                products.Add(new Product(rdr.GetInt32("product_id"), rdr.GetString("name"), rdr.GetInt32("stock"), rdr.GetFloat("price_na"), rdr.GetFloat("price_a"),getRandomCategorie()));
            }

            mySqlConnection.Close();
            this.products = products;
            /**
            products.Add(new Product(1, "Coca cola", 20, 22, 0.80f, getRandomCategorie()));
            products.Add(new Product(2, "Fanta", 20, 22, 0.80f, getRandomCategorie()));
            products.Add(new Product(3,"Monster", 20, 22, 1.20f, getRandomCategorie()));
            products.Add(new Product(4,"SUPER MONSTER", 1, 22, 2.20f, getRandomCategorie()));
            products.Add(new Product(5,"Pablo", 1, 22, 500, getRandomCategorie()));
            products.Add(new Product(6,"Carotte", 30, 22, 0.20f, getRandomCategorie()));
            products.Add(new Product(7,"Chocolat blanc", 15, 22, 1, getRandomCategorie()));
            products.Add(new Product(8,"Chocolat rouge", 1, 22, 1.50f, getRandomCategorie()));
            products.Add(new Product(9,"Monster ETIQ", 999, 22, 50, getRandomCategorie()));
            products.Add(new Product(10,"Monster Jus de pablo", 1, 22, 220, getRandomCategorie()));
            products.Add(new Product(11, "Monster Infernale", 3, 22, 1.50f, getRandomCategorie()));
            products.Add(new Product(12,"Tomate noire", 23, 22, 20, getRandomCategorie()));
            products.Add(new Product(13, "Chaire pourrie", 122, 22, 0.50f, getRandomCategorie()));
            products.Add(new Product(14, "Ane", 0, 22, 10000, getRandomCategorie()));
            products.Add(new Product(15,"Eau", 9, 22, 0.20f, getRandomCategorie()));
            products.Add(new Product(16, "XXX", 666, 22, 2, getRandomCategorie()));
            products.Add(new Product(17,"TOP SECRET", 1, 22, 999, getRandomCategorie()));
            products.Add(new Product(18,"Larme de sardoches", 999, 22, 0.20f, getRandomCategorie()));
            products.Add(new Product(19, "Epee de saske", 213, 22, 21, getRandomCategorie()));
            products.Add(new Product(20, "Pyamide de france anglaise", 1, 22, 500, getRandomCategorie()));
            products.Add(new Product(21, "MATTEO BADET", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(22,"Chien en laisse", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(23, "Chien sans laisse", 3, 22, 10, getRandomCategorie()));
            products.Add(new Product(24, "API Sumup", 243, 22, 1, getRandomCategorie()));
            products.Add(new Product(25, "Mouguel", 2, 22, 10, getRandomCategorie()));
            products.Add(new Product(26, "BDSM", 23, 22, 1000, getRandomCategorie()));
            products.Add(new Product(27, "Songoku", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(28, "Thé", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(29, "Bière blonde", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(30, "Bière brune", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(31, "Arobase @", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(32, "Souris logitech", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(33, "Carte graphique", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(34, "Poudre de perlinpinpidoupin", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(35, "Gato", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(36, "Ice tea", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(37, "Kinder buenp", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(38, "Craprice des dieux", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(39, "Louis devie", 23, 22, 10, getRandomCategorie()));
            products.Add(new Product(40, "Non", 23, 22, 10, getRandomCategorie()));**/
        }
        public void CreateProduct(Product product)
        {
            this.products.Add(product);
        }

        public Product? GetProduct(int id)
        {
           return this.products.Find(x => id == x.ID);
        }

        /// <summary>
        /// Récupère tous les produits d'une catégorie
        /// </summary>
        /// <returns> liste de produits </returns>
        public List<Product> GetProductsByCategory(string category)
        {
            return this.products.FindAll(x => category == x.Categorie);
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public void RemoveProduct(Product product)
        {
            this.products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            Product p = GetProduct(product.ID);
            p.NomProduit = product.NomProduit;
            p.PrixNonAdherent = product.PrixNonAdherent;
            p.PrixAdherent = product.PrixAdherent;
            p.Quantite = product.Quantite;
            p.Categorie = product.Categorie;
        }


        private string getRandomCategorie()
        {
            List<string> categories =new List<string>(){
                "BOISSON",
                "SNACKS",
                "HIDDEN",
                "PABLO",
                "test"
            };
            return categories[new Random().Next(0, categories.Count)];
        }
    }
}
