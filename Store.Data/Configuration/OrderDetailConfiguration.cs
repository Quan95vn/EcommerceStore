using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Store.Data.Configuration
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            HasKey(od => new { od.OrderId, od.ProductId });
        }
    }
}
