using System;
using System.Collections.Generic;

namespace StudentReg.Models
{
    public partial class Registration
    {
        public int StudentsId { get; set; }
        public int CoursesId { get; set; }
        public int Id { get; set; }

        public virtual Students Students { get; set; }
        public virtual Courses Courses { get; set; }
    }
}
