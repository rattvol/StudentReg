using System;
using System.Collections.Generic;

namespace StudentReg.sakila
{
    public partial class Students
    {
        public int IdStudents { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
    }
    public class StudWithCourse
    {
        public int IdStudents { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public List<Courses> courses { get; set; }
    }
    
}
