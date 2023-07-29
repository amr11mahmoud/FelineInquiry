using FelineInquiry.Core.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.DTOs.Questions
{
    public class QuestionResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublic { get; set; }
        public bool IsAnonymous { get; set; }
        public User Author { get; set; }
        public User Recipient { get; set; }
    }
}
