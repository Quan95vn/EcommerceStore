using System;
using System.Collections.Generic;

namespace Store.Model.Models
{
    public class Order
    {
        public int Id { set; get; }
        public string CustomerName { set; get; }
        public string CustomerAddress { set; get; }
        public string CustomerEmail { set; get; }
        public string CustomerMobile { set; get; }
        public string CustomerMessage { set; get; }
        public string PaymentMethod { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string PaymentStatus { set; get; }
        public bool Status { set; get; }
        public string CustomerId { set; get; }
        public virtual ApplicationUser User { set; get; }

        public virtual ICollection<OrderDetail> OrderDetails { set; get; }
    }
}
