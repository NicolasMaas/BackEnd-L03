using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02.Models
{
    public class Person
    {
        //Id, Name,Gender, Email, Birthday, Password en een ImageUrl

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public Uri ImageUrl { get; set; }

        public enum GenderType
        {
            Male = 0,
            Female = 1
        }

        public GenderType Gender { get; set; }



    }
}
