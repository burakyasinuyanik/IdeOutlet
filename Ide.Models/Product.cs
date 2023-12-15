using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class Product:BaseModel
    {
        public double Price { get; set; }
        public string Barcode { get; set; }
        public string ProductNo { get; set; }
        public int Stock { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<ShoppingBasket> ShoppingBaskets { get; set; } = new List<ShoppingBasket>();

    }
}
