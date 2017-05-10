namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Field_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Alias", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Alias");
        }
    }
}
