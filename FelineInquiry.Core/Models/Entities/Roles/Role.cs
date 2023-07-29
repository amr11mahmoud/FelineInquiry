using FelineInquiry.Core.Models.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Entities.Roles
{
    public class Role : IdentityRole<Guid>/*, IFullAuditedEntityWithoutId*/
    {
        //public Guid CreatedBy { get; set; }

        //public Guid? ModifiedBy { get; set; }

        //public bool IsDeleted { get; set; }

        //public Guid? DeletedBy { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime? ModifiedAt { get; set; }

        //public DateTime? DeletedAt { get; set; }
    }
}
