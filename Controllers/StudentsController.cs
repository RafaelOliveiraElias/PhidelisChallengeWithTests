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
    public class StudentsController : ControllerBase
    {
        private readonly StudentsContext _context;
        public StudentsController(StudentsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult FindAll()
        {
            var allStudents = _context.Students.Where(x => x.Name.Contains(""));
            _context.SaveChanges();
            return Ok(allStudents);
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Student = _context.Students.Find(id);
            
            if (Student == null)
                return NotFound();

            return Ok(Student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student Student)
        {
            var foundStudent = _context.Students.Find(id);
            if (foundStudent == null)
                return NotFound();
            foundStudent.Name = Student.Name;
            foundStudent.MotherName = Student.MotherName;
            foundStudent.MotherName = Student.MotherName;
            _context.Students.Update(foundStudent);
            _context.SaveChanges();
            return Ok(foundStudent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Student = _context.Students.Find(id);
            if (Student == null)
                return NotFound();
            _context.Students.Remove(Student);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName(string nome)
        {
            var Students = _context.Students.Where(x => x.Name.Contains(nome));
            
            if (Students == null)
                return NotFound();

            return Ok(Students);
        }
    }
}