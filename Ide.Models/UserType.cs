using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class UserType:BaseModel
    {
        public ICollection<AppUser> appUsers { get; set; } = new List<AppUser>();
    }
}
