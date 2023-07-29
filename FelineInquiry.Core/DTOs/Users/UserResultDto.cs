using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.DTOs.Users
{
    public class UserResultDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string UserName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
