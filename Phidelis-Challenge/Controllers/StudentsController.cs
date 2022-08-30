using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phidelis_Challenge.Entities;
using Phidelis_Challenge.Context;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Phidelis_Challenge.utils;
using Microsoft.AspNetCore.Http;

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
        /// <summary>
        /// Retorna toda a lista de matriculados.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso em listar todos os estudantes matriculados.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> FindAll()
        {
            StudentGenerator test = new StudentGenerator();
            var allStudents = _context.Students.Where(x => x.Name.Contains(""));
            _context.SaveChanges();
            System.Console.WriteLine(await test.GenarateRandomStudent());
            return Ok(allStudents);
        }
        /// <summary>
        /// Adiciona estudante na lista de matriculados.
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Novo estudante criado.</returns>
        /// <response code="201">Novo estudante criado com sucesso.</response>
        /// <response code="400">O Body está vazio</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Create(StudentBody student)
        {
            Student adding = new Student();
            adding.Document = student.Document;
            adding.Name = student.Name;
            adding.ParentName = student.ParentName;
            _context.Add(adding);
            _context.SaveChanges();
            return Created("Student", adding);
        }
        /// <summary>
        /// Retorna um estudante da lista de acordo com seu id.
        /// </summary>
        /// <response code="200">Encontrado com Sucesso</response>
        /// <response code="404">Nome não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetById(int id)
        {
            var Student = _context.Students.Find(id);
            
            if (Student == null)
                return NotFound();

            return Ok(Student);
        }
        /// <summary>
        /// Deleta estudantes da lista por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <response code="201">Estudante editado com sucesso.</response>
        /// <response code="400">O Body está vazio</response>
        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Update(int id, StudentBody student)
        {
            var foundStudent = _context.Students.Find(id);
            if (foundStudent == null)
                return NotFound();
            foundStudent.Name = student.Name;
            foundStudent.ParentName = student.ParentName;
            _context.Students.Update(foundStudent);
            _context.SaveChanges();
            return Accepted(foundStudent);
        }
        /// <summary>
        /// Deleta estudantes da lista por id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Estudante deletado com sucesso.</response>
        /// <response code="404">Id não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            var Student = _context.Students.Find(id);
            if (Student == null)
                return NotFound();
            _context.Students.Remove(Student);
            _context.SaveChanges();
            return NoContent();
        }
        /// <summary>
        /// Seleciona estudantes a partir de um nome.
        /// </summary>
        /// <param name="nome"></param>
        /// <response code="200">Encontrado com Sucesso</response>
        /// <response code="404">Nome não encontrado</response>
        [HttpGet("GetByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetByName(string nome)
        {
            var Students = _context.Students.Where(x => x.Name.Contains(nome));
            
            if (Students.Count() == 0)
                return NotFound();

            return Ok(Students);
        }
        /// <summary>
        /// Configurar o tempo de atualização de estudantes adicionados aleatoriamente.
        /// </summary>
        /// <param name="Seconds"></param>
        /// <response code="201">Timer editado com sucesso.</response>
        /// <response code="400">Tempo inválido</response>
        [HttpPut("SetTimer")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult SetTimer(int Seconds)
        {
            Time foundTimer = _context.Times.Find(1);
            foundTimer.Seconds = Seconds;
            _context.SaveChanges();
            TimeBody result = new TimeBody();
            result.Seconds = foundTimer.Seconds;
            return Accepted(result);
        }
    }
}