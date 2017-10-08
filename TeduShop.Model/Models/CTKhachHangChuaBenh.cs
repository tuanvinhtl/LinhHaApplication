using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("CTKhachHangChuaBenhs")]
    public class CTKhachHangChuaBenh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public int IdKhachHang { set; get; }
        public string SuDungThuoc { get; set; }
        public decimal ChiPhiChuaBenh { set; get; }
        public DateTime? NgayChuaBenh { set; get; }
        public decimal? TraTruoc { set; get; }

        [ForeignKey("IdKhachHang")]
        public virtual KhachHang KhachHang { set; get; }
    }
}
