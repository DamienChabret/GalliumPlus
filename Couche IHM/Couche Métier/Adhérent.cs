﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    public class Adhérent
    {
        private string id;
        private string nom;
        private string prenom;
        private bool canPass;
        private float argent;

        /// <summary>
        /// Constructeur de la classe adhérent
        /// </summary>
        /// <param name="id">id de l'adhérent</param>
        /// <param name="nom">nom de l'adhérent</param>
        /// <param name="prenom">prenom de l'adhérent</param>
        /// <param name="canPass">si le mdp peut être skip</param>
        /// <param name="argent">argent de l'adhérent</param>
        public Adhérent(string id, string nom, string prenom, bool canPass, float argent)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.canPass = canPass;
            this.argent = argent;
        }

        /// <summary>
        /// Id de l'adhérent
        /// </summary>
        public string Id { get => id; set => id = value; }

        /// <summary>
        /// Nom de l'adhérent
        /// </summary>
        public string Nom { get => nom; set => nom = value; }

        /// <summary>
        /// Prénom de l'adhérent
        /// </summary>
        public string Prenom { get => prenom; set => prenom = value; }

        /// <summary>
        /// Le mot de passe peut-il être facultatif
        /// </summary>
        public bool CanPass { get => canPass; set => canPass = value; }

        /// <summary>
        /// Argent de l'adhérent
        /// </summary>
        public float Argent { get => argent; set => argent = value; }
    }
}
