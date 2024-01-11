using Ide.Business.Abstract;
using Ide.Models;
using Ide.Models.DTOs;
using Ide.Repository.Shared.Abstract;
using Ide.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ide.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        public UserController(IUnitOfWork unitOfWork, IUserService userService) : base(unitOfWork)
        {
            this.userService = userService;
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
           
           
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginAndAddUserDto loginAndAddUser)
        {
           AppUser appUser= userService.Login(loginAndAddUser.Email, loginAndAddUser.Password);
            if(appUser!=null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, appUser.Email));
                claims.Add(new Claim(ClaimTypes.MobilePhone, appUser.Gsm));
                claims.Add(new Claim(ClaimTypes.Role,appUser.UserType.Name));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                ClaimsIdentity claimsIdentity= new ClaimsIdentity(claims,"login");

                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = loginAndAddUser.IsPersistent });


                TempData["success"] = "Giriş Başarılı !";


                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Kullanıcı adı veya şifre hatalı";
                return View();

            }

        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        
        [HttpPost]
        [AllowAnonymous]

        public IActionResult UserAdd(LoginAndAddUserDto loginAndAddUser)
        {
            try
            {

                if (userService.UserContains(loginAndAddUser.Email))
                {
                  
                    return Ok(new { result = false, message = "Kullanıcı Mevcut!" });

                }
                userService.UserAdd(loginAndAddUser);
               

                return Ok(new { result = true, message = "Kullanıcı Eklendi!" });

            }
            catch (Exception ex)
            {
                TempData["error"] = "Kayıt Başarısız !";

                return BadRequest(ex.InnerException.Message);

            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult UserShow()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UserGetAll()
        {
            return Json(new { data = userService.UserGetAll() });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult UserGetById(int userId)
        {
            return Json(userService.GetById(userId));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult UserUpdate(AppUser user)
        {
            userService.UserUpdate(user);
            return Ok();
        }
    }
}
