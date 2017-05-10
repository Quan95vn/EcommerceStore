using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Web.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public virtual OrderViewModel Order { get; set; }

        public int ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}