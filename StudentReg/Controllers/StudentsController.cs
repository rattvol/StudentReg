using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentReg.Models;

namespace StudentReg.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentsRegContext _context;

        public StudentsController(StudentsRegContext context)
        {
            _context = context;
        }
       
    }
}