using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Models
{
    public class Student:Person
    {
        public int? EducationId { get; set; }

        //navigatie property - one to many
        public Education Education { get; set; }
           

    }
}
