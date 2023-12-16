using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork=unitOfWork;
        }

        public IQueryable GetAll()
        {
          return  unitOfWork.Products.GetAll();
        }

        public IQueryable GetAllCustomer()
        {
            return unitOfWork.Products.GetAll(u=>u.IsActive==true);
        }

        public Product ProductGetById(int productId)
        {
            return unitOfWork.Products.GetById(productId);
        }

        public void ProductUpdate(Product product)
        {
            unitOfWork.Products.Update(product);
            unitOfWork.Save();
        }
        

    }
}
