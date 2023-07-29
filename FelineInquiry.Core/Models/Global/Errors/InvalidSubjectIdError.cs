using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Global.Errors
{
    public class InvalidSubjectIdError:ErrorResult
    {
        public InvalidSubjectIdError() : base()
        {
            Title = "Invalid Subject!";
            Description = "Invalid User Token, Please Check and try again";
            HttpStatusCode = System.Net.HttpStatusCode.Unauthorized;
        }
    }
}
