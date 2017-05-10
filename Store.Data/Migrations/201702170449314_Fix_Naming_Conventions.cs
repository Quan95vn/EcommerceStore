namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Naming_Conventions : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductCategories", newName: "Categories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Categories", newName: "ProductCategories");
        }
    }
}
