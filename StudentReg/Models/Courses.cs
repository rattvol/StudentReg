using System;
using System.Collections.Generic;

namespace StudentReg.Models
{
    public partial class Courses
    {
        public int CoursesID { get; set; }
        public string CoursesName { get; set; }

        public virtual List<Registration> Registrations { get; set; }
    }
}
