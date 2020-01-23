using System;
using System.Collections.Generic;

namespace StudentReg.Models
{
    public partial class Students
    {
        public int StudentsId { get; set; }
        public string StudentsName { get; set; }
        public string StudentsMiddleName { get; set; }
        public string StudentsSurName { get; set; }

       // public virtual List<Registration> Registrations {get; set;}
    }
    public class StudWithCourse
    {
        public int IdStudents { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public List<Courses> Courses { get; set; }
    }
}
