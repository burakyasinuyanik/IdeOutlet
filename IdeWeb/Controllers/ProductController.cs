using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [Authorize(Roles = "Admin")]
        public IActionResult ProductCustom(int id)
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ProductUpdate(Product product)
        {
            productService.ProductUpdate(product);

            return Ok();
        }
        public IActionResult ProductGetById(int productId)
        {

            return Json(productService.ProductGetById(productId));
        }

        public IActionResult GetProductRemainingStock(string productNo)
        {
            return Json(productService.GetProductRemainingStock(productNo));
        }

        public IActionResult GetAllCustomer()
        {
            return Json(new { data = productService.GetAllCustomer() });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> NewProduct(Product product)
        {
          
            try {
               await productService.NewProduct(product);
            
            }
            catch(Exception ex)
            {
                return BadRequest(new { result = false, message = "ürün eklenirken hata oluştu! " + ex.Message });
            }
            return Ok(new { result = true, message = "Ürün eklendi !" });

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPicture(int productId, string picture)
        {

            try
            {
                await productService.AddProductPicture(productId, picture);

            }
            catch (Exception ex)
            {
                return BadRequest(new { result = false, message = "ürün resmi eklenirken hata oluştu! " + ex.Message });
            }
            return Ok(new { result = true, message = "Ürün resmi güncellendi !" });

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int productId)
        {

            productService.Delete(productId);
            return Ok();
        }
    }
}
