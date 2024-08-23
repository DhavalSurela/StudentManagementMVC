using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;

namespace RepositoryPattern.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DbContextClass dbContext) : base(dbContext) { }

        public async Task<IQueryable<Student>> GetAllManual()
        {
           var students = from s in base._dbContext.Students
                           select s;
            return students;
        }

    }
}
