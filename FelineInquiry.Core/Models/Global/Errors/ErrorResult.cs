using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Global.Errors
{
    public class ErrorResult
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual HttpStatusCode HttpStatusCode { get; set; }
    }
}
