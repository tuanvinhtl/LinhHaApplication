using System.Collections.Generic;
using TeduShop.Common.CommonModel;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Linq;
using System;
using System.Data.SqlClient;

namespace TeduShop.Data.Repositories
{
    public interface ICTKhachHangChuaBenhRepository : IRepository<CTKhachHangChuaBenh>
    {
        IEnumerable<CTKhachHangModelCommon> GetAllChiTietKhachHang();
    }
    public class CTKhachHangChuaBenhRepository : RepositoryBase<CTKhachHangChuaBenh>, ICTKhachHangChuaBenhRepository
    {
        public CTKhachHangChuaBenhRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<CTKhachHangModelCommon> GetAllChiTietKhachHang()
        {
            return DbContext.Database.SqlQuery<CTKhachHangModelCommon>("GetCTKhachHangChuaBenhKhachHang");
        }
    }
}
