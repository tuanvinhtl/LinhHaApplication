using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("ChuaBenhs")]
    public class ChuaBenh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Ten { set; get; }
        public string MoTa { set; get; }
        public DateTime NgayTao { set; get; }
        public string ChuThich { set; get; }
    }
}
