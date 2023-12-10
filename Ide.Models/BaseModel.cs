using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; } = new Guid();
        public string? Name { get; set; }
        public string? Description { get; set; }
      
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public DateTime DateCraeted { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }


    }
}
