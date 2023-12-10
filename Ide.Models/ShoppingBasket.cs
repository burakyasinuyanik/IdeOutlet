using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class ShoppingBasket:BaseModel
    {
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Product> Products { get; set; }= new List<Product>();
    }
}
