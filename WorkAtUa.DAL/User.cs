//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkAtUa.DAL
{
    using System;
    using System.Collections.Generic;

    public partial class User : DbEntity
    {
        public User()
        {
            this.SearchAgents = new HashSet<SearchAgent>();
            this.SearchAgentsEmployers = new HashSet<SearchAgentsEmployer>();
            this.SearchResultsEmployers = new HashSet<SearchResultsEmployer>();
            this.UserInCategories = new HashSet<UserInCategory>();
            this.UserInRoles = new HashSet<UserInRole>();
            this.VacancyInUsers = new HashSet<VacancyInUser>();
        }
    
     
        public string Email { get; set; }
        public string Password { get; set; }
        public System.DateTime Birthday { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }
        public Nullable<int> CompanyDetails { get; set; }
        public Nullable<int> UserDetails { get; set; }
    
        public virtual City City { get; set; }
        public virtual CompanyDetail CompanyDetail { get; set; }
        public virtual ICollection<SearchAgent> SearchAgents { get; set; }
        public virtual ICollection<SearchAgentsEmployer> SearchAgentsEmployers { get; set; }
        public virtual ICollection<SearchResultsEmployer> SearchResultsEmployers { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual ICollection<UserInCategory> UserInCategories { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<VacancyInUser> VacancyInUsers { get; set; }
    }
}
