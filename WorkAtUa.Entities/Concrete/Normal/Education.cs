using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyEducation
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Place { get; set; }
        public string Speciality { get; set; }

        public MyEducation()
        {
            Id = -1;
            Level = "";
            Place = "";
            Speciality = "";
        }

        public MyEducation(int id, string level, string place, string speciality)
        {
            Id = id;
            Level = level;
            Place = place;
            Speciality = speciality;
        }
    }
}
