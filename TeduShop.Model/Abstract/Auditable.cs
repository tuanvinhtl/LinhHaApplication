using System;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Model.Abstract
{
    public class Auditable: IAuditable
    {
        public DateTime? CreatedDate { set; get; }

        [StringLength(256)]
        public string CreatedBy { set; get; }

        public DateTime? UpdateDate { set; get; }

        [StringLength(256)]
        public string UpdateBy { set; get; }

        public string Description { set; get; }

        public int? DisplayOrder { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}
