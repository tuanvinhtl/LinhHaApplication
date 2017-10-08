namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCTkhachhang : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetCTKhachHangChuaBenhKhachHang", @"select kh.Name,kh.Address,ct.SuDungThuoc,ct.NgayChuaBenh,ct.Id,ct.ChiPhiChuaBenh ,ct.IdKhachHang,ct.TraTruoc,ct.ChiPhiChuaBenh-TraTruoc as CTNoLai
                                from KhachHangs kh 
                                inner join CTKhachHangChuaBenhs ct
                                on kh.Id=ct.IdKhachHang"
        );
        }
        
        public override void Down()
        {
        }
    }
}
