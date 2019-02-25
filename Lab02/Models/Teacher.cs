using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Models
{
    public class Teacher : Person
    {

        // public int EducationId { get; set; }

        public ICollection<TeachersEducations> TeachersEducations { get; set; }
    }
}
