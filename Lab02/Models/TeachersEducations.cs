using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Models
{
    public class TeachersEducations
    {
        public int TeacherId { get; set; }
        public int EducationId { get; set; }
        //navigatie properties - many to many
        public Teacher Teacher { get; set; }
        public Education Education { get; set; }



    }
}
