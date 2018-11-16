#region

using System.Collections.Generic;
using TaskManager.Models;

#endregion

namespace TaskManager.Repository
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Priority> GetAvailablePriorities();
        IEnumerable<Status> GetAvailableStatuses();
        IEnumerable<Task> GetFiltered(string filter);
    }
}
