using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Common.CommonModel
{
    public class CTKhachHangModelCommon
    {
        public int Id { set; get; }
        public int IdKhachHang { set; get; }
        public decimal ChiPhiChuaBenh { set; get; }
        public string Address { get; set; }
        public string SuDungThuoc { get; set; }
        public DateTime NgayChuaBenh { set; get; }
        public decimal TraTruoc { set; get; }
        public bool TrangThaiNo { set; get; }
        public string Name { set; get; }
        public decimal CTNoLai { set; get; }

    }
}
