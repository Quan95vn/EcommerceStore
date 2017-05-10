namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSomeField2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            DropColumn("dbo.ProductCategories", "ParentId");
            DropColumn("dbo.ProductCategories", "HomeFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategories", "HomeFlag", c => c.Boolean());
            AddColumn("dbo.ProductCategories", "ParentId", c => c.Int());
            CreateIndex("dbo.ProductCategories", "ParentId");
            AddForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories", "Id");
        }
    }
}
