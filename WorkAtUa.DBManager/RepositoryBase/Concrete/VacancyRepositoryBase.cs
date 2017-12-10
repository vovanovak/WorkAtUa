using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public class VacancyRepositoryBase: RepositoryBase<MyVacancy, Vacancy>
    {
        protected override System.Data.Entity.DbSet<Vacancy> GetTable()
        {
            return ctx.Vacancies;
        }

        protected override System.Linq.Expressions.Expression<Func<Vacancy, MyVacancy>> GetConverter()
        {
            return c => new MyVacancy()
            {
                Id = c.Id,
                ProfessionName = c.ProfessionName,
                MinSalary = c.MinSalary,
                MaxSalary = c.MaxSalary,
                TypeOfEmployment = c.TypeOfEmployment,
                CompanyId = c.CompanyId,
                EmployerName = c.EmployerName,
                EmployerPhone = c.EmployerPhone,
                City = c.City,
                Requirments = c.Requirments,
                Description = c.Description,
                Categories = c.VacancyInCategories.Select(v => v.Category.Name).ToList(),
                CreationTime = c.CreationTime.Value,
                IsSolved = c.IsSolved
            };
        }

        protected override void UpdateEntry(Vacancy dbEntity, MyVacancy entity)
        {
            dbEntity.ProfessionName = entity.ProfessionName;
            dbEntity.MinSalary = entity.MinSalary;
            dbEntity.MaxSalary = entity.MaxSalary;
            dbEntity.Requirments = entity.Requirments;
            dbEntity.TypeOfEmployment = entity.TypeOfEmployment;
            dbEntity.CompanyId = entity.CompanyId;
            dbEntity.Description = entity.Description;
            dbEntity.EmployerName = entity.EmployerName;
            dbEntity.City = entity.City;
            dbEntity.EmployerPhone = entity.EmployerPhone;
            dbEntity.CreationTime = entity.CreationTime;
            dbEntity.IsSolved = entity.IsSolved;
        }
    }
}
