using System.Collections.Generic;
using System.Threading.Tasks;
using Lab02.Models;

namespace Lab02.Repositories
{
    public interface ITeacherRepo
    {
        Task<Teacher> Add(Teacher teacher);
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetTeacherForId(int id);
    }
}