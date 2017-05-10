namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixLayers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Products", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Categories", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Categories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Categories", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Categories", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.Categories", "MetaDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.Categories", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Status", c => c.Boolean());
            DropColumn("dbo.Categories", "MetaDescription");
            DropColumn("dbo.Categories", "MetaKeyword");
            DropColumn("dbo.Categories", "UpdatedBy");
            DropColumn("dbo.Categories", "UpdatedDate");
            DropColumn("dbo.Categories", "CreatedBy");
            DropColumn("dbo.Products", "MetaDescription");
            DropColumn("dbo.Products", "MetaKeyword");
            DropColumn("dbo.Products", "UpdatedBy");
            DropColumn("dbo.Products", "UpdatedDate");
        }
    }
}
