﻿using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class FakeCategoryDAO : ICategory
    {
        private List<string> _categories = new List<string>()
        {
            "BOISSON",
            "SNACKS",
            "HIDDEN",
            "PABLO"
        };

        public void CreateCategory(string ca)
        {
            _categories.Add(ca);
        }

        public void DeleteCategory(string ca)
        {
            _categories.Remove(ca);
        }

        public string GetCategory(string category)
        {
            return _categories.Find(x => x == category);
        }

        public List<string> ListALlCategory()
        {
            return _categories;
        }

        public void UpdateCategory(string baseCategory, string category)
        {
            throw new NotImplementedException();
        }
    }
}
