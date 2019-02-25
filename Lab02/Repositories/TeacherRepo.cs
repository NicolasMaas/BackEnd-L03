using Lab02.Data;
using Lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab02.Repositories
{
    public class TeacherRepo : ITeacherRepo
    {
        private readonly SchoolDBContext context;

        public TeacherRepo(SchoolDBContext schoolDBContext) //niet Dbcontext!
        {
            this.context = schoolDBContext;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await context.Teachers.OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<Teacher> GetTeacherForId(int id)
        {
            return await context.Teachers.Where(t => t.Id == id).SingleAsync();
        }

        public async Task<Teacher> Add (Teacher teacher)
        {
            try
            {
                var result = context.AddAsync(teacher);
                await context.SaveChangesAsync();
                return teacher;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
