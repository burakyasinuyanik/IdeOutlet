using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Ide.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Ide.Web.Controllers
{
    public class DashBoardController : BaseController
    {
        public DashBoardController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult TotalOrder()
            {
          
           

            return Json(unitOfWork.Orders.GetAll().OrderBy(o => o.DateCraeted).GroupBy(
                   o => new { o.DateCraeted.Year, o.DateCraeted.Month, o.DateCraeted.Day }).Select(o =>
                  new
                  {
                      DateCraeted = o.Key,
                      OrderCount = o.Count(),
                      TotalAmaunt = o.Sum(o => o.TotalAmount),
                     
                  }
                  ));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult OrderCase()
        {

            return Json(unitOfWork.Orders.GetAll().GroupBy(o => o.OrderType.Name).Select(o => new
            {
                OrderTypeName = o.Key,
                OrderTypeCount = o.Count()
            })) ;
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult OrderProductCase()
        {
            return Json(unitOfWork.OrderProducts.GetAll().Include(o=>o.OrderProductType).GroupBy(o => o.OrderProductType.Name).Select(o => new
            {
                OrderProductTypeName = o.Key,
                OrderProductTypeCount = o.Count()
            }));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ProductCase()
        {
            List<Product> product = unitOfWork.Products.GetAll().ToList();
            double totalStock = 0;
            double remainingStock = 0;
            foreach(Product productItem in product)
            {
                totalStock += productItem.Stock;
                remainingStock += productItem.RemainingStock.ToInt();
            }
            double percentRemainingStock = (remainingStock * 100) / totalStock;
            return Json(new { percentRemainingStock = percentRemainingStock.ToString("0.00").Replace(",",".") });
        }
       
        [Authorize(Roles = "Admin")]
        public IActionResult ConfirmationOrderProductCase()
        {
            
           return  Json(unitOfWork.OrderProducts.GetAll(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll().Where(o => o.Name.ToLower().Contains("onay")).First().Id).GroupBy(o => new { o.DateModified.Year, o.DateModified.Month, o.DateModified.Day }).Select(
                o => new
                {
                    DateModified = o.Key,
                    OrderProductPrice = o.Sum(o => o.Price),
                }
                ));
        }
       
        [Authorize(Roles = "Admin")]
        public IActionResult PendingOrderProductCase()
        {

            return Json(unitOfWork.OrderProducts.GetAll(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll().Where(o => o.Name.ToLower().Contains("bek")).First().Id).GroupBy(o => new { o.DateModified.Year, o.DateModified.Month, o.DateModified.Day }).Select(
                 o => new
                 {
                     DateModified = o.Key,
                     OrderProductPrice = o.Sum(o => o.Price),
                 }
                 ));
        }
       
        [Authorize(Roles = "Admin")]
        public IActionResult AnnulmentOrderProductCase()
        {

            return Json(unitOfWork.OrderProducts.GetAll(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll().Where(o => o.Name.ToLower().Contains("iptal")).First().Id).GroupBy(o => new { o.DateModified.Year, o.DateModified.Month, o.DateModified.Day }).Select(
                 o => new
                 {
                     DateModified = o.Key,
                     OrderProductPrice = o.Sum(o => o.Price),
                 }
                 ));
        }
      
        [Authorize(Roles = "Admin")]
        public IActionResult TotalCustomer()
        {

            return Json(unitOfWork.Users.GetAll().OrderBy(o => o.DateCraeted).GroupBy(
                   o => new { o.DateCraeted.Year, o.DateCraeted.Month, o.DateCraeted.Day }).Select(o =>
                  new
                  {
                      DateCraeted = o.Key,
                      CustomerCount = o.Count(),
                     

                  }
                  )); 
        }
    }
}
