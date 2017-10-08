namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CTKhachHangChuaBenhs");
            AddColumn("dbo.CTKhachHangChuaBenhs", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CTKhachHangChuaBenhs", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CTKhachHangChuaBenhs");
            DropColumn("dbo.CTKhachHangChuaBenhs", "Id");
            AddPrimaryKey("dbo.CTKhachHangChuaBenhs", new[] { "IdKhachHang", "IdChuaBenh" });
        }
    }
}
