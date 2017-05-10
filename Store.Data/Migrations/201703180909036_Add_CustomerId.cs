namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CustomerId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "CustomerId");
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "CustomerId");
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_CustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_CustomerId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Orders", name: "CustomerId", newName: "User_Id");
            AddColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
        }
    }
}
