using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskRepository.Custom
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
        }
    }
}
