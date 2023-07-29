using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Global.Errors
{
    public class UserNotFoundErrorResult:ErrorResult
    {
        public UserNotFoundErrorResult() : base()
        {
            Title = "User Not Found!";
            Description = "User not found or deleted";
            HttpStatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
