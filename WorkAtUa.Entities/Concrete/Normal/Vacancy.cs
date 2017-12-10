using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyVacancy: Entity
    {
        public string ProfessionName { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int CompanyId { get; set; }
        public string Requirments { get; set; }
        public string TypeOfEmployment { get; set; }
        public string Description { get; set; }
        public string EmployerName { get; set; }
        public string EmployerPhone { get; set; }
        public string City { get; set; }
        public DateTime CreationTime { get; set; }
        public List<string> Categories { get; set; }
        public bool IsSolved { get; set; }

        public MyVacancy()
        {
            Id = -1;
            ProfessionName = "";
            MinSalary = 0;
            MaxSalary = 0;
            CompanyId = -1;
            Requirments = "";
            TypeOfEmployment = "";
            Description = "";
            Categories = new List<string>();
            EmployerName = "";
            EmployerPhone = "";
            City = "";
            CreationTime = DateTime.Now;
            IsSolved = false;
        }

        public MyVacancy(int id, string profName, decimal minSalary, decimal maxSalary, int compId,
            string req, string typeOfEmp, string desc, DateTime creationTime, string empName, string empPhone, string city, List<string> categories, bool isSolved)
        {
            Id = id;
            ProfessionName = profName;
            MinSalary = minSalary;
            MaxSalary = maxSalary;
            CompanyId = compId;
            Requirments = req;
            TypeOfEmployment = typeOfEmp;
            Description = desc;
            Categories = categories;
            EmployerName = empName;
            EmployerPhone = empPhone;
            City = city;
            CreationTime = creationTime;
            IsSolved = isSolved;
        }

    }
}
