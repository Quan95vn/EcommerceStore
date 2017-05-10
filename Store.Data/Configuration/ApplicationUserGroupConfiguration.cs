using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    public class ApplicationUserGroupConfiguration : EntityTypeConfiguration<ApplicationUserGroup>
    {
        public ApplicationUserGroupConfiguration()
        {
            ToTable("ApplicationUserGroups");
            HasKey(x => new { x.UserId, x.GroupId });
            Property(x => x.UserId).HasMaxLength(128);

            HasRequired(x => x.ApplicationUser).WithMany().HasForeignKey(x => x.UserId);
            HasRequired(x => x.ApplicationGroup).WithMany().HasForeignKey(x => x.GroupId);
        }
    }
}
