using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Concrete
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IQueryable<UserType> GetAll()
        {
            return unitOfWork.UserTypes.GetAll();
        }
    }
}
