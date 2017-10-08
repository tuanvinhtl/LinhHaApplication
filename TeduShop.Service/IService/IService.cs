using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Service.IService
{
    public interface IService<T>
    {
        T Create(T t);
        void Update(T t);
        void Delete(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllPaging(int page, int pageSize, out int totalRow, string filter);
        void SaveChange();
        T GetDetail(int id);
    }
}
