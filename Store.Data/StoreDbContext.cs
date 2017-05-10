using Microsoft.AspNet.Identity.EntityFramework;
using Store.Data.Configuration;
using Store.Model.Models;
using System.Data.Entity;

namespace Store.Data
{
    public class StoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreDbContext() : base("StoreDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Slide> Slides { get; set; }

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static StoreDbContext Create()
        {
            return new StoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);

            builder.Configurations.Add(new AboutConfiguration());
            builder.Configurations.Add(new CategoryConfiguration());
            builder.Configurations.Add(new ContactDetailConfiguration());
            builder.Configurations.Add(new FeedbackConfiguration());
            builder.Configurations.Add(new FooterConfiguration());
            builder.Configurations.Add(new OrderConfiguration());
            builder.Configurations.Add(new OrderDetailConfiguration());
            builder.Configurations.Add(new ProductConfiguration());
            builder.Configurations.Add(new SlideConfiguration());

            builder.Configurations.Add(new ApplicationGroupConfiguration());
            builder.Configurations.Add(new ApplicationRoleGroupConfiguration());
            builder.Configurations.Add(new ApplicationUserGroupConfiguration());

            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}