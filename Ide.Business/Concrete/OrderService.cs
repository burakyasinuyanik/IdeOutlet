using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Ide.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
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

        public double AnnulmentOrderProductPrice(int orderId)
        {
            Order order = unitOfWork.Orders.GetAll(u => u.Id == orderId).Include(u => u.OrderProducts).FirstOrDefault();
            double annulmentPrice = 0;
            foreach (OrderProduct item in order.OrderProducts)
            {
                if (item.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll(u => u.Name.Contains("iptal")).FirstOrDefault().Id)
                    annulmentPrice += item.Price;
            }
            return annulmentPrice;
        }

        public void ChangeOrderType(int orderId, int orderTypeId)
        {
            Order order = unitOfWork.Orders.GetById(orderId);
            order.OrderTypeId = unitOfWork.OrderTypes.GetById(orderTypeId).Id;
            unitOfWork.Orders.Update(order);
            unitOfWork.Save();
        }

        public double ConsentOrderProductPrice(int orderId)
        {
            Order order = unitOfWork.Orders.GetAll(u => u.Id == orderId).Include(u => u.OrderProducts).FirstOrDefault();
            double consentPrice = 0;
            foreach(OrderProduct item in order.OrderProducts)
            {
                if(item.OrderProductTypeId==unitOfWork.OrderProductTypes.GetAll(u=>u.Name.Contains("ona")).FirstOrDefault().Id)
                consentPrice += item.Price;
            }
            return consentPrice;
        }

        public List<Order> GetAll(int id, int pageId)
        {
            int skip = 0;
            int take = 5;

            if (pageId > 1)
            {
                skip = take * (pageId - 1);
            }


            return unitOfWork.Orders.GetAll(u => u.AppUser.Id == id).Include(u=>u.OrderProducts).ThenInclude(u=>u.OrderProductType).Include(u=>u.OrderType).OrderByDescending(u=>u.Id).Skip(skip).Take(take).ToList();
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
          ShoppingBasket basket=  unitOfWork.ShoppingBaskets.GetAll().Include(u => u.AppUsers).Include(u => u.Products).Where(u=>u.AppUsers.Any(u=>u.Email==mail)).FirstOrDefault();

            if (basket.Products.Count > 0)
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


        public bool MailMessage(int orderId)
        {

           Order order= unitOfWork.Orders.GetAll(o => o.Id == orderId).Include(o=>o.AppUser).Include(o => o.OrderProducts).ThenInclude(o => o.OrderProductType).FirstOrDefault();
          
            string mail=  order.AppUser.Email;
            string subject = $"{order.Id}' nolu Siparişiniz Hakkında";
            double price = 0;
            List<string> onaylananUrunler = new List<string>();
            List<string> iptalEdilenUrunler = new List<string>();

            order.OrderProducts.Where(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll(o => o.Name.ToLower().Contains("onay")).FirstOrDefault().Id).ToList().ForEach(o => price += o.Price);
            order.OrderProducts.Where(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll(o => o.Name.ToLower().Contains("onay")).FirstOrDefault().Id).ToList().ForEach(o => onaylananUrunler.Add(o.Name+" "+o.Price+" TL"));
            order.OrderProducts.Where(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll(o => o.Name.ToLower().Contains("iptal")).FirstOrDefault().Id).ToList().ForEach(o => iptalEdilenUrunler.Add(o.Name+" "+o.Price+" TL"));

            string b = "";
            string c = "";
            onaylananUrunler.ForEach(o => b += o+"<br>");
            iptalEdilenUrunler.ForEach(o => c += o + "<br>");

            string a = $"Merhaba {order.AppUser.Name},<br>" +
                $"{orderId}'nolu Siparişindeki aşağıdaki ürünlerin satın alman için onaylandı.<br>" +
                $"Ödemeyi altta bulunan Iban'a outlet-{orderId} açıklaması ile 24 saat içinde yatırman gerekiyor.<br>" +
                $"IBAN Bilgisi: Turkuvaz Müzik Kitap Mağazacılık Pazarlama A.Ş. BNK:DENİZBANK - TR20 0013 4000 0039 7116 0000 07<br>" +
                $"Ödeme yaptıktan sonra bu mail üzerinden dekontunu paylaşmanı rica ediyoruz.<br>" +
                $"Toplam Ödenecek Tutar: {price} TL<br>" +
                $"Siparişinde satın almaya hak kazandığın ürünler;<br>" +
                $"{b}<br>" +
                $"Satın almaya hak kazanamadığın ürünler;<br>" +
                $"{c}<br>" +
                $"<br>Ödemeniz ulaştığında ürününüzü 8.katta bulunan idefix SSH Ofisi’ne getirteceğiz.<br>" +
                $"Ekibimiz, ürününüzü teslim etmek için sizi bilgilendirecek ve gerekli kontrolleri sağlayarak ürünü sizlere elden teslim edecektir.<br>" +
                $"<br>Teşekkürler İyi Alışverişler<br>" +
                $"SSH"
                
                ;
           bool mailBool= SendMail.mailSend(mail, subject, a);

            return mailBool;
        }
    }
}
