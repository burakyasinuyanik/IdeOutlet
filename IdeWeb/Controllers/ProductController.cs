using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
