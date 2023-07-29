using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Global.Errors
{
    public class NotFoundErrorResult:ErrorResult
    {
        public NotFoundErrorResult():base()
        {
            Title = "Entity Not Found!";
            Description = "Enitity not found or deleted";
            HttpStatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
