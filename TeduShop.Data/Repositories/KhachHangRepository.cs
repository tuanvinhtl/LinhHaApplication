using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IKhachHangRepository:IRepository<KhachHang>
    {

    }
    public class KhachHangRepository:RepositoryBase<KhachHang>,IKhachHangRepository
    {
        public KhachHangRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
