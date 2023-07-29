using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Abstract
{
    public interface IEntityWithId
    {
        Guid Id { get; set; }
    }
}
