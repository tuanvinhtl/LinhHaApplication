namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddmoreStore : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetTongSoNoKhachHang", @"
                select kh.id , kh.Name,sum(ct.ChiPhiChuaBenh-ct.TraTruoc) as TongSoNo from KhachHangs kh 
                inner join CTKhachHangChuaBenhs ct
                on kh.Id=ct.IdKhachHang
                group by ct.IdKhachHang,kh.Name,kh.Id
                HAVING sum(ct.ChiPhiChuaBenh-ct.TraTruoc)  > 0 
            ");
        }
        
        public override void Down()
        {
        }
    }
}
