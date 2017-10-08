namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editChuaBenh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CTKhachHangChuaBenhs", "IdChuaBenh", "dbo.ChuaBenhs");
            DropIndex("dbo.CTKhachHangChuaBenhs", new[] { "IdChuaBenh" });
            AddColumn("dbo.CTKhachHangChuaBenhs", "SuDungThuoc", c => c.String());
            DropColumn("dbo.CTKhachHangChuaBenhs", "IdChuaBenh");
            DropColumn("dbo.CTKhachHangChuaBenhs", "ConNolai");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CTKhachHangChuaBenhs", "ConNolai", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CTKhachHangChuaBenhs", "IdChuaBenh", c => c.Int(nullable: false));
            DropColumn("dbo.CTKhachHangChuaBenhs", "SuDungThuoc");
            CreateIndex("dbo.CTKhachHangChuaBenhs", "IdChuaBenh");
            AddForeignKey("dbo.CTKhachHangChuaBenhs", "IdChuaBenh", "dbo.ChuaBenhs", "Id", cascadeDelete: true);
        }
    }
}
