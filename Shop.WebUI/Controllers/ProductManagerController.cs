using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.InMemory;
using Shop.Core.ViewModels;

namespace Shop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository <Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory>productCategoryContext)
        {
            context =productContext;
            productCategories = productCategoryContext;                
        }

        // GET: ProductManager
        public ActionResult Index()
        {
           
            List<Product>products=context.Collection().ToList();
             return View(products);

        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel=new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories =productCategories .Collection ();
            return View (viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid )
            {
                return View(product);
            }
            else
            {
                context.Insert (product);
                context .Commit ();

                return RedirectToAction ("Index");
            }
        }
        public ActionResult Edit(String Id)
        {
            Product product = context.Find(Id);
            if(product ==null)
            {
                return HttpNotFound ();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories .Collection ();

                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product,String Id )
        {
            Product productToEdit = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit .Category = product.Category;
                productToEdit .Description = product.Description;
                productToEdit .Name = product.Name;
                productToEdit .Price = product.Price;
                productToEdit.Image = product.Image;

                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(String Id)
        {
            Product productToDelete=context.Find (Id);
            {
                if(productToDelete == null)
                {
                    return HttpNotFound ();
                }
                else
                {
                    return View(productToDelete);
                }
            }
        }
        [HttpPost ]
        [ActionName ("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            Product productToDelete= context.Find (Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.tToDelete(Id);
                context.Commit ();
                return RedirectToAction("Index");
            }
        }
        }
    }
