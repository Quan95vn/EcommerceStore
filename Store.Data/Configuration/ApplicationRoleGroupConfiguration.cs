using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    public class ApplicationRoleGroupConfiguration : EntityTypeConfiguration<ApplicationRoleGroup>
    {
        public ApplicationRoleGroupConfiguration()
        {
            ToTable("ApplicationRoleGroups");
            HasKey(x => new { x.GroupId, x.RoleId });
            Property(x => x.RoleId).HasMaxLength(128);

            HasRequired(x => x.ApplicationRole).WithMany().HasForeignKey(x => x.RoleId);
            HasRequired(x => x.ApplicationGroup).WithMany().HasForeignKey(x => x.GroupId);
        }
    }
}
