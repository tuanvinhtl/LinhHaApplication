using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common.CommonModel;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IChiTietKhachHangService
    {
        IEnumerable<CTKhachHangModelCommon> GetAll();
        IEnumerable<CTKhachHangModelCommon> GetAllPaginAndFindByDate(int page, int pageSize, out int totalRow, string fillterName, string filterAddress, string fromDate, string toDate);
        CTKhachHangChuaBenh Add(CTKhachHangChuaBenh ctkhachHang);
        void Delete(int id);
        void Update(CTKhachHangChuaBenh ctkhachHang);
        CTKhachHangChuaBenh GetById(int id);
        IEnumerable<CTKhachHangModelCommon> GetAllTotalAmount(string fillterName, string filterAddress, string fromDate, string toDate);
        IEnumerable<CTKhachHangModelCommon> GetMultilById(int id);
       
    }
    public class ChiTietKhachHangService : IChiTietKhachHangService
    {
        ICTKhachHangChuaBenhRepository _chitietKhachHang;
        public ChiTietKhachHangService(ICTKhachHangChuaBenhRepository chitietKhachHang)
        {
            this._chitietKhachHang = chitietKhachHang;
        }

        public CTKhachHangChuaBenh Add(CTKhachHangChuaBenh ctkhachHang)
        {
            return _chitietKhachHang.Add(ctkhachHang);
        }

        public void Delete(int id)
        {
            _chitietKhachHang.Delete(id);
        }

        public IEnumerable<CTKhachHangModelCommon> GetAll()
        {
            return _chitietKhachHang.GetAllChiTietKhachHang();
        }
        public IEnumerable<CTKhachHangModelCommon> GetAllPaginAndFindByDate(int page, int pageSize, out int totalRow, string fillterName, string filterAddress, string fromDate, string toDate)
        {
            var query = _chitietKhachHang.GetAllChiTietKhachHang();
            if (!string.IsNullOrEmpty(fillterName))
            {
                query = query.Where(x => x.Name.Contains(fillterName));
            }
            if (!string.IsNullOrEmpty(filterAddress))
            {
                query = query.Where(x => x.Address.Contains(filterAddress));
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(fromDate))
            {
                query = query.Where(x => x.NgayChuaBenh >= DateTime.Parse(fromDate) && x.NgayChuaBenh <= DateTime.Parse(toDate));
            }
            totalRow = query.Count();
            return query.OrderByDescending(x => x.NgayChuaBenh).Skip(page * pageSize).Take(pageSize);

        }
        public IEnumerable<CTKhachHangModelCommon> GetMultilById(int id)
        {
            var query = _chitietKhachHang.GetAllChiTietKhachHang();
            query = query.Where(x => x.IdKhachHang == id);
            return query;

        }

        public CTKhachHangChuaBenh GetById(int id)
        {
            return _chitietKhachHang.GetSingleById(id);
        }

        public void Update(CTKhachHangChuaBenh ctkhachHang)
        {
            _chitietKhachHang.Update(ctkhachHang);
        }

        public IEnumerable<CTKhachHangModelCommon> GetAllTotalAmount(string fillterName, string filterAddress, string fromDate, string toDate)
        {
            var query = _chitietKhachHang.GetAllChiTietKhachHang();
            if (!string.IsNullOrEmpty(fillterName))
            {
                query = query.Where(x => x.Name.Contains(fillterName));
            }
            if (!string.IsNullOrEmpty(filterAddress))
            {
                query = query.Where(x => x.Address.Contains(filterAddress));
            }
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(fromDate))
            {
                query = query.Where(x => x.NgayChuaBenh > DateTime.Parse(fromDate) && x.NgayChuaBenh <= DateTime.Parse(toDate));
            }
            return query;

        }
    }
}
