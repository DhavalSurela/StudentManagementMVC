using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;
using RepositoryPattern.Services.Interfaces;
using RepositoryPattern.Services.Utilities;
namespace RepositoryPattern.Services
{
    public class StudentService : IStudentService
    {
        public IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateStudent(Student student)
        {
            if (student != null)
            {
                await _unitOfWork.Students.Add(student);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteStudent(int StudentId)
        {
            if (StudentId > 0)
            {
                var student = await _unitOfWork.Students.GetById(StudentId);
                if (student != null)
                {
                    _unitOfWork.Students.Delete(student);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Student>> GetAllStudents(string searchString)
        {
            var student = await _unitOfWork.Students.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                student = student.Where(s => s.Name.ToString().ToLower().Contains(searchString.ToLower()));
            }
            return student;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsManual(string sortOrder, string currentFilter,
         string searchString, int? pageNumber)

        {
            var students = await _unitOfWork.Students.GetAllManual();
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "age":
                    students = students.OrderBy(s => s.Age);
                    break;
                case "age_desc":
                    students = students.OrderByDescending(s => s.Age);
                    break;
                default:
                    students = students.OrderBy(s => s.Id);
                    break;
            }
            int pageSize = 10;
            return await PaginatedList<Student>.CreateAsync(students, pageNumber ?? 1, pageSize);

        }

        public async Task<Student> GetStudentById(int StudentId)
        {
            if (StudentId > 0)
            {
                var student = await _unitOfWork.Students.GetById(StudentId);
                if (student != null)
                {
                    return student;
                }
            }
            return null;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            if (student != null)
            {
                var Student = await _unitOfWork.Students.GetById(student.Id);
                if (Student != null)
                {
                    Student.Name = student.Name;
                    Student.Gender = student.Gender;
                    Student.Age = student.Age;

                    _unitOfWork.Students.Update(Student);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}