using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using TaskManager.Models;


namespace TaskRepository.Custom
{
    public class TaskDbConfiguration : EntityTypeConfiguration<Task>
    {
        public TaskDbConfiguration()
        {
            Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);

            Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.EndDate)
                .IsOptional();

            HasRequired(t => t.Status)
                .WithRequiredPrincipal(s => s.Task);

            HasRequired(t => t.Priority)
                .WithRequiredPrincipal(p => p.Task);
        }
    }
}
