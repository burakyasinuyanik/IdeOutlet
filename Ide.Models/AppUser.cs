using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class AppUser:BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gsm { get; set; }
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public int ShoppingBasketId { get; set; }
        public virtual ShoppingBasket ShoppingBasket { get; set; } = new ShoppingBasket();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
