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

    public partial class Vacancy : DbEntity
    {
        public Vacancy()
        {
            this.SearchResults = new HashSet<SearchResult>();
            this.VacancyInCategories = new HashSet<VacancyInCategory>();
            this.VacancyInComments = new HashSet<VacancyInComment>();
            this.VacancyInUsers = new HashSet<VacancyInUser>();
        }
    
       
        public string ProfessionName { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int CompanyId { get; set; }
        public string City { get; set; }
        public string EmployerName { get; set; }
        public string EmployerPhone { get; set; }
        public string Requirments { get; set; }
        public string TypeOfEmployment { get; set; }
        public string Description { get; set; }
        public bool IsSolved { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    
        public virtual CompanyDetail CompanyDetail { get; set; }
        public virtual ICollection<SearchResult> SearchResults { get; set; }
        public virtual ICollection<VacancyInCategory> VacancyInCategories { get; set; }
        public virtual ICollection<VacancyInComment> VacancyInComments { get; set; }
        public virtual ICollection<VacancyInUser> VacancyInUsers { get; set; }
    }
}
