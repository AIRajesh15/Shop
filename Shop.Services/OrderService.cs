using Shop.Core.Contracts;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class OrderService:IOrderService 
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order>OrderContext)
        {
            this.orderContext = OrderContext;
        }

        public void CreateOrder(Order baseorder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseorder.OrderItems.Add(new OrderItem()
                {
                    ProductId =item.id,
                    Image=item.Image,
                    Price=item.Price ,
                    ProductName=item .ProductName,
                    Quantity=item .Quantity ,

                    
                });
            }
            orderContext.Insert(baseorder);
            orderContext.Commit();
        }

        public List<Order> GetOrderList()
        {
            return orderContext.Collection().ToList();
        }

        public Order GetOrder(string Id)
        {
            return orderContext.Find(Id); 
        }

        public void UpdateOrder(Order updateOrder)
        {
            orderContext.Update(updateOrder);
            orderContext.Commit();
        }
    }
}
