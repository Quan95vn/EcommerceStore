namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSomeField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.ImageDetails", new[] { "ProductId" });
            AddColumn("dbo.Products", "MoreImage1", c => c.String());
            AddColumn("dbo.Products", "MoreImage2", c => c.String());
            DropColumn("dbo.Products", "MetaKeyword");
            DropColumn("dbo.Products", "MetaDescription");
            DropColumn("dbo.ProductCategories", "AttributeId");
            DropColumn("dbo.ProductCategories", "MetaKeyword");
            DropColumn("dbo.ProductCategories", "MetaDescription");
            DropTable("dbo.ImageDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImageDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 256),
                        ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductCategories", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductCategories", "AttributeId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "MetaKeyword", c => c.String(maxLength: 256));
            DropColumn("dbo.Products", "MoreImage2");
            DropColumn("dbo.Products", "MoreImage1");
            CreateIndex("dbo.ImageDetails", "ProductId");
            AddForeignKey("dbo.ImageDetails", "ProductId", "dbo.Products", "Id");
        }
    }
}
