using FelineInquiry.Core.Models.Abstract;
using FelineInquiry.Core.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Models.Entities.Questions
{
    public class Question:/*IFullAuditedEntityWithoutId,*/ IEntityWithId
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;


        public bool IsPublic { get; set; } = true;

        public bool IsAnonymous { get; set; } = false;

        public User Author { get; set; }

        public User Recipient { get; set; }

        public Guid? AuthorId { get; set; }
        public Guid? RecipientId { get; set; }

        //public Guid CreatedBy { get; set; }

        //public Guid? ModifiedBy { get; set; }

        //public bool IsDeleted { get; set; }

        //public Guid? DeletedBy { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime? ModifiedAt { get; set; }

        //public DateTime? DeletedAt { get; set; }
    }
}
