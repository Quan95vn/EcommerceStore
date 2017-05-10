namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Field_4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Status", c => c.Boolean());
            DropColumn("dbo.Categories", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Image", c => c.String(maxLength: 256));
            AlterColumn("dbo.Categories", "Status", c => c.Boolean(nullable: false));
        }
    }
}
