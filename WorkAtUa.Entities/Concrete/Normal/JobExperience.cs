using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyJobExperience
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string Post { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Desc { get; set; }

        public MyJobExperience()
        {
            Id = -1;
            Company = "";
            City = "";
            Post = "";
            Start = new DateTime();
            End = new DateTime();
            Desc = "";
        }

        public MyJobExperience(int id, string company, string city, string post,
                               DateTime start, DateTime end, string desc)
        {
            Id = id;
            Company = company;
            City = city;
            Post = post;
            Start = start;
            End = end;
            Desc = desc;
        }
    }
}
