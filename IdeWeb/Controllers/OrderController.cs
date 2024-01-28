using Ide.Business.Abstract;
using Ide.Business.Concrete;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;

namespace Ide.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IOrderTypeService orderTypeService;
        private readonly IOrderProductTypeService orderProductTypeService;
        private readonly IShopingBasketService shopingBasketService;
        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService, IOrderTypeService orderTypeService, IOrderProductTypeService orderProductTypeService, IShopingBasketService shopingBasketService) : base(unitOfWork)
        {
            this.orderService = orderService;
            this.orderTypeService = orderTypeService;
            this.orderProductTypeService = orderProductTypeService;
            this.shopingBasketService = shopingBasketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
     
        public IActionResult SendMail(int orderId)
        {

            return Ok(new {result=orderService.MailMessage(orderId)});
        }


        [HttpPost]
        public async Task <IActionResult> NewOrder(string mail)
        {

            try
            {
                if (shopingBasketService.BasketNull(mail))
                {
                    await orderService.NewOrder(mail);
                    string message = "Sipariş Numarınız : " +orderService.LastOrderId(mail);
                    return Ok(new { result = true, message = message });
                }
                else
                {
                    return Ok(new { result = false, message = "Sipariş oluşmadı.Sepeteinizde ürün bulunmamaktadır." });
                }
            }catch(Exception ex)
            {
                return BadRequest(new { result = false, message = "Sipariş Oluşturulurken Hata Oluştu. Tekrar Deneyiniz. " + ex.Message });


            }



        }
        [HttpPost]
        

        public IActionResult ConsentOrderProductPrice(int orderId)
        {
            
            return Json(orderService.ConsentOrderProductPrice(orderId));
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult AnnulmentOrderProductPrice(int orderId)
        {
            
            return Json(orderService.AnnulmentOrderProductPrice(orderId));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeOrderProductType(int orderId, int orderProductTypeId,int productId,string productNo)
        {
            orderProductTypeService.ChangeOrderProductType(orderId, orderProductTypeId, productId, productNo);

            return Ok(new { result = true, message = "Ürün Stasüsü Değişti !" });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeOrderType(int orderId,int orderTypeId)
        {
            orderService.ChangeOrderType(orderId, orderTypeId);
            return Ok(new {result =true,message="Sipariş Stasüsü Değişti !"});
        }

        public IActionResult PageSlice(int userId)
        {

            double productCount = (unitOfWork.Orders.GetAll(u=>u.AppUser.Id==userId).ToList().Count() / 5.00); ;
            double Number2 = Math.Round(productCount, MidpointRounding.ToPositiveInfinity);
            if (Number2 == 0)
                Number2 = 1;
            return Json(new
            {
                PageCount = Number2.ToString(),

            });
        }

        [HttpPost]
        public IActionResult GetAll(int id,int pageId)
        {

            return Json(orderService.GetAll(id,pageId));
        }
        [Authorize(Roles = "Admin")]

        public IActionResult OrderControl()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        public IActionResult GetAllFull()
        {
            return Json(new {data=orderService.GetAllFull()});
        }
        [Authorize(Roles = "Admin")]

        [Route("/OrderPage/{orderId}")]
       public IActionResult OrderPage(int orderId)
        {
            return View(orderService.GetOrderPage(orderId));
        }
        [Authorize(Roles = "Admin")]

        public IActionResult OrderDetail(int orderId)
        {
           
            return Json(orderService.GetOrderDetail( orderId));
        }
        [Authorize(Roles = "Admin")]

        public IActionResult GetOrderStatus()
        {
            return Json(orderTypeService.GetOrderStatus());
        }
        [Authorize(Roles = "Admin")]

        public IActionResult GetOrderProductStatus()
        {
            return Json(orderProductTypeService.GetOrderProductStatus());
        }
    }
}
