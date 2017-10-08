namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteStore : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            DropStoredProcedure("GetChiTietKhachHangChuaBenhByDate");
            DropStoredProcedure("GetCTKhachHangChuaBenh");
            DropStoredProcedure("GetChiTietKhachHangChuaBenhByDate2");
            DropStoredProcedure("GetCTKhachHangChuaBenhByIdKhachHang");
            
        }
    }
}
