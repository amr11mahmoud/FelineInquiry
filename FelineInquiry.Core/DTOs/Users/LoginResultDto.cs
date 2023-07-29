using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.DTOs.Users
{
    public class LoginResultDto
    {
    }

    public class UserToken
    {
        public JWTToken? AccessToken { get; set; } 
        public JWTToken? RefreshToken { get; set; }
        public bool IsEmailConfirmed { get; set; }
        
    }

    public class JWTToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
