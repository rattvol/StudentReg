using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentReg.Models;

namespace StudentReg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentsRegContext _context;
        private int StudentToCourseAdd { get; set; }

        public HomeController(StudentsRegContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(GetStudWCourses());
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [Route("AddedStudent")]
        public IActionResult AddedStudent(string StudentsName, string StudentsMiddleName, string StudentsSurName)//запись после добавления студента
        {
            Students student = new Students();
            student.StudentsName = StudentsName;
            student.StudentsMiddleName = StudentsMiddleName;
            student.StudentsSurName = StudentsSurName;
            _context.Students.AddAsync(student);
           _context.SaveChangesAsync();
            return View("Index", GetStudWCourses());
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        public IActionResult AddedCourse(string CoursesName)//запись в базу после добавления курса
        {
            if (!_context.Courses.Any(a => a.CoursesName== CoursesName))
            {
                _context.Courses.Add(new Courses { CoursesName = CoursesName });
                _context.SaveChanges();
            }
            return View("Index", GetStudWCourses());
        }
        [Route("AddToCourse")]
        public IActionResult AddToCourse(int id)
        {
            Students studentName = _context.Students.Where(b => b.StudentsId == id).First();
            ViewData["StudentName"] = studentName.StudentsName + " " + studentName.StudentsMiddleName + " " + studentName.StudentsSurName;
            ViewData["StudentId"] = studentName.StudentsId;
            List<Courses> courses = _context.Courses
                                            .Where(b => _context.Registration
                                                                .Where(a => a.CoursesId == b.CoursesID & a.StudentsId == id)
                                                                .Any() == false)
                                            .ToList();
            return View("AddToCourse", courses);
        }
        [Route("AddedToCourse")]
        public IActionResult AddedToCourse(int id, int studId)//запись в базу после регистрации на курсе
        {
            Registration reg = new Registration();
            reg.StudentsId = studId;
            reg.CoursesId = id;
            _context.Registration.Add(reg);
            _context.SaveChanges();
            return View("Index", GetStudWCourses());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<StudWithCourse> GetStudWCourses()
        {
            List<StudWithCourse> studWithCourses = new List<StudWithCourse>();
            List<Students> swc = _context.Students.ToList();
            //создание экземпляра строки
            foreach (Students students in swc)
            {
                studWithCourses.Add(new StudWithCourse
                {
                    IdStudents = students.StudentsId,
                    Name = students.StudentsName,
                    MiddleName = students.StudentsMiddleName,
                    SurName = students.StudentsSurName,
                    //добавление перечня курсов
                    Courses = _context.Registration
                .Where(a => a.StudentsId == students.StudentsId)
                .Select(a => new Courses
                {
                    CoursesID = a.CoursesId,
                    CoursesName = _context.Courses
                                                    .Where(b => b.CoursesID == a.CoursesId)
                                                    .Select(b => b.CoursesName).First()
                }).ToList()
                });
            }
            return studWithCourses;
        }
    }
}
