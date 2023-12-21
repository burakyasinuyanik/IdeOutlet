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
    public class OrderProductTypeService:IOrderProductTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderProductTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork= unitOfWork;
        }

        public void ChangeOrderProductType(int orderId, int orderProductTypeId, int productId)
        {
            Order order = unitOfWork.Orders.GetAll(u => u.Id == orderId).Include(u => u.OrderProducts).FirstOrDefault();
            
            foreach(OrderProduct item in order.OrderProducts)
            {
                if(item.Id == productId)
                {
                    item.OrderProductTypeId = orderProductTypeId;
                }
            }
            unitOfWork.OrderProducts.Update(order.OrderProducts.FirstOrDefault(u => u.Id == productId));
            unitOfWork.Save();
        }

        public IQueryable GetOrderProductStatus()
        {
            return unitOfWork.OrderProductTypes.GetAll();
            
        }
    }
}
