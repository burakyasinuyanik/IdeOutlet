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

        public void ChangeOrderProductType(int orderId, int orderProductTypeId, int productId, string productNo)
        {
            Order order = unitOfWork.Orders.GetAll(u => u.Id == orderId).Include(u => u.OrderProducts).FirstOrDefault();
            
            foreach(OrderProduct item in order.OrderProducts)
            {   
               

                if (item.Id == productId)
                {

                    if (unitOfWork.OrderProductTypes.GetById(item.OrderProductTypeId.Value).Name.ToLower().Contains("bek") && unitOfWork.OrderProductTypes.GetById(orderProductTypeId).Name.ToLower().Contains("onay"))
                    {
                        Product product = unitOfWork.Products.GetFirstOrDefault(u => u.ProductNo == productNo);
                        product.RemainingStock -= 1;
                        unitOfWork.Products.Update(product);
                    }

                    if (unitOfWork.OrderProductTypes.GetById(item.OrderProductTypeId.Value).Name.ToLower().Contains("onay") && unitOfWork.OrderProductTypes.GetById(orderProductTypeId).Name.ToLower().Contains("bek"))
                    {
                        Product product = unitOfWork.Products.GetFirstOrDefault(u => u.ProductNo == productNo);
                        product.RemainingStock += 1;
                        unitOfWork.Products.Update(product);
                    }
                    if (unitOfWork.OrderProductTypes.GetById(item.OrderProductTypeId.Value).Name.ToLower().Contains("onay") && unitOfWork.OrderProductTypes.GetById(orderProductTypeId).Name.ToLower().Contains("iptal"))
                    {
                        Product product = unitOfWork.Products.GetFirstOrDefault(u => u.ProductNo == productNo);
                        product.RemainingStock += 1;
                        unitOfWork.Products.Update(product);
                    }
                    if (unitOfWork.OrderProductTypes.GetById(item.OrderProductTypeId.Value).Name.ToLower().Contains("iptal") && unitOfWork.OrderProductTypes.GetById(orderProductTypeId).Name.ToLower().Contains("onay"))
                    {
                        Product product = unitOfWork.Products.GetFirstOrDefault(u => u.ProductNo == productNo);
                        product.RemainingStock -= 1;
                        unitOfWork.Products.Update(product);
                    }
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
