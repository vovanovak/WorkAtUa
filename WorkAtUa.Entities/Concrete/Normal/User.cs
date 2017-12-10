using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WorkAtUa.Entities
{
    public class MyUser : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public string Phone { get; set; }
        public string City { get; set;  }
        public int CompanyDetails { get; set; }
        public int UserDetails { get; set; }
        public List<int> Roles { get; set; }
        public List<int> Categories { get; set; }
       
        public MyUser()
        {
            Id = -1;
            Email = "";
            Password = "";
            Birthday = new DateTime();
            Name = "";
            Surname = "";
            FathersName = "";
            City = "";
            Phone = "";
            CompanyDetails = -1;
            UserDetails = -1;
            Roles = new List<int>();
            Categories = new List<int>();
        }

        public MyUser(int id, string email, string password, DateTime birthday,
            string name, string surname, string fathersName, string phone, string city,
            int companyDetails, int userDetails, List<int> roles, List<int> categories):base(id)
        {
            Id = id;
            Email = email;
            Password = password;
            Birthday = birthday;
            Name = name;
            Surname = surname;
            FathersName = fathersName;
            Phone = phone;
            City = city;
            CompanyDetails = companyDetails;
            UserDetails = userDetails;
            Roles = roles;
            Categories = categories;
        }
    }
}
