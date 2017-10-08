namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStoreFindByDate2 : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetChiTietKhachHangChuaBenhByDate2", p => new {
                fromDate = p.String(),
                toDate = p.String()
            },
                @"select kh.Name, cb.Ten,ct.NgayChuaBenh as Date,ct.Id,ct.ChiPhiChuaBenh ,ct.IdChuaBenh,ct.IdKhachHang,ct.TrangThaiNo,ct.TraTruoc,ct.ChiPhiChuaBenh-TraTruoc as CTNoLai
                    from KhachHangs kh 
                    inner join CTKhachHangChuaBenhs ct
                    on kh.Id=ct.IdKhachHang
                    inner join ChuaBenhs cb
                    on cb.Id=ct.IdChuaBenh
	                where ct.NgayChuaBenh >= cast (@fromDate as date ) AND  ct.NgayChuaBenh  <= cast( @toDate as date)
                   ");
        }
        
        public override void Down()
        {
            DropStoredProcedure("GetChiTietKhachHangChuaBenhByDate2");
        }
    }
}
