using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ide.Business.Concrete
{
    public class ShoppingBasketService : IShopingBasketService
    {
        private readonly IUnitOfWork unitOfWork;

        public ShoppingBasketService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        
     
        public IQueryable GetAll(string mail)
        {
            return unitOfWork.Products.GetAll().Where(u => u.ShoppingBaskets.Any(u => u.AppUsers.Any(u => u.Email == mail)));
        }

        public void ProductDown(string mail, int id)
        {
         ShoppingBasket basket = unitOfWork.ShoppingBaskets.GetAll().Where(u => u.AppUsers.Any(u => u.Email == mail)).Include(u => u.Products).FirstOrDefault();

            basket.Products.Remove(unitOfWork.Products.GetFirstOrDefault(u => u.Id == id));
            unitOfWork.Save();
        }
    }
}
