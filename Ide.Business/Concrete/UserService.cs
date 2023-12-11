using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public AppUser Login(string email, string password)
        {
            AppUser appUser = unitOfWork.Users.GetAll(u => u.Email == email && u.Password == password).Include(u=>u.UserType).FirstOrDefault();
            return appUser;
        }
    }
}
