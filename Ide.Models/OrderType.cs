using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class OrderType:BaseModel
    {
        public ICollection<Order> Orders { get; set; }
    }
}
