using Ide.Business.Abstract;
using Ide.Repository.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Concrete
{
    public class OrderTypeService:IOrderTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork=unitOfWork;
        }

        public IQueryable GetOrderStatus()
        {
            return unitOfWork.OrderTypes.GetAll();
        }
    }
}
