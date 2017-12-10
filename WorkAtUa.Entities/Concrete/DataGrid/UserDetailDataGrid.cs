using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyUserDetailDataGrid: Entity
    {
        public string Header { get; set; }
        public decimal Salary { get; set; }
        public string Description { get; set; }
        public int Education { get; set; }
        public string EducationLevel { get; set; }
        public string EducationPlace { get; set; }
        public string EducationSpeciality { get; set; }
        public int JobExperience { get; set; }
        public string JobExperienceCompany { get; set; }
        public string JobExperienceCity { get; set; }
        public string JobExperiencePost { get; set; }
        public DateTime JobExperienceStart { get; set; }
        public DateTime JobExperienceEnd { get; set; }
        public string JobExperienceDesc { get; set; }
        
        public MyUserDetailDataGrid()
        {
            Id = -1;
            Salary = -1;
            Description = "123";
            Education = -1;
            EducationLevel = "";
            EducationPlace = "";
            EducationSpeciality = "";
            JobExperience = -1;
            JobExperienceCompany = "";
            JobExperienceCity = "";
            JobExperienceDesc = "";
            JobExperiencePost = "";
            JobExperienceStart = new DateTime();
            JobExperienceEnd = new DateTime();
            Header = "123";
        }

        public MyUserDetailDataGrid(int id, string header, decimal salary, string desc, 
            int education, string eduLevel, string eduPlace, string eduSpec,
            int jobExperience, string expCompany, string expCity, string expPost,
            DateTime expStart, DateTime expEnd,
            string expDesc)
        {
            Id = id;
            Header = header;
            Salary = salary;
            Description = desc;
            Education = education;
            EducationLevel = eduLevel;
            EducationPlace = eduPlace;
            EducationSpeciality = eduSpec;
            JobExperience = jobExperience;
            JobExperienceCompany = expCompany;
            JobExperienceCity = expCity;
            JobExperiencePost = expPost;
            JobExperienceStart = expStart;
            JobExperienceEnd = expEnd;
            JobExperienceDesc = expDesc;
        }
    }
}
