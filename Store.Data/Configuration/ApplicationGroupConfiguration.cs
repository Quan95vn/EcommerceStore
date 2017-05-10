using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Configuration
{
    public class ApplicationGroupConfiguration : EntityTypeConfiguration<ApplicationGroup>
    {
        public ApplicationGroupConfiguration()
        {
            ToTable("ApplicationGroups");

            HasKey(x => x.Id);

            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(250);
            Property(x => x.Description).HasMaxLength(250);
        }
    }
}
