using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models.DTOs
{
    public class LoginAndAddUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gsm { get; set; }
        public int UserTypeId { get; set; } = 2;
        public bool IsPersistent { get; set; } = false;
    }
}
