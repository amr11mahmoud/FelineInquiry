using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Core.Models.Entities.Roles;
using FelineInquiry.Core.Models.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FelineInquiry.Data.DBContext
{
    public class FelineInquiryDbContext: IdentityDbContext<User, Role, Guid>
    {
        public FelineInquiryDbContext(DbContextOptions<FelineInquiryDbContext> options):base(options) { }

        public DbSet<Question> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between Parent and Child using Fluent API
            modelBuilder.Entity<Question>()
                .HasOne(c => c.Author)           // Child entity has one Parent
                .WithMany(p => p.AuthoredQuestions)       // Parent entity has many Children
                .HasForeignKey(c => c.AuthorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);  // Foreign key in Child table to reference ParentId

            modelBuilder.Entity<Question>()
               .HasOne(c => c.Recipient)           // Child entity has one Parent
               .WithMany(p => p.ReceivedQuestions)       // Parent entity has many Children
               .HasForeignKey(c => c.RecipientId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction);  // Foreign key in Child table to reference ParentId
        }

    }
}
