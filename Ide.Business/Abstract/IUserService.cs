using Ide.Models;
using Ide.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Abstract
{
    public interface IUserService
    {
        AppUser Login(string email, string password);
        AppUser UserAdd(LoginAndAddUserDto loginAndAddUser);
        bool UserContains(string email);
        IQueryable<AppUser> UserGetAll();
        AppUser GetById(int id);
        AppUser UserUpdate(AppUser user);
    }
}
