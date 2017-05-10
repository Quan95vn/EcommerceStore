namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Field_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsFeature", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsFeature");
        }
    }
}
