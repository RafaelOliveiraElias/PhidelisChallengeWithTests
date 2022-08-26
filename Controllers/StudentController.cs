using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phidelis_Challenge.Entities;
using Phidelis_Challenge.Context;

namespace Phidelis_Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentsContext _context;
        public StudentController(StudentsContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
    }
}