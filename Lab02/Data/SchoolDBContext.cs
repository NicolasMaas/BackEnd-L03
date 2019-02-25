using Lab02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Data
{
    public class SchoolDBContext : DbContext
    {
        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        //nodig voor de veel op veel relatie
        public DbSet<TeachersEducations> TeachersEducations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //composite key
            modelBuilder.Entity<TeachersEducations>().HasKey(t => new { t.TeacherId, t.EducationId });} 
        }
}
