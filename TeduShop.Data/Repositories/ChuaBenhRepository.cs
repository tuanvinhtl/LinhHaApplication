using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IChuaBenhRepository:IRepository<ChuaBenh>
    {

    }
    public class ChuaBenhRepository:RepositoryBase<ChuaBenh>,IChuaBenhRepository
    {
        public ChuaBenhRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
    }
}
