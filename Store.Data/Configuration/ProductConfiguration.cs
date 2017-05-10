using Store.Model.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
          
            ToTable("Products");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(256);
            Property(p => p.Alias).IsRequired().HasMaxLength(256);
            Property(p => p.ThumbImage).HasMaxLength(256);
            Property(p => p.MoreImages).HasColumnType("xml");

            Property(p => p.CreatedBy).HasMaxLength(256);
            Property(p => p.UpdatedBy).HasMaxLength(256);
            Property(p => p.MetaKeyword).HasMaxLength(256);
            Property(p => p.MetaDescription).HasMaxLength(256);
         
            HasMany(p => p.OrderDetails).WithRequired(p => p.Product).HasForeignKey(od => od.ProductId);
        }
    }
}
