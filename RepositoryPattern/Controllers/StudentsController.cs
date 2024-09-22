using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryPattern.Core.Models;
using RepositoryPattern.Services;
using RepositoryPattern.Services.Interfaces;
using RepositoryPattern.Utilities;

namespace RepositoryPattern.Controllers
{


    public class StudentsController : Controller
    {

        public readonly IStudentService _studentService;
        public readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;
        public StudentsController(IStudentService studentService, ILoginService loginService, IConfiguration configuration)
        {
            _studentService = studentService;
            _loginService = loginService;
            _configuration = configuration;
        }

        [JwtAuthentication]
        public async Task<IActionResult> GetStudentList(string searchString)
        {
            var studentList = await _studentService.GetAllStudents(searchString);
            if (studentList == null)
            {
                return NotFound();
            }
            return View(studentList);
        }

        [JwtAuthentication]
        public async Task<IActionResult> GetStudentListManual(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["AgeSortParm"] = sortOrder == "age" ? "age_desc" : "age";
            ViewData["CurrentFilter"] = searchString;
            var students = _studentService.GetAllStudentsManual(sortOrder, currentFilter, searchString, pageNumber);

            return View(await students);
        }
        [JwtAuthentication]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            var student = await _studentService.GetStudentById(studentId);

            if (student != null)
            {
                return View(student);
            }
            else
            {
                return NotFound();
            }
        }
        [JwtAuthentication]
        public IActionResult CreateStudent()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [JwtAuthentication]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            var isStudentCreated = await _studentService.CreateStudent(student);

            if (isStudentCreated)
            {
                return RedirectToAction(nameof(GetStudentList));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [JwtAuthentication]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            if (student != null)
            {
                var isStudentCreated = await _studentService.UpdateStudent(student);
                if (isStudentCreated)
                {
                    return RedirectToAction(nameof(GetStudentList));
                }
                return RedirectToAction(nameof(GetStudentById));
            }
            else
            {
                return NotFound();
            }
        }


        [JwtAuthentication]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var isStudentCreated = await _studentService.DeleteStudent(studentId);

            if (isStudentCreated)
            {
                return RedirectToAction(nameof(GetStudentList));
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var token = Request.Cookies["jwt"];
            var jwtkey = Request.Cookies["jwtkey"];

            if (token != null && jwtkey != null)
            {
                var username = Authentication.ValidateToken(token, jwtkey);
                if (username != null)
                {
                    return RedirectToAction(nameof(GetStudentList));
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Login(Login login)
        {
            AppSettingSIngleton appSetting = AppSettingSIngleton.GetInstance(_configuration);
            var jwtkey = appSetting.JwtKey;
            var jwtexpiredays = appSetting.JwtExpireDays;
            var jwtissuer = appSetting.JwtIssuer;
            var jwtaudience = appSetting.JwtAudience;
            var jwtToken = _loginService.Login(login, jwtkey, jwtexpiredays, jwtissuer, jwtaudience);
            var validUserName = Authentication.ValidateToken(jwtToken, jwtkey);
            if (string.IsNullOrEmpty(validUserName))
            {
                ModelState.AddModelError("", "Unauthorized login attempt ");

                return View();
            }

            CookieOptions options = new CookieOptions()
            {
                Domain = "localhost",
                Path = "/",
                Expires = DateTime.Now.AddDays(7),
                Secure = false,
                HttpOnly = true,
                IsEssential = true
            };

            Response.Cookies.Append("jwt", jwtToken, options);
            Response.Cookies.Append("jwtkey", jwtkey, options);
            return RedirectToAction(nameof(GetStudentList));
        }

        [HttpPost]
        public async Task<IActionResult> Details(int studentId)
        {
            var student = await _studentService.GetStudentById(studentId);
            return PartialView("_Details", student);
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("jwtkey");
            return RedirectToAction(nameof(Login));
        }
    }
}
