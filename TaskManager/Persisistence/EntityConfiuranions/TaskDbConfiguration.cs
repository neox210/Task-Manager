#region

using System.Data.Entity.ModelConfiguration;
using TaskManager.Models;

#endregion

namespace TaskManager.Persisistence.EntityConfiuranions
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
                .WithMany(t => t.Tasks)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Priority)
                .WithMany(t => t.Tasks)
                .WillCascadeOnDelete(false);
        }
    }
}
