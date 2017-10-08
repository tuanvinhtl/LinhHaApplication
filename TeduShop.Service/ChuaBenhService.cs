using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service.IService;

namespace TeduShop.Service
{
    public interface IChuaBenhService : IService<ChuaBenh>
    {

    }
    public class ChuaBenhService : IChuaBenhService
    {
        IChuaBenhRepository _chuabenhRepository;
        IUnitOfWork _unitOfWork;
        public ChuaBenhService(IChuaBenhRepository chuabenhRepository, IUnitOfWork unitOfWork)
        {
            this._chuabenhRepository = chuabenhRepository;
            this._unitOfWork = unitOfWork;
        }
        public ChuaBenh Create(ChuaBenh t)
        {
            return _chuabenhRepository.Add(t);
        }

        public void Delete(int id)
        {
            _chuabenhRepository.Delete(id);
        }

        public IEnumerable<ChuaBenh> GetAll()
        {
            return _chuabenhRepository.GetAll();
        }

        public IEnumerable<ChuaBenh> GetAllPaging(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _chuabenhRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Ten.Contains(filter));
            }
            totalRow = query.Count();
            return query.OrderBy(x => x.NgayTao).Skip(page * pageSize).Take(pageSize);
        }

        public ChuaBenh GetDetail(int id)
        {
            return _chuabenhRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(ChuaBenh t)
        {
            _chuabenhRepository.Update(t);
        }


    }
}
