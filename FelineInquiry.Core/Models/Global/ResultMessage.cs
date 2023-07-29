using FelineInquiry.Core.Models.Global.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Global
{
    public class ResultMessage
    {
        public bool Success { get; set; }
        public ErrorResult Error { get; set; }
    }
}
