using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class CTKhachHangChuaBenhViewModel
    {
        public int Id { set; get; }
        public int IdKhachHang { set; get; }
        public string SuDungThuoc { get; set; }
        public decimal ChiPhiChuaBenh { set; get; }
        public DateTime? NgayChuaBenh { set; get; }
        public decimal TraTruoc { set; get; }

        public virtual KhachHangViewModel KhachHang { set; get; }


    }
}