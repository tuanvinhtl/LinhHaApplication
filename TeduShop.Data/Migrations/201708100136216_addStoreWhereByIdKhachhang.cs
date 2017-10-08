namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addStoreWhereByIdKhachhang : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetCTKhachHangChuaBenhByIdKhachHang",p=> new{
                 khachhangId= p.Int()
            },
                @"
                select kh.Name, cb.Ten,ct.NgayChuaBenh,ct.Id,ct.ChiPhiChuaBenh ,ct.IdChuaBenh,ct.IdKhachHang,ct.TrangThaiNo,ct.TraTruoc,ct.ChiPhiChuaBenh-TraTruoc as CTNoLai
                from KhachHangs kh 
                inner join CTKhachHangChuaBenhs ct
                on kh.Id=ct.IdKhachHang
                inner join ChuaBenhs cb
                on cb.Id=ct.IdChuaBenh
                where ct.IdKhachHang=@khachhangId;
                ");
        }

        public override void Down()
        {
        }
    }
}
