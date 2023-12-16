using Ide.Business.Abstract;
using Ide.Repository.Shared.Abstract;
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
    }
}
