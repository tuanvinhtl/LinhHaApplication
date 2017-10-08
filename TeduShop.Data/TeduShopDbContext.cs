using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Models;

namespace TeduShop.Data
{
    public class TeduShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public TeduShopDbContext() : base("LinhSaHuynhConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<KhachHang> KhachHangs { set; get; }
        public DbSet<ChuaBenh> ChuaBenhs { set; get; }
        public DbSet<CTKhachHangChuaBenh> CTKhachHangChuaBenhs { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public DbSet<Error> Errors { set; get; }
        public static TeduShopDbContext Create()
        {
            return new TeduShopDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicatonUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaims");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        }

    }
}
