namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategories", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategories", "Status", c => c.Boolean());
        }
    }
}
