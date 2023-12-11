using Ide.Business.Abstract;
using Ide.Models;
using Ide.Models.DTOs;
using Ide.Repository.Shared.Abstract;
using Ide.Utility;

using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        public UserController(IUnitOfWork unitOfWork, IUserService userService) : base(unitOfWork)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
           
           
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginAndAddUserDto loginAndAddUser)
        {
           AppUser appUser= userService.Login(loginAndAddUser.Email, loginAndAddUser.Password);
            if(appUser!=null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
