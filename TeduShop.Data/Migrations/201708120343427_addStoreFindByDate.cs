namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStoreFindByDate : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetChiTietKhachHangChuaBenhByDate", p => new {
                fromDate = p.DateTime(),
                toDate = p.DateTime()
            },
                @" select kh.Name, cb.Ten,ct.NgayChuaBenh,ct.Id,ct.ChiPhiChuaBenh ,ct.IdChuaBenh,ct.IdKhachHang,ct.TrangThaiNo,ct.TraTruoc,ct.ChiPhiChuaBenh-TraTruoc as CTNoLai
                from KhachHangs kh 
                inner join CTKhachHangChuaBenhs ct
                on kh.Id=ct.IdKhachHang
                inner join ChuaBenhs cb
                on cb.Id=ct.IdChuaBenh
	            where ct.NgayChuaBenh >= @fromDate   AND ct.NgayChuaBenh <= @toDate"
            );
        }
        
        public override void Down()
        {
            DropStoredProcedure("GetChiTietKhachHangChuaBenhByDate");
        }
    }
}
