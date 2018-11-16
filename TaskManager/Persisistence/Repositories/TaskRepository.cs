#region

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TaskManager.Models;
using TaskManager.Repository;

#endregion

namespace TaskManager.Persisistence.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(TaskDbContext context) : base(context)
        {
            
        }

        public IEnumerable<Task> GetFiltered(string filter)
        {
            var parameter = new SqlParameter("@Filter", filter);
            return TaskDbContext.Tasks.SqlQuery("dbo.GetFilteredTasks @Filter", parameter).ToList();
        }

        public IEnumerable<Priority> GetAvailablePriorities()
        {
            return TaskDbContext.Priorities.ToList();
        }

        public IEnumerable<Status> GetAvailableStatuses()
        {
            return TaskDbContext.Statuses.ToList();
        }

        public TaskDbContext TaskDbContext => Context as TaskDbContext;
    }
}
