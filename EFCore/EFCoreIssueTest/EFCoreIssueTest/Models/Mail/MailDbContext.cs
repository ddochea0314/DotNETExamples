using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIssueTest.Models.Mail
{
    public class MailDbContext : DbContext
    {
        public MailDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ReceiveUser> ReceiveUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReceiveUser>(entity =>
            {
                entity.HasKey(e => new { e.TemplateID, e.UserID });
            });
        }
    }
}
