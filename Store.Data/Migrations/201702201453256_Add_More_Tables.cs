namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_More_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cpus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Screens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Categories", "ParentId", c => c.Int());
            AddColumn("dbo.Products", "MoreImages", c => c.String(storeType: "xml"));
            AddColumn("dbo.Products", "BrandId", c => c.Int());
            AddColumn("dbo.Products", "CpuId", c => c.Int());
            AddColumn("dbo.Products", "ScreenId", c => c.Int());
            CreateIndex("dbo.Products", "BrandId");
            CreateIndex("dbo.Products", "CpuId");
            CreateIndex("dbo.Products", "ScreenId");
            CreateIndex("dbo.Categories", "ParentId");
            AddForeignKey("dbo.Products", "BrandId", "dbo.Brands", "Id");
            AddForeignKey("dbo.Categories", "ParentId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Products", "CpuId", "dbo.Cpus", "Id");
            AddForeignKey("dbo.Products", "ScreenId", "dbo.Screens", "Id");
            DropColumn("dbo.Products", "MoreImage1");
            DropColumn("dbo.Products", "MoreImage2");
            DropColumn("dbo.Products", "MoreImage3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MoreImage3", c => c.String());
            AddColumn("dbo.Products", "MoreImage2", c => c.String());
            AddColumn("dbo.Products", "MoreImage1", c => c.String());
            DropForeignKey("dbo.Products", "ScreenId", "dbo.Screens");
            DropForeignKey("dbo.Products", "CpuId", "dbo.Cpus");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "ScreenId" });
            DropIndex("dbo.Products", new[] { "CpuId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropColumn("dbo.Products", "ScreenId");
            DropColumn("dbo.Products", "CpuId");
            DropColumn("dbo.Products", "BrandId");
            DropColumn("dbo.Products", "MoreImages");
            DropColumn("dbo.Categories", "ParentId");
            DropTable("dbo.Screens");
            DropTable("dbo.Cpus");
            DropTable("dbo.Brands");
        }
    }
}
