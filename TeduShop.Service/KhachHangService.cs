using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IKhachHangService
    {
        KhachHang Add(KhachHang khachhang);
        void Edit(KhachHang khachhang);
        void Delete(int id);
        IEnumerable<KhachHang> GetAll();
        IEnumerable<KhachHang> GetListPaging(int page, int pageSize, out int totalRow, string filter);
        void SaveChange();
        KhachHang GetDetail(int id);

    }
    public class KhachHangService : IKhachHangService
    {
        IKhachHangRepository _khachHangRepository;
        IUnitOfWork _unitOfWork;
        public KhachHangService(IKhachHangRepository khachHangRepository, IUnitOfWork unitOfWork)
        {
            _khachHangRepository = khachHangRepository;
            _unitOfWork = unitOfWork;
        }

        public KhachHang Add(KhachHang khachhang)
        {
            return _khachHangRepository.Add(khachhang);
        }

        public void Delete(int id)
        {
            _khachHangRepository.Delete(id);
        }

        public void Edit(KhachHang khachhang)
        {
            _khachHangRepository.Update(khachhang);

        }

        public IEnumerable<KhachHang> GetAll()
        {
            return _khachHangRepository.GetAll();

        }

        public KhachHang GetDetail(int id)
        {
            return _khachHangRepository.GetSingleById(id);
        }

        public IEnumerable<KhachHang> GetListPaging(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _khachHangRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter) || x.PhoneNumber.Contains(filter));

            }
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
       
    }
}
