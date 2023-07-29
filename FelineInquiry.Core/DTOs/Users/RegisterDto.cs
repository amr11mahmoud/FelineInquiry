using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.DTOs.Users
{
    //TODO Consider using FluentValidation instead of data annotaions for input validations
    public class RegisterDto
    {
        [Required(ErrorMessage ="Email can't be empty!")]
        [EmailAddress(ErrorMessage ="Email should be in a proper email address format!")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(32)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "FirstName can't be empty!")]
        [MaxLength(32)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName can't be empty!")]
        [MaxLength(32)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be empty!")]
        [DataType(DataType.Password)]
        [MaxLength(16)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password can't be empty!")]
        [DataType(DataType.Password)]
        [MaxLength(16)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserName can't be empty!")]
        [MaxLength(32)]
        public string UserName { get; set; } = string.Empty;

        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone should be in a proper phone number format!")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(16)]
        public string? PhoneNumber { get; set; }
    }
} 
