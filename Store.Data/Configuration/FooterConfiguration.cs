using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Configuration
{
    public class FooterConfiguration : EntityTypeConfiguration<Footer>
    {
        public FooterConfiguration()
        {
            ToTable("Footers");

            HasKey(f => f.Id);

            Property(f => f.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
