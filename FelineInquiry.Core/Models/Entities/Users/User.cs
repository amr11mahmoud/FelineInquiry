using FelineInquiry.Core.Models.Abstract;
using FelineInquiry.Core.Models.Entities.Questions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Entities.Users
{
    public class User:IdentityUser<Guid>/*, IFullAuditedEntityWithoutId*/
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string? EmailConfirmationCode { get; set; }

        public string? PhoneConfirmationCode { get; set; }

        public ICollection<Question> ReceivedQuestions { get; set; }
        public ICollection<Question> AuthoredQuestions { get; set; }

        public User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public User(string firstName, string lastName, string email, Guid id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Id = id;
        }

        public User(string firstName, string lastName, string email, Guid id, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Id = id;
            IsActive = isActive;
        }

        //public Guid CreatedBy { get; set; }

        //public Guid? ModifiedBy { get; set; }

        //public bool IsDeleted { get; set; }

        //public Guid? DeletedBy { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime? ModifiedAt { get; set; }

        //public DateTime? DeletedAt { get; set; }

        public override string ToString()
        {

            return $"FirstName:{FirstName},LastName:{LastName},Email:{Email}";
        }

    }


}
