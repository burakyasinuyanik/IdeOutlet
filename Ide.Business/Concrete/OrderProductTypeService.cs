using Ide.Business.Abstract;
using Ide.Repository.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Concrete
{
    public class OrderProductTypeService:IOrderProductTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderProductTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork= unitOfWork;
        }

        public IQueryable GetOrderProductStatus()
        {
            return unitOfWork.OrderProductTypes.GetAll();
            
        }
    }
}
