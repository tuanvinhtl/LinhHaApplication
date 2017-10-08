using System;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupVM)
        {
            appGroup.ID = appGroupVM.ID;
            appGroup.Name = appGroupVM.Name;
            appGroup.Description = appGroupVM.Description;
        }
        public static void UpdateApplicationUser(this ApplicationUser appUser, ApplicationUserViewModel appUserVM)
        {
            appUser.Id = appUserVM.Id;
            appUser.FullName = appUserVM.FullName;
            appUser.BirthDay = appUserVM.BirthDay;
            appUser.Email = appUserVM.Email;
            appUser.UserName = appUserVM.UserName;
            appUser.PhoneNumber = appUserVM.PhoneNumber;
            appUser.CreatedDate = DateTime.Now;
        }
        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleVM, string Action)
        {
            if (Action == "create")
            {
                appRole.Id = Guid.NewGuid().ToString();
                appRole.Name = appRoleVM.Name;
                appRole.Description = appRoleVM.Description;
            }
            if (Action == "update")
            {
                appRole.Id = appRoleVM.Id;
                appRole.Name = appRoleVM.Name;
                appRole.Description = appRoleVM.Description;
            }
        }
        public static void UpdateKhachHang(this KhachHang khachhang, KhachHangViewModel khachhangVM)
        {
            khachhang.Id = khachhangVM.Id;
            khachhang.Name = khachhangVM.Name;
            khachhang.PhoneNumber = khachhangVM.PhoneNumber;
            khachhang.Address = khachhangVM.Address;
            khachhang.CreatedDate = DateTime.Now;
        }
        public static void UpdateChuaBenh(this ChuaBenh chuabenh, ChuaBenhViewModel chuabenhVM)
        {
            chuabenh.Id = chuabenhVM.Id;
            chuabenh.Ten = chuabenhVM.Ten;
            chuabenh.MoTa = chuabenhVM.MoTa;
            chuabenh.ChuThich = chuabenhVM.ChuThich;
            chuabenh.NgayTao = DateTime.Now;
        }
        public static void UpdateCTKhachHangChuaBenh(this CTKhachHangChuaBenh ctKhachHangChuaBenh, CTKhachHangChuaBenhViewModel ctKhachHangChuaBenhVM)
        {
            ctKhachHangChuaBenh.IdKhachHang = ctKhachHangChuaBenhVM.IdKhachHang;
            ctKhachHangChuaBenh.SuDungThuoc = ctKhachHangChuaBenh.SuDungThuoc;

            if (ctKhachHangChuaBenhVM.NgayChuaBenh==DateTime.MinValue || ctKhachHangChuaBenhVM.NgayChuaBenh==null)
            {
                ctKhachHangChuaBenh.NgayChuaBenh = DateTime.Now;
            }
            else
            {
                ctKhachHangChuaBenh.NgayChuaBenh = ctKhachHangChuaBenhVM.NgayChuaBenh;
            }
            ctKhachHangChuaBenh.ChiPhiChuaBenh = ctKhachHangChuaBenhVM.ChiPhiChuaBenh;
            ctKhachHangChuaBenh.Id = ctKhachHangChuaBenhVM.Id;
            ctKhachHangChuaBenh.TraTruoc = ctKhachHangChuaBenhVM.TraTruoc;
            ctKhachHangChuaBenh.SuDungThuoc = ctKhachHangChuaBenhVM.SuDungThuoc;
        }

    }

}