using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.InMemory;

namespace Shop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        // GET: ProductManager
        public ActionResult Index()
        {
            context = new ProductRepository();
            List<Product>products=context.Collection().ToList();
             return View(products);

        }
        public ActionResult Create()
        {
            Product product = new Product();
            return View (product);
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
                context.insert (product);
                context .commit ();

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
                return View(product);
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

                context.commit();
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
                context.delete(Id);
                context.commit ();
                return RedirectToAction("Index");
            }
        }
        }
    }
