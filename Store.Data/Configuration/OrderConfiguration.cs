using Store.Model.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");

            HasKey(o => o.Id);
            Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(o => o.CustomerName).IsRequired().HasMaxLength(256);
            Property(o => o.CustomerAddress).IsRequired().HasMaxLength(256);
            Property(o => o.CustomerEmail).IsRequired().HasMaxLength(256);
            Property(o => o.CustomerMobile).IsRequired().HasMaxLength(50);
            Property(o => o.PaymentMethod).HasMaxLength(256);

            Property(o => o.CustomerId).HasMaxLength(128).HasColumnType("nvarchar");

            HasOptional(u => u.User).WithMany(u => u.Orders).HasForeignKey(o => o.CustomerId);
            HasMany(o => o.OrderDetails).WithRequired(od => od.Order).HasForeignKey(od => od.OrderId);
          
        }
    }
}
