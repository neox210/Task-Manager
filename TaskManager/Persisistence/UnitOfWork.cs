#region

using TaskManager.Persisistence.Repositories;
using TaskManager.Repository;

#endregion

namespace TaskManager.Persisistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _context;

        public UnitOfWork(TaskDbContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
        }
        
        public ITaskRepository Tasks { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
