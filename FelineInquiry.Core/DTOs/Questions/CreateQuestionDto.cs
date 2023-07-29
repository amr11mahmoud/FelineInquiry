using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.DTOs.Questions
{
    public class CreateQuestionDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public bool IsAnonymous { get; set; }
        [Required]
        public Guid RecipientId { get; set; }
    }
}
