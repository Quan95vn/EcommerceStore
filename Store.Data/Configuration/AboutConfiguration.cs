using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    public class AboutConfiguration : EntityTypeConfiguration<About>
    {
        public AboutConfiguration()
        {
            ToTable("Abouts");

            Property(a => a.Id).IsRequired().HasMaxLength(50);
            Property(a => a.Content).IsRequired();
        }
    }
}
