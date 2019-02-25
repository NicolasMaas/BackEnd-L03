using System.Collections.Generic;
using Lab02.Models;

namespace Lab02.Data
{
    public interface IDataInitializer
    {
        IEnumerable<Education> Educations { get; set; }
        IEnumerable<Student> Students { get; set; }
        IEnumerable<Teacher> Teachers { get; set; }
    }
}