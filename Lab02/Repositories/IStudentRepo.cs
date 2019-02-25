using Lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Repositories
{
    public interface IStudentRepo
    {
        //READ 
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentAsync(int id);

        //CREATE
        Task<Student> AddAsync(Student student);

        //UPDATE
        Task<Student> Update(Student student);


        //DELETE  (niet doen >> archiveer)
        Task Delete(int id); 

    }
}
