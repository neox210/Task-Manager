#region

using System.Data.Entity;
using TaskManager.Models;
using TaskManager.Persisistence.EntityConfiuranions;

#endregion

namespace TaskManager.Persisistence
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("name=TaskDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TaskDbConfiguration());
            modelBuilder.Entity<Status>().ToTable("dbo.Statuses");
        }
    }
}
