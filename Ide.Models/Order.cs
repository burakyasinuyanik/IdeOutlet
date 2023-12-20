using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class Order : BaseModel
    {
        public double? TotalAmount { get; set; }
        public int? OrderTypeId { get; set; }
        public virtual OrderType? OrderType { get; set; }

        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
       
        public virtual ICollection< OrderProduct> OrderProducts { get; set; }= new List<OrderProduct>();
       
    }
}
