using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class Product:BaseModel
    {
        public string? Picture { get; set; }

        public double Price { get; set; }
        public string Barcode { get; set; }
        public string ProductNo { get; set; }
        public int Stock { get; set; }
        public int? RemainingStock { get; set; }
       public int? ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<ShoppingBasket> ShoppingBaskets { get; set; } = new List<ShoppingBasket>();

    }
}
