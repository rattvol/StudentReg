using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentReg.Models;
using StudentReg.sakila;

namespace StudentReg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly studentsregContext _context;
        private int StudentToCourseAdd { get; set; }

        public HomeController(studentsregContext context, ILogger<HomeController> logger)
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
        public IActionResult AddedStudent(string Name, string MiddleName, string SurName)//запись после добавления студента
        {
            Students student = new Students();
            student.Name = Name;
            student.MiddleName = MiddleName;
            student.SurName = SurName;
            _context.Students.Add(student);
            _context.SaveChangesAsync();
            return View("Index", GetStudWCourses());
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        public IActionResult AddedCourse(string CourseName)//запись в базу после добавления курса
        {
            if (!_context.Courses.Any(a => a.CourseName==CourseName))
            {
                _context.Courses.Add(new Courses { CourseName = CourseName });
                _context.SaveChanges();
            }
            return View("Index", GetStudWCourses());
        }
        [Route("AddToCourse")]
        public IActionResult AddToCourse(int id)
        {
            Students studentName = _context.Students.Where(b => b.IdStudents == id).First();
            ViewData["StudentName"] = studentName.Name + " " + studentName.MiddleName + " " + studentName.SurName;
            ViewData["StudentId"] = studentName.IdStudents;
            List<Courses> courses = _context.Courses
                                            .Where(b => _context.Registration
                                                                .Where(a => a.CourseId == b.Idcourses & a.StudentId == id)
                                                                .Any() == false)
                                            .ToList();
            return View("AddToCourse", courses);
        }
        [Route("AddedToCourse")]
        public IActionResult AddedToCourse(int id, int studId)//запись в базу после регистрации на курсе
        {
            Registration reg = new Registration();
            reg.StudentId = studId;
            reg.CourseId = id;
            _context.Registration.Add(reg);
            _context.SaveChanges();
            return View("Index", GetStudWCourses());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<StudWithCourse> GetStudWCourses(string filter = "")
        {

            List<StudWithCourse> studWithCourses = new List<StudWithCourse>();
            //на случай необходимости фильтра на сервере
            List<Students> swc = String.IsNullOrEmpty(filter) ? _context.Students
                .Where(s => s.Name.Contains(filter) || s.Name.Contains(filter) || s.Name.Contains(filter))
                .ToList() : _context.Students.ToList();
            //создание экземпляра строки
            foreach (Students students in swc)
            {
                studWithCourses.Add(new StudWithCourse
                {
                    IdStudents = students.IdStudents,
                    Name = students.Name,
                    MiddleName = students.MiddleName,
                    SurName = students.SurName,
                    //добавление перечня курсов
                    courses = _context.Registration
                .Where(a => a.StudentId == students.IdStudents)
                .Select(a => new Courses
                {
                    Idcourses = a.CourseId,
                    CourseName = _context.Courses
                                                    .Where(b => b.Idcourses == a.CourseId)
                                                    .Select(b => b.CourseName).First()
                }).ToList()
                });
            }
            return studWithCourses;
        }
    }
}
