using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ChuaBenhViewModel
    {

        public int Id { set; get; }
        public string Ten { set; get; }
        public string MoTa { set; get; }
        public DateTime NgayTao { set; get; }
        public string ChuThich { set; get; }
    }
}