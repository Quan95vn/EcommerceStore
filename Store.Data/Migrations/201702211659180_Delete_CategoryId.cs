namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_CategoryId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "Description");
            DropColumn("dbo.Categories", "CategoryId");
            DropColumn("dbo.Categories", "CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Categories", "CategoryId", c => c.Int());
            AddColumn("dbo.Categories", "Description", c => c.String(maxLength: 500));
        }
    }
}
