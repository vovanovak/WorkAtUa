using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyUserDataGrid : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public int CompanyDetails { get; set; }
        public int UserDetails { get; set; }
        public string Roles { get; set; }
        public string Categories { get; set; }

        public MyUserDataGrid()
        {
            Id = -1;
            Email = null;
            Password = null;
            Birthday = new DateTime();
            Name = null;
            Surname = null;
            FathersName = null;
            City = null;
            Phone = null;
            CompanyDetails = -1;
            UserDetails = -1;
            Roles = null;
            Categories = null;
        }

        public MyUserDataGrid(int id, string email, string password, DateTime birthday,
            string name, string surname, string fathersName, string phone, string city,
            int companyDetails, int userDetails, string roles, string categories)
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
