using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Models
{
    public class Teacher
    {

        // public int EducationId { get; set; }

        public ICollection<Education> Educations { get; set; }
    }
}
