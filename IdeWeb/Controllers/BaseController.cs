using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class BaseController : Controller
    {

        public readonly IUnitOfWork unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
