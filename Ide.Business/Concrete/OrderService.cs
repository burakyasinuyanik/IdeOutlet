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

        public void ChangeOrderType(int orderId, int orderTypeId)
        {
            Order order = unitOfWork.Orders.GetById(orderId);
            order.OrderTypeId = unitOfWork.OrderTypes.GetById(orderTypeId).Id;
            unitOfWork.Orders.Update(order);
            unitOfWork.Save();
        }

        public IQueryable GetAll(string mail)
        {
            return unitOfWork.Orders.GetAll().Include(u=>u.AppUser).Include(u=>u.OrderProducts).ThenInclude(u=>u.OrderProductType).Include(u=>u.OrderType).Where(u=>u.AppUser.Email== mail);
        }

        public IQueryable GetAllFull()
        {
          return  unitOfWork.Orders.GetAll().Include(u => u.AppUser).Include(u => u.OrderProducts);
        }

        public Order GetOrderDetail(int orderId)
        {
            return unitOfWork.Orders.GetAll(u => u.Id == orderId).Include(u => u.OrderProducts).FirstOrDefault();
        }

        public Order GetOrderPage(int orderId)
        {
           return unitOfWork.Orders.GetAll(u => u.Id == orderId).Include(u => u.AppUser).Include(u => u.OrderProducts).FirstOrDefault();
        }

        public Order NewOrder(string mail)
        {
          ShoppingBasket basket=  unitOfWork.ShoppingBaskets.GetAll().Include(u => u.AppUsers.Where(u => u.Email == mail)).Include(u => u.Products).FirstOrDefault();

            if (basket.Products != null)
            {
                Order order = new Order();
                order.OrderTypeId = unitOfWork.OrderTypes.GetFirstOrDefault(u => u.Name.Contains("bek")).Id;
                List<OrderProduct> orderProductsList = new List<OrderProduct>();
                double totalAmount = 0;
                int orderProductTypeId = unitOfWork.OrderProductTypes.GetFirstOrDefault(u => u.Name.Contains("bek")).Id;
              foreach(var product in basket.Products)
                {
                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.OrderProductTypeId= orderProductTypeId;
                    orderProduct.Barcode=product.Barcode;
                    orderProduct.ProductNo = product.ProductNo;
                    orderProduct.Picture = product.Picture;
                    orderProduct.Price = product.Price;
                    totalAmount += product.Price;
                    orderProduct.Description = product.Description;
                    orderProduct.Name = product.Name;
                   
                    orderProductsList.Add(orderProduct);
                    
                }
                order.OrderProducts = orderProductsList;
                order.TotalAmount = totalAmount;
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
