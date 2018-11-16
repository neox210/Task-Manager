using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskRepository.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        int Complete();
    }
}
