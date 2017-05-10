namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeThings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Products", "CpuId", "dbo.Cpus");
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CpuId" });
            DropColumn("dbo.Products", "BrandId");
            DropColumn("dbo.Products", "CpuId");
            DropTable("dbo.Brands");
            DropTable("dbo.Cpus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cpus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Alias = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Alias = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "CpuId", c => c.Int());
            AddColumn("dbo.Products", "BrandId", c => c.Int());
            CreateIndex("dbo.Products", "CpuId");
            CreateIndex("dbo.Products", "BrandId");
            AddForeignKey("dbo.Products", "CpuId", "dbo.Cpus", "Id");
            AddForeignKey("dbo.Products", "BrandId", "dbo.Brands", "Id");
        }
    }
}
