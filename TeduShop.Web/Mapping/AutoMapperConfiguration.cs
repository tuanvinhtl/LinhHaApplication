using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configuration()
        {

            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            Mapper.CreateMap<KhachHang, KhachHangViewModel>();
            Mapper.CreateMap<ChuaBenh, ChuaBenhViewModel>();
            Mapper.CreateMap<CTKhachHangChuaBenh, CTKhachHangChuaBenhViewModel>();
        }
    }
}