﻿using Ide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll(int id,int pageId);
        Task NewOrder(string mail);
        IQueryable GetAllFull();
        public Order GetOrderDetail(int orderId);
        public Order GetOrderPage(int orderId);
        public void ChangeOrderType(int orderId, int orderTypeId);
        public double ConsentOrderProductPrice(int orderId);
        public double AnnulmentOrderProductPrice(int orderId);
        public bool MailMessage(int orderId);
        public int LastOrderId(string mail);
    }
}
