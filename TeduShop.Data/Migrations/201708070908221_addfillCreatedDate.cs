namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfillCreatedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHangs", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KhachHangs", "CreatedDate");
        }
    }
}
