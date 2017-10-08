using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ApplicationGroupViewModel
    {
        public int ID { set; get; }
        [StringLength(250)]
        public string Name { set; get; }
        [StringLength(250)]
        public string Description { set; get; }
        public IEnumerable<ApplicationRoleViewModel> Roles { set; get; }
    }
}