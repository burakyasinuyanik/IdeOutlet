using Ide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Abstract
{
    public interface IOrderService
    {
        IQueryable GetAll(string mail);
        public Order NewOrder(string mail);
        IQueryable GetAllFull();
        public Order GetOrderDetail(int orderId);
        public Order GetOrderPage(int orderId);
        public void ChangeOrderType(int orderId, int orderTypeId);

    }
}
