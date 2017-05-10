namespace Store.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Store.Data.StoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Store.Data.StoreDbContext context)
        {
            CreateContactDetailSample(context);
            CreateUser(context);
        }

        private void CreateUser(StoreDbContext context)
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new StoreDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new StoreDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "StoppingSky",
            //    Email = "StoppingSky@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Trần Ngọc Quân"

            //};

            //manager.Create(user, "paul1234");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("StoppingSky@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateContactDetailSample(Store.Data.StoreDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var contactDetail = new ContactDetail()
                {
                    Name = "Shop quần áo",
                    Address = "Số 10 Lý Thường Kiệt Hà Nội",
                    Email = "crazylove94hn1@gmail.com",
                    Lat = 21.0215701,
                    Lng = 105.8562524,
                    PhoneNumber = 0989473631,
                    Website = "QuanShop.com",
                    Status = true
                };
                context.ContactDetails.Add(contactDetail);
                context.SaveChanges();
            }
        }
    }
}
