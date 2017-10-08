namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTrangThaiNo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CTKhachHangChuaBenhs", "TrangThaiNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CTKhachHangChuaBenhs", "TrangThaiNo", c => c.Boolean());
        }
    }
}
