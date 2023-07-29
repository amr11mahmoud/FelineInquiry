﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.DTOs.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
