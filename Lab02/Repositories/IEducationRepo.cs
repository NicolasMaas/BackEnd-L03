using System.Collections.Generic;
using System.Threading.Tasks;
using Lab02.Models;

namespace Lab02.Repositories
{
    public interface IEducationRepo
    {
        Task<IEnumerable<Education>> GetAllEducationsAsync();
        Task<Education> GetEducationAsync(int id);
    }
}