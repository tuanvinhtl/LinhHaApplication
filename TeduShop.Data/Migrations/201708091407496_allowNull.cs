namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CTKhachHangChuaBenhs", "NgayChuaBenh", c => c.DateTime());
            AlterColumn("dbo.CTKhachHangChuaBenhs", "TraTruoc", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CTKhachHangChuaBenhs", "ConNolai", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CTKhachHangChuaBenhs", "TrangThaiNo", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CTKhachHangChuaBenhs", "TrangThaiNo", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CTKhachHangChuaBenhs", "ConNolai", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CTKhachHangChuaBenhs", "TraTruoc", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CTKhachHangChuaBenhs", "NgayChuaBenh", c => c.DateTime(nullable: false));
        }
    }
}
