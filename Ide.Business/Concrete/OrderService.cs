using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Concrete
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable GetAll(string mail)
        {
            return unitOfWork.Orders.GetAll().Include(u=>u.AppUser).Include(u=>u.OrderProducts).Where(u=>u.AppUser.Email== mail);
        }

        public Order NewOrder(string mail)
        {
          ShoppingBasket basket=  unitOfWork.ShoppingBaskets.GetAll().Include(u => u.AppUsers.Where(u => u.Email == mail)).Include(u => u.Products).FirstOrDefault();

            if (basket.Products != null)
            {
                Order order = new Order();
                List<OrderProduct> orderProductsList = new List<OrderProduct>();

              foreach(var product in basket.Products)
                {
                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.Barcode=product.Barcode;
                    orderProduct.ProductNo = product.ProductNo;
                    orderProduct.Picture = product.Picture;
                    orderProduct.Price = product.Price;
                    orderProduct.Description = product.Description;
                    orderProduct.Name = product.Name;
                    orderProduct.Status = "bekiyor";
                    orderProductsList.Add(orderProduct);
                    
                }
                order.OrderProducts = orderProductsList;
                order.AppUser=unitOfWork.Users.GetAll(u => u.Email == mail).First();
                basket.Products.Clear();
            
                unitOfWork.Orders.Add(order);
                unitOfWork.Save();
                return order;
            }
            else
            {
                return null;
            }
            
        }
    }
}
