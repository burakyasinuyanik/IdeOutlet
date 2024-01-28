using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models.DTOs
{
    public class LoginAndAddUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gsm { get; set; }
        public int UserTypeId { get; set; } 
        public bool IsPersistent { get; set; } = false;
        public string AddPas { get; set; }
    }
}
