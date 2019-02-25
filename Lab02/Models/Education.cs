using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        //navigatie property
        public ICollection<Student> Students { get; set; }
        public ICollection<TeachersEducations> TeachersEcuations { get; set; }
    }
}
