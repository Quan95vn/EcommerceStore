using Store.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace Store.Data.Configuration
{
    class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasMany(u => u.Orders).WithOptional(o => o.User).HasForeignKey(o => o.CustomerId);
        }
    }
}
