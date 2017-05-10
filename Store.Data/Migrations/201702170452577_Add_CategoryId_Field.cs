namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CategoryId_Field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CategoryId");
        }
    }
}
