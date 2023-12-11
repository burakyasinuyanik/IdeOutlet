using Ide.Models;
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

    }
}
