namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddstoreGetKHAll : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetTongSoNoKhachHangAll", @"
                select kh.id , kh.Name,sum(ct.ChiPhiChuaBenh-ct.TraTruoc) as TongSoNo from KhachHangs kh 
                inner join CTKhachHangChuaBenhs ct
                on kh.Id=ct.IdKhachHang
                group by ct.IdKhachHang,kh.Name,kh.Id
            ");
        }
        
        public override void Down()
        {
        }
    }
}
