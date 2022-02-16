using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Models;

namespace Univer.Data
{
    public class UniverContext: DbContext
    {
        public UniverContext(DbContextOptions<UniverContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
    }
}
