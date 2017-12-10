using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyUserDetail: Entity
    {
        public string Header { get; set; }
        public decimal Salary { get; set; }
        public string Description { get; set; }
        public int Education { get; set; }
        public int JobExperience { get; set; }

        public MyUserDetail()
        {
            Id = -1;
            Salary = -1;
            Description = "";
            Education = -1;
            JobExperience = -1;
            Header = "";
        }

        public MyUserDetail(int id, string header, decimal salary, string desc, 
            int education, int jobExperience)
        {
            Id = id;
            Header = header;
            Salary = salary;
            Description = desc;
            Education = education;
            JobExperience = jobExperience;
        }
    }
}
