using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class ProductType:BaseModel
    {
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
