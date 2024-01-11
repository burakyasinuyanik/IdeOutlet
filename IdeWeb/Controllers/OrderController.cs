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
        [HttpPost]
      

        public IActionResult NewOrder(string mail)
        {
           if(shopingBasketService.BasketNull(mail))
            {
                int ıd = orderService.NewOrder(mail).Id;
                string message = "Sipariş Numarınız : " + ıd;
                return Ok(new { result = true, message = message });
            }
            else
            {
                return Ok(new {result=false,message="sipariş oluşmadı"});
            }
           

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

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
        [HttpPost]

        public IActionResult GetAll(string mail)
        {

            return Json(orderService.GetAll(mail));
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
