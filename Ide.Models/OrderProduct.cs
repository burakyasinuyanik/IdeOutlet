using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class OrderProduct:BaseModel
    {
        
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string? Picture { get; set; }

        public double Price { get; set; }
        public string Barcode { get; set; }
        public string ProductNo { get; set; }
        public int Stock { get; set; }
        public string Status { get; set; }
       



    }
}
