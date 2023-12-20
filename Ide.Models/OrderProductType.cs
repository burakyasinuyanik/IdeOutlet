using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class OrderProductType:BaseModel
    {
        public ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    }
}
