using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Ide.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ide.Web.Controllers
{
    public class DashBoardController : BaseController
    {
        public DashBoardController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult OrderCase()
        {

            return Json(unitOfWork.Orders.GetAll().GroupBy(o => o.OrderType.Name).Select(o => new
            {
                OrderTypeName = o.Key,
                OrderTypeCount = o.Count()
            })) ;
        }
        public IActionResult OrderProductCase()
        {
            return Json(unitOfWork.OrderProducts.GetAll().Include(o=>o.OrderProductType).GroupBy(o => o.OrderProductType.Name).Select(o => new
            {
                OrderProductTypeName = o.Key,
                OrderProductTypeCount = o.Count()
            }));
        }
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

        public IActionResult ConfirmationOrderProductCase()
        {
            double totalPrice = 0;
            //  unitOfWork.OrderProducts.GetAll(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll().Where(o => o.Name.Contains("onay")).First().Id).ToList().ForEach(o => totalPrice += o.Price);
            unitOfWork.OrderProducts.GetAll(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll().Where(o => o.Name.Contains("onay")).First().Id).GroupBy(o => new { o.DateModified.Year, o.DateModified.Month, o.DateModified.Day }).Select(
                o => new
                {
                    DateModified=o.Key,
                    OrderProductPrice=o.Sum(o => o.Price),
                }
                );
           return  Json(unitOfWork.OrderProducts.GetAll(o => o.OrderProductTypeId == unitOfWork.OrderProductTypes.GetAll().Where(o => o.Name.Contains("onay")).First().Id).GroupBy(o => new { o.DateModified.Year, o.DateModified.Month, o.DateModified.Day }).Select(
                o => new
                {
                    DateModified = o.Key,
                    OrderProductPrice = o.Sum(o => o.Price),
                }
                ));
        }
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
