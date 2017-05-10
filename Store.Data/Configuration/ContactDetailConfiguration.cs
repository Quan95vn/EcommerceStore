using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    public class ContactDetailConfiguration : EntityTypeConfiguration<ContactDetail>
    {
        public ContactDetailConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.Email).IsRequired().HasMaxLength(50);
            Property(x => x.Message).HasMaxLength(500);
            Property(x => x.Address).HasMaxLength(100);
            Property(x => x.Website).IsRequired().HasMaxLength(50);
        }
    }
}
