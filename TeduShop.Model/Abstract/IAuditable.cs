using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Abstract
{
    public interface IAuditable
    {
        DateTime? CreatedDate { set; get; }
        [StringLength(256)]
        string CreatedBy { set; get; }

        DateTime? UpdateDate { set; get; }

        [StringLength(256)]
        string UpdateBy { set; get; }

        string Description { set; get; }

        int? DisplayOrder { set; get; }

        [Required]
        bool Status { set; get; }
    }
}
