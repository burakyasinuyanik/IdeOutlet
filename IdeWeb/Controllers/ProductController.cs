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
        public IActionResult ProductDetail(int productId)
        {
            return View(productService.ProductGetById(productId));
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
        [HttpPost]
        public IActionResult GetAllForCustomer(int page,string search)
        {
            return Json(new { data = productService.GetAllForCustomer(page,search) });
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
        [Authorize(Roles = "Admin")]

        public IActionResult PageSlice(string search)
        {

            double productCount = (unitOfWork.Products.GetAll(p => p.IsActive == true&& search != null ? p.Name.ToLower().Contains(search) : true ).ToList().Count() / 20.0); ;
            double Number2 = Math.Round(productCount ,MidpointRounding.ToPositiveInfinity);
            if (Number2 == 0)
                Number2 = 1;
            return Json(new
            {
                PageCount=Number2.ToString(),
               
            }) ;
        }
        public async Task<IActionResult> ExcelAddProduct()
        {
            try {
                IFormCollection form = Request.Form;

               await productService.ExcelAddProduct(form);
                return Ok(new { result = true, message = "Ürünler Eklendi" });

            }
            catch(Exception ex)
            {
                return BadRequest(new { result = false, message = "Ürünler eklenirken hata oluştu: " + ex.Message });

            }
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
