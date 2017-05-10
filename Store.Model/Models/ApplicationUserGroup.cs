using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model.Models
{
    public class ApplicationUserGroup
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ApplicationGroup ApplicationGroup { get; set; }
    }
}
