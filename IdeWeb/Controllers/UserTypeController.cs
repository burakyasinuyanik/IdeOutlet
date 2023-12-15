using Ide.Business.Abstract;
using Ide.Repository.Shared.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Ide.Web.Controllers
{
    public class UserTypeController : BaseController
    {
        private readonly IUserTypeService userTypeService;
        public UserTypeController(IUnitOfWork unitOfWork,IUserTypeService userTypeService) : base(unitOfWork)
        {
            this.userTypeService = userTypeService;
        }

        public IActionResult GetAll()
        {
            return Json (userTypeService.GetAll());
        }
    }
}
