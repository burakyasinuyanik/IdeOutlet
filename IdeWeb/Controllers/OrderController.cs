using Ide.Business.Abstract;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IOrderTypeService orderTypeService;
        private readonly IOrderProductTypeService orderProductTypeService;
        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService, IOrderTypeService orderTypeService, IOrderProductTypeService orderProductTypeService) : base(unitOfWork)
        {
            this.orderService = orderService;
            this.orderTypeService = orderTypeService;
            this.orderProductTypeService = orderProductTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewOrder(string mail)
        {

            int ıd = orderService.NewOrder(mail).Id;
            string message= "Sipariş Numarınız : " + ıd;
            return Ok(new { result = true, message = message });

        }

        [HttpPost]
        public IActionResult ChangeOrderProductType(int orderId, int orderProductTypeId,int productId)
        {
            orderProductTypeService.ChangeOrderProductType(orderId, orderProductTypeId, productId);

            return Ok(new { result = true, message = "Ürün Stasüsü Değişti !" });
        }
        [HttpPost]
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

        public IActionResult OrderControl()
        {
            return View();
        }
        public IActionResult GetAllFull()
        {
            return Json(new {data=orderService.GetAllFull()});
        }
     
       [Route("/OrderPage/{orderId}")]
       public IActionResult OrderPage(int orderId)
        {
            return View(orderService.GetOrderPage(orderId));
        }

        public IActionResult OrderDetail(int orderId)
        {
           
            return Json(orderService.GetOrderDetail( orderId));
        }

        public IActionResult GetOrderStatus()
        {
            return Json(orderTypeService.GetOrderStatus());
        }
        public IActionResult GetOrderProductStatus()
        {
            return Json(orderProductTypeService.GetOrderProductStatus());
        }
    }
}
