namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChuaBenhs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        MoTa = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        ChuThich = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CTKhachHangChuaBenhs",
                c => new
                    {
                        IdKhachHang = c.Int(nullable: false),
                        IdChuaBenh = c.Int(nullable: false),
                        ChiPhiChuaBenh = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NgayChuaBenh = c.DateTime(nullable: false),
                        ConNolai = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrangThaiNo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdKhachHang, t.IdChuaBenh })
                .ForeignKey("dbo.ChuaBenhs", t => t.IdChuaBenh, cascadeDelete: true)
                .ForeignKey("dbo.KhachHangs", t => t.IdKhachHang, cascadeDelete: true)
                .Index(t => t.IdKhachHang)
                .Index(t => t.IdChuaBenh);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CTKhachHangChuaBenhs", "IdKhachHang", "dbo.KhachHangs");
            DropForeignKey("dbo.CTKhachHangChuaBenhs", "IdChuaBenh", "dbo.ChuaBenhs");
            DropIndex("dbo.CTKhachHangChuaBenhs", new[] { "IdChuaBenh" });
            DropIndex("dbo.CTKhachHangChuaBenhs", new[] { "IdKhachHang" });
            DropTable("dbo.KhachHangs");
            DropTable("dbo.CTKhachHangChuaBenhs");
            DropTable("dbo.ChuaBenhs");
        }
    }
}
