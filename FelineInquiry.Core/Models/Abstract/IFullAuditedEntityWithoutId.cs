using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Abstract
{
    public interface IFullAuditedEntityWithoutId
    {
        Guid CreatedBy { get; set; }
        Guid? ModifiedBy { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        bool IsDeleted { get; set; }
        Guid? DeletedBy { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
