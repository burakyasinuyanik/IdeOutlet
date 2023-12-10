using Ide.Utility;

using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class UserController : Controller
    {
      
        public IActionResult Index()
        {
           
           
            return View();
        }
    }
}
