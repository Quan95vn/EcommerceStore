namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_More_Image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MoreImage3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "MoreImage3");
        }
    }
}
