using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Shop.DataAccess.InMemory
{
    public  class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcategories;

        public ProductCategoryRepository()
        {
            productcategories = cache["productcategories"] as List<ProductCategory>;
            if (productcategories == null)
            {
                productcategories = new List<ProductCategory>();
            }
        }
        public void commit()
        {
            cache["productcategories"] = productcategories;
        }
        public void insert(ProductCategory productcategory)
        {
            productcategories.Add(productcategory);
        }
        public void update(ProductCategory productcategory)
        {
            ProductCategory productCategoryToUpdate = productcategories.Find(p => p.Id == productcategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productcategory;
            }
            else
            {
                throw new Exception("product category not found");
            }
        }
        public ProductCategory Find(String Id)
        {
            ProductCategory productCategory = productcategories.Find(p => p.Id == p.Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("product category not found");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productcategories.AsQueryable();
        }
        public void delete(String Id)
        {
            ProductCategory productCategoryToDelete = productcategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productcategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("product category no found");
            }
        }
    }
}
