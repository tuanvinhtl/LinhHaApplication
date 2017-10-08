using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class KhachHangViewModel
    {
        public int Id { set; get; }

        public string Name { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}