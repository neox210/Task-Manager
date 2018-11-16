#region

using System;

#endregion

namespace TaskManager.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        int Complete();
    }
}
