using Ide.Business.Abstract;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService) : base(unitOfWork)
        {
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewOrder(string mail)
        {

            int ıd = orderService.NewOrder(mail).Id;
            string message= "Sipariş Numarınız" + ıd;
            return Ok(new { result = true, message = message });

        }
        [HttpPost]
        public IActionResult GetAll(string mail)
        {

            return Json(orderService.GetAll(mail));
        }
    }
}
