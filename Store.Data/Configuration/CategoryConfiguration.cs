using Store.Model.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {

            HasMany(pr => pr.Products)
                .WithOptional(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(false);

            ToTable("Categories");
            HasKey(pr => pr.Id);
            Property(pr => pr.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(pr => pr.Name).IsRequired().HasMaxLength(256);
            Property(pr => pr.Alias).IsRequired().HasMaxLength(256);

            Property(pr => pr.CreatedBy).HasMaxLength(256);
            Property(pr => pr.UpdatedBy).HasMaxLength(256);
            Property(pr => pr.MetaKeyword).HasMaxLength(256);
            Property(pr => pr.MetaDescription).HasMaxLength(256);
        }
    }
}
