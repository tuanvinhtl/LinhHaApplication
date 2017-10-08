namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteStore1 : DbMigration
    {
        public override void Up()
        {
            DropStoredProcedure("GetCTKhachHangChuaBenhKhachHang");
        }
        
        public override void Down()
        {
        }
    }
}
