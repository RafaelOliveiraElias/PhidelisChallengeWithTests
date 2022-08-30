using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phidelis_Challenge.Context;
using Phidelis_Challenge.Entities;
using Phidelis_Challenge.utils;

// Criando o Escopo do hosted service adicionando os estudantes no AddingNewStudent e fazendo WhenAll de 5 estudantes, como pedido.
// TimeSeconds busca o valor do Timer em segundos no banco de dados.

namespace Phidelis_Challenge.HostedService
{
    public interface IScopedService
    {
        Task<bool> Write();
        int TimeSeconds();
        int GetListSize();
    }

    public class MyScopedService: IScopedService
    {
        private readonly StudentsContext _context;
        private readonly ILogger<MyScopedService> _logger;
        public MyScopedService(ILogger<MyScopedService> logger, StudentsContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> Write()
        {
            await Task.WhenAll(AddingNewStudent(),
            AddingNewStudent(),
            AddingNewStudent(),
            AddingNewStudent(),
            AddingNewStudent());
            return true;
        }
        public int TimeSeconds()
        {
            var foundTimer = _context.Times.Find(1);
            return foundTimer.Seconds;
        }
        public async Task<bool> AddingNewStudent()
        {
            StudentGenerator test = new StudentGenerator();
            await _context.Students.AddAsync(await test.full());
            _context.SaveChanges();
            return true;
        }
        public int GetListSize()
        {
            var allStudents = _context.Students.Count();
            return allStudents;
        }
    }
}