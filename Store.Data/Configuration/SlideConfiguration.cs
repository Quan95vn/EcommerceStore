using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Configuration
{
    public class SlideConfiguration : EntityTypeConfiguration<Slide>
    {
        public SlideConfiguration()
        {
            ToTable("Slides");

            HasKey(s => s.Id);

            Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(s => s.Name).IsRequired().HasMaxLength(256);
            Property(s => s.Image).IsRequired().HasMaxLength(256);

        }
    }
}
