using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskRepository.Generic;
using TaskRepository.Interfaces;

namespace TaskRepository.Custom
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(TaskDbContext context) : base(context)
        {
            
        }
    }
}
