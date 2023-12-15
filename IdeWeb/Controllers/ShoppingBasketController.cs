using Ide.Business.Abstract;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class ShoppingBasketController : BaseController
    {
        private readonly IShopingBasketService shopingBasketService;
        public ShoppingBasketController(IUnitOfWork unitOfWork, IShopingBasketService shopingBasketService) : base(unitOfWork)
        {
            this.shopingBasketService = shopingBasketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll(string mail)
        {

            return Json(shopingBasketService.GetAll(mail));
        }
        [HttpPost]
        public IActionResult ProductDown(string mail,int id)
        {
            shopingBasketService.ProductDown(mail, id);
        
            return Ok(new { result = true, message = "Ürün sepetinizden çıkartıldı !" });
        }
    }
}
