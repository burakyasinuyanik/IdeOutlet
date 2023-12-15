using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class ShoppingBasket:BaseModel
    {
    
         public virtual ICollection< AppUser> AppUsers { get; set; }=new List< AppUser>();
        public virtual ICollection<Product> Products { get; set; }= new List<Product>();
    }
}
