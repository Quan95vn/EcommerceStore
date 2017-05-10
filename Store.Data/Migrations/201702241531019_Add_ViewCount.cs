namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ViewCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ViewCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ViewCount");
        }
    }
}
