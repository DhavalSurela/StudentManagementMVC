using RepositoryPattern.Core.Interfaces;

namespace RepositoryPattern.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        public IStudentRepository Students { get; }
        public ILoginRepositorry Login { get; }
        public UnitOfWork(DbContextClass dbContext, IStudentRepository studentRepository, ILoginRepositorry login)
        {
            _dbContext = dbContext;
            Students = studentRepository;
            Login = login;
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
