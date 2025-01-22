using Shop.Core.Contracts;
using Shop.Core.Models;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Shop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        // GET: Basket
        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService BasketService, IOrderService orderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = orderService;
            this.customers = Customers;
        }
        public ActionResult Index()
        {
            
            ViewBag.message = "Basket Page";
            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            
            Customer customer = customers.Collection().FirstOrDefault(c=>c.Email==User.Identity.Name);
            if(customer!=null)
            {
                Order order=new Order()
                {
                    Email=customer .Email,
                    City =customer .City,
                    State =customer .State,
                    Street =customer .Street,
                    FirstName =customer .FirstName,
                    Surname =customer.LastName ,
                    Zipcode =customer .Zipcode
                };
                return View(order);
            }
            else
            {
                return RedirectToAction(" ERROR!!! ");
            }
      
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email =User.Identity .Name;
            //payment process
            order.OrderStatus = "payment processed";
            orderService.CreateOrder(order,basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("THANKYOU", new { OrderId =order.Id });
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}