namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMenuGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "MenuGroupId", "dbo.MenuGroups");
            DropIndex("dbo.Menus", new[] { "MenuGroupId" });
            DropColumn("dbo.Menus", "MenuGroupId");
            DropTable("dbo.MenuGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MenuGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Menus", "MenuGroupId", c => c.Int());
            CreateIndex("dbo.Menus", "MenuGroupId");
            AddForeignKey("dbo.Menus", "MenuGroupId", "dbo.MenuGroups", "Id");
        }
    }
}
