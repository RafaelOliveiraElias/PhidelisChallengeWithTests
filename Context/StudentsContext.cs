using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phidelis_Challenge.Entities;

namespace Phidelis_Challenge.Context
{
    public class StudentsContext : DbContext
    {
        public StudentsContext(DbContextOptions<StudentsContext> options): base(options)
        {

        }

        public DbSet<Student> Students{ get; set; }
    }
}