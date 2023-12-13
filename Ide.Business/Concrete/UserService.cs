using Ide.Business.Abstract;
using Ide.Models;
using Ide.Models.DTOs;
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

        public AppUser UserAdd(LoginAndAddUserDto loginAndAddUserDto)
        {
            AppUser appUser = new AppUser();
            appUser.Email = loginAndAddUserDto.Email;
            appUser.Password = loginAndAddUserDto.Password;
            appUser.Gsm = loginAndAddUserDto.Gsm;
            appUser.UserTypeId = unitOfWork.UserTypes.GetAll().Where(u => u.Name.Contains("musteri")).FirstOrDefault().Id;
            unitOfWork.Users.Add(appUser);
            unitOfWork.Save();

            return appUser;
        }

        public bool UserContains(string email)
        {
            return unitOfWork.Users.GetAll().Where(u=>u.Email==email).Any();
        }

        public IQueryable<AppUser> UserGetAll()
        {
            return unitOfWork.Users.GetAll().Include(u=>u.UserType);
        }

        public AppUser GetById(int id)
        {
            return unitOfWork.Users.GetById(id);
        }
    }
}
