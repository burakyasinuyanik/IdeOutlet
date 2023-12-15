using Ide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Abstract
{
    public interface IUserTypeService
    {
        IQueryable<UserType> GetAll();
    }
}
