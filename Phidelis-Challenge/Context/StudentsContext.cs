using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Phidelis_Challenge.Entities;

#pragma warning disable CS1591
namespace Phidelis_Challenge.Context
{
    public class StudentsContext : DbContext
    {
        public StudentsContext(DbContextOptions<StudentsContext> options): base(options)
        {

        }

        public DbSet<Student> Students{ get; set; }
        public DbSet<Time> Times{ get; set; }

    }
}
#pragma warning disable CS1591
