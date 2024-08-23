using RepositoryPattern.Core.Models;

namespace RepositoryPattern.Services.Interfaces
{
    public interface IStudentService

    {
        Task<bool> CreateStudent(Student student);

        Task<IEnumerable<Student>> GetAllStudents( string searchString);
        Task<IEnumerable<Student>> GetAllStudentsManual(string sortOrder, string currentFilter,
    string searchString, int? pageNumber);

        Task<Student> GetStudentById(int studentId);

        Task<bool> UpdateStudent(Student student);

        Task<bool> DeleteStudent(int studentId);
    }
}