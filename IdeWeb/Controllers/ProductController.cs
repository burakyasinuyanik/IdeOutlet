using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        public ProductController(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            
            return Json(new { data = productService.GetAll() });
        }
        
        public IActionResult ProductCustom(int id)
        {

            return View();
        }
        [HttpPost]
        public IActionResult ProductUpdate(Product product)
        {
            productService.ProductUpdate(product);

            return Ok();
        }
        public IActionResult ProductGetById(int productId)
        {

            return Json(productService.ProductGetById(productId));
        }
        public IActionResult GetAllCustomer()
        {
            return Json(new { data = productService.GetAllCustomer() });
        }
    }
}
